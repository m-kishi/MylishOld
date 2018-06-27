namespace Mylish.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using Newtonsoft.Json;

    /// <summary>
    /// 成績モデル
    /// </summary>
    public class Score : IDataModel
    {
        /// <summary>ID</summary>
        [Column("id")]
        [JsonProperty(PropertyName = "Id")]
        public string   Id        { get; set; }
        /// <summary>日付</summary>
        [Column("date")]
        [JsonProperty(PropertyName = "Date")]
        public DateTime Date      { get; set; }
        /// <summary>解答数</summary>
        [Column("answered")]
        [JsonProperty(PropertyName = "Answered")]
        public int      Answered  { get; set; }
        /// <summary>正解数</summary>
        [Column("corrected")]
        [JsonProperty(PropertyName = "Corrected")]
        public int      Corrected { get; set; }

        /// <summary>
        /// 成績IDの生成
        /// </summary>
        /// <remarks>
        /// 現在日時のハッシュ値のハッシュ値(SHA256)
        /// </remarks>
        public static string GenerateID()
        {
            var seed = DateTime.Now.ToString("yyyyMMddhhmmss");
            var sha256 = new System.Security.Cryptography.SHA256Managed();

            var hash = sha256.ComputeHash(
                System.Text.Encoding.UTF8.GetBytes(seed)
            );
            hash = sha256.ComputeHash(hash);
            sha256.Clear();

            return BitConverter.ToString(hash).ToUpper().Replace("-", "");
        }
    }
}