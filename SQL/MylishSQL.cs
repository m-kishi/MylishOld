namespace Mylish.SQL
{
    /// <summary>
    /// SQL定義
    /// </summary>
    public static class MylishSQL
    {
        /// <summary>
        /// @no
        /// </summary>
        public static readonly string SELECT_SENTENCE = ""
        + " SELECT "
        + "     * "
        + " FROM"
        + "      sentences "
        + " WHERE"
        + "      no = @no"
        + ";";

        /// <summary>
        /// @score_id
        /// </summary>
        public static readonly string SELECT_NEXT_SENTENCE = ""
        + " SELECT "
        + "     s.no "
        + " FROM "
        + "     sentences s "
        + " WHERE "
        + "     NOT EXISTS ( "
        + "         SELECT "
        + "             1 "
        + "         FROM "
        + "             problems p "
        + "         WHERE "
        + "             p.score_id = @score_id "
        + "         AND p.sentence_no = s.no "
        + "     ) "
        + ";";

        /// <summary>
        /// @score_id
        /// </summary>
        public static readonly string SELECT_SEQ = ""
        + " SELECT "
        + "     COALESCE(MAX(seq), 0) + 1 "
        + " FROM "
        + "     problems "
        + " WHERE "
        + "     score_id = @score_id "
        + ";";

        /// <summary>
        /// @score_id
        /// @sentence_no
        /// @seq
        /// @result
        /// @answer
        /// </summary>
        public static readonly string INSERT_PROBLEM = ""
        + " INSERT INTO problems ( "
        + "     score_id "
        + "   , sentence_no "
        + "   , seq "
        + "   , result "
        + "   , answer "
        + " ) VALUES ( "
        + "     @score_id "
        + "   , @sentence_no "
        + "   , @seq "
        + "   , @result "
        + "   , @answer "
        + " );";

        /// <summary>
        /// @id
        /// </summary>
        public static readonly string SELECT_SCORE = ""
        + " SELECT "
        + "     * "
        + " FROM "
        + "     scores "
        + " WHERE "
        + "     id = @id "
        + ";";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string SELECT_SCORES = ""
        + " SELECT "
        + "     * "
        + " FROM "
        + "     scores "
        + " ORDER BY "
        + "     date DESC "
        + ";";

        /// <summary>
        /// @id
        /// @date
        /// </summary>
        public static readonly string INSERT_SCORE = ""
        + " INSERT INTO scores ( "
        + "     id "
        + "   , date "
        + "   , answered "
        + "   , corrected "
        + " ) VALUES ( "
        + "     @id "
        + "   , @date "
        + "   , 0 "
        + "   , 0 "
        + " );";

        /// <summary>
        /// @id
        /// </summary>
        public static readonly string UPDATE_SCORE = ""
        + " UPDATE scores "
        + " SET "
        + "     answered = ( "
        + "         SELECT COUNT(*) FROM problems WHERE score_id = @id "
        + "     ) "
        + "   , corrected = ( "
        + "         SELECT COUNT(*) FROM problems WHERE score_id = @id AND result = 1 "
        + "     ) "
        + " WHERE "
        + "     id = @id "
        + ";";

        /// <summary>
        /// @id
        /// </summary>
        public static readonly string DELETE_SCORE = ""
        + " DELETE FROM scores "
        + " WHERE "
        + "     id = @id "
        + ";";

        /// <summary>
        /// @score_id
        /// </summary>
        public static readonly string SELECT_RESULTS = ""
        + " SELECT "
        + "     p.score_id "
        + "   , p.sentence_no "
        + "   , p.seq "
        + "   , p.result "
        + "   , p.answer "
        + "   , s.en "
        + " FROM "
        + "     problems p "
        + "     LEFT JOIN sentences s "
        + "       ON p.sentence_no = s.no "
        + " WHERE "
        + "     score_id = @score_id "
        + " ORDER BY "
        + "     p.seq "
        + ";";
    }
}