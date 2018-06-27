namespace Mylish.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Newtonsoft.Json;

    /// <summary>
    /// 問題モデル
    /// </summary>
    public class Problem : IDataModel
    {
        /// <summary>成績ID</summary>
        [Column("score_id")]
        [JsonProperty(PropertyName = "ScoreId")]
        public string ScoreId    { get; set; }
        /// <summary>例文番号</summary>
        [Column("sentence_no")]
        [JsonProperty(PropertyName = "SentenceNo")]
        public int    SentenceNo { get; set; }
        /// <summary>出題順序</summary>
        [Column("seq")]
        [JsonProperty(PropertyName = "Seq")]
        public int    Seq        { get; set; }
        /// <summary>0:誤り/1:正解</summary>
        [Column("result")]
        [JsonProperty(PropertyName = "Result")]
        public int    Result     { get; set; }
        /// <summary>解答文章</summary>
        [Column("answer")]
        [JsonProperty(PropertyName = "Answer")]
        public string Answer     { get; set; }
    }
}