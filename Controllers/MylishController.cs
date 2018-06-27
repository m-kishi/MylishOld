namespace Mylish.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Mylish.Models;
    using Mylish.Params;
    using Mylish.Services;

    /// <summary>
    /// バックエンドAPIコントローラー
    /// </summary>
    [Route("api")]
    public class MylishController : Controller
    {
        /// <summary>サービス</summary>
        private IMylishService service;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="service">サービス</param>
        public MylishController(IMylishService service)
        {
            this.service = service;
        }

        /// <summary>
        /// 成績一覧の取得
        /// </summary>
        [HttpGet("[action]")]
        public IEnumerable<Score> GetScores()
        {
            return service.GetScores();
        }

        /// <summary>
        /// 成績の削除
        /// </summary>
        /// <param name="id">成績ID</param>
        [HttpPost("[action]/{id}")]
        public ActionResult Delete(string id)
        {
            service.Delete(id);
            return Ok();
        }

        /// <summary>
        /// スタート
        /// </summary>
        [HttpPost("[action]")]
        public string Start()
        {
            return service.Start();
        }

        /// <summary>
        /// 例文の取得
        /// </summary>
        /// <param name="id">成績ID</param>
        [HttpGet("[action]/{id}")]
        public Sentence GetSentence(string id)
        {
            int no = service.NextSentenceNo(id);
            var sentence = service.EmptySentence();
            if (no > 0) {
                sentence = service.GetSentence(no);
            }
            return sentence;
        }

        /// <summary>
        /// 解答の判定
        /// </summary>
        /// <param name="id">成績ID</param>
        /// <param name="param">パラメタ</param>
        [HttpPost("[action]/{id}")]
        public ActionResult Submit(string id, [FromBody]Submit param)
        {
            service.Submit(id, param.No, param.Answer);
            service.Update(id);
            return Ok();
        }

        /// <summary>
        /// 成績の取得
        /// </summary>
        /// <param name="id">成績ID</param>
        [HttpGet("[action]/{id}")]
        public Score GetScore(string id)
        {
            return service.GetScore(id);
        }

        /// <summary>
        /// 結果一覧の取得
        /// </summary>
        /// <param name="id">成績ID</param>
        [HttpGet("[action]/{id}")]
        public IEnumerable<Result> GetResult(string id)
        {
            return service.GetResult(id);
        }
    }
}