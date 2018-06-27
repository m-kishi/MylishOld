import Vue from 'vue';
import { Component } from 'vue-property-decorator';

//==================================================
// api/GetScore のレスポンス
//==================================================
class ScoreJson {
    Id: string = "";
    Date: Date = new Date();
    Answered: number = 0;
    Corrected: number = 0;
}

//==================================================
// api/GetResult のレスポンス
//==================================================
interface ResultJson {
    ScoreId: string;
    SentenceNo: number;
    Seq: number;
    Result: number;
    Answer: string;
    Sentence: string;
}

//==================================================
// 結果画面
//==================================================
@Component
export default class ScoreComponent extends Vue {

    // 画面初期化フラグ
    loaded: boolean = false;

    // 成績ID
    score_id: string = "";

    // 成績データ
    score: ScoreJson = new ScoreJson();

    // 結果データ
    results: ResultJson[] = [];

    //==================================================
    // 画面ロード時のイベント
    //==================================================
    mounted() {
        this.score_id = this.$route.params.score_id;
        this.get_score();
    }

    //==================================================
    // 成績データの取得
    //==================================================
    get_score() {
        fetch("api/GetScore/" + this.score_id)
            .then(response =>
                response.json() as Promise<ScoreJson>
            )
            .then(score => {
                this.score = score;
            })
            .then(
                this.get_results
            );
    }

    //==================================================
    // 結果データの取得
    //==================================================
    get_results() {
        fetch("api/GetResult/" + this.score_id)
            .then(response =>
                response.json() as Promise<ResultJson[]>
            )
            .then(results => {
                this.results = results;
            })
            .then(_ => {
                this.loaded = true;
            });
    }

    //==================================================
    // 成績データの正答率
    //==================================================
    get rate(): string {
        var rate = 0.0;
        if (this.score.Answered > 0) {
            rate = (this.score.Corrected / this.score.Answered) * 100;
        }
        return rate.toFixed(1);
    }
}