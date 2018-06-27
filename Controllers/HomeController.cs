namespace Mylish.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Homeコントローラー
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// ベースアクション
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// エラー
        /// </summary>
        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}