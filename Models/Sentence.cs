namespace Mylish.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Newtonsoft.Json;

    /// <summary>
    /// 例文モデル
    /// </summary>
    public class Sentence : IDataModel
    {
        /// <summary>番号</summary>
        [Column("no")]
        [JsonProperty(PropertyName = "No")]
        public string No { get; set; }
        /// <summary>英文</summary>
        [Column("en")]
        [JsonProperty(PropertyName = "En")]
        public string En { get; set; }
        /// <summary>和訳</summary>
        [Column("ja")]
        [JsonProperty(PropertyName = "Ja")]
        public string Ja { get; set; }

        /// <summary>
        /// 空の例文モデルを生成
        /// </summary>
        public static Sentence Empty()
        {
            var sentence = new Sentence();
            sentence.No = "0";
            sentence.En = "";
            sentence.Ja = "";
            return sentence;
        }
    }
}