namespace Mylish
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.SpaServices.Webpack;
    using Microsoft.Data.Sqlite;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Mylish.Models;
    using Mylish.Services;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<IDbConnection>(provider =>
                new SqliteConnection(
                    Configuration.GetConnectionString("DefaultConnection")
                )
            );
            services.AddScoped<IMylishService, MylishService>();

            // Dapperのマッピング設定
            // モデルクラスの属性([Column("XXX")])で指定したカラム名をマッピングするように指定する
            Assembly.GetAssembly(typeof(IDataModel))
                .GetTypes()
                .Where(t =>
                    typeof(IDataModel).IsAssignableFrom(t) && typeof(IDataModel) != t
                )
                .ToList()
                .ForEach(dataModelType =>
                {
                    Dapper.SqlMapper.SetTypeMap(dataModelType,
                        new Dapper.CustomPropertyTypeMap(
                            dataModelType,
                            (type, columnName) =>
                                type.GetProperties().FirstOrDefault(prop =>
                                    prop.Name == columnName
                                    ||
                                    prop.GetCustomAttributes(true).OfType<ColumnAttribute>().Any(attr => attr.Name == columnName)
                                )
                        )
                    );
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
