namespace Mylish.Services
{
    using System.Collections.Generic;
    using Mylish.Models;

    /// <summary>
    /// Mylishサービスのインタフェース
    /// </summary>
    public interface IMylishService
    {
        /// <summary>
        /// 例文の取得
        /// </summary>
        /// <param name="no">番号</param>
        Sentence GetSentence(int no);

        /// <summary>
        /// 例文の取得(空)
        /// </summary>
        Sentence EmptySentence();

        /// <summary>
        /// スタート
        /// </summary>
        string Start();

        /// <summary>
        /// 解答の判定
        /// </summary>
        /// <param name="id">成績ID</param>
        /// <param name="no">例文番号</param>
        /// <param name="answer">解答</param>
        void Submit(string id, int no, string answer);

        /// <summary>
        /// 次の例文番号の取得
        /// </summary>
        /// <param name="id"履歴ID></param>
        int NextSentenceNo(string id);

        /// <summary>
        /// 成績の取得
        /// </summary>
        /// <param name="id">履歴ID</param>
        Score GetScore(string id);

        /// <summary>
        /// 成績一覧の取得
        /// </summary>
        IEnumerable<Score> GetScores();

        /// <summary>
        /// 成績の更新
        /// </summary>
        /// <param name="id">履歴ID</param>
        void Update(string id);

        /// <summary>
        /// 成績の削除
        /// </summary>
        /// <param name="id">履歴ID</param>
        void Delete(string id);

        /// <summary>
        /// 結果一覧の取得
        /// </summary>
        /// <param name="id"></param>
        IEnumerable<Result> GetResult(string id);
    }
}