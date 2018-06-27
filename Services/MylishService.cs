namespace Mylish.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using Dapper;
    using Mylish.Models;
    using SQL = Mylish.SQL.MylishSQL;

    /// <summary>
    /// Mylishサービス
    /// </summary>
    public class MylishService : IMylishService
    {
        /// <summary>DBコネクション</summary>
        private IDbConnection cnn;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="cnn">DBコネクション</param>
        public MylishService(IDbConnection cnn)
        {
            this.cnn = cnn;
        }

        /// <summary>
        /// 例文の取得
        /// </summary>
        /// <param name="no">番号</param>
        public Sentence GetSentence(int no)
        {
            return cnn.Query<Sentence>(SQL.SELECT_SENTENCE, new { no = no }).SingleOrDefault();
        }

        /// <summary>
        /// 例文の取得(空)
        /// </summary>
        public Sentence EmptySentence()
        {
            return Sentence.Empty();
        }

        /// <summary>
        /// スタート
        /// </summary>
        public string Start()
        {
            var id = Score.GenerateID();
            cnn.Execute(SQL.INSERT_SCORE, new { id = id, date = DateTime.Now });
            return id;
        }

        /// <summary>
        /// 解答の判定
        /// </summary>
        /// <param name="id">成績ID</param>
        /// <param name="no">例文番号</param>
        /// <param name="answer">解答</param>
        /// <returns>true:成功/false:失敗</returns>
        public void Submit(string id, int no, string answer)
        {
            var sentence = cnn.Query<Sentence>(SQL.SELECT_SENTENCE, new { no = no }).FirstOrDefault();

            var seq = cnn.Query<int>(SQL.SELECT_SEQ, new { score_id = id }).First();
            var result = (sentence != null && sentence.En == answer) ? 1 : 0;

            cnn.Execute(SQL.INSERT_PROBLEM, new {
                score_id  = id,
                sentence_no = no,
                seq         = seq,
                result      = result,
                answer      = answer
            });
        }

        /// <summary>
        /// 次の例文番号の取得
        /// </summary>
        /// <param name="id">成績ID</param>
        public int NextSentenceNo(string id)
        {
            var numbers = cnn.Query<int>(SQL.SELECT_NEXT_SENTENCE, new { score_id = id });
            return numbers.OrderBy(i => Guid.NewGuid()).FirstOrDefault();
        }

        /// <summary>
        /// 成績の取得
        /// </summary>
        /// <param name="id">成績ID</param>
        public Score GetScore(string id)
        {
            return cnn.Query<Score>(SQL.SELECT_SCORE, new { id = id }).FirstOrDefault();
        }

        /// <summary>
        /// 成績一覧の取得
        /// </summary>
        public IEnumerable<Score> GetScores()
        {
            return cnn.Query<Score>(SQL.SELECT_SCORES);
        }

        /// <summary>
        /// 成績の更新
        /// </summary>
        /// <param name="id">成績ID</param>
        public void Update(string id)
        {
            cnn.Execute(SQL.UPDATE_SCORE, new { id = id });
        }

        /// <summary>
        /// 成績の削除
        /// </summary>
        /// <param name="id">成績ID</param>
        public void Delete(string id)
        {
            cnn.Execute(SQL.DELETE_SCORE, new { id = id });
        }

        /// <summary>
        /// 結果一覧の取得
        /// </summary>
        /// <param name="id">成績ID</param>
        public IEnumerable<Result> GetResult(string id)
        {
            return cnn.Query<Result>(SQL.SELECT_RESULTS, new { score_id = id });
        }
    }
}