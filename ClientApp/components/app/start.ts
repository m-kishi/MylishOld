import Vue from 'vue';
import { Component } from 'vue-property-decorator';

//==================================================
// api/GetScores のレスポンス
//==================================================
interface ScoreJson {
    Id: string;
    Date: Date;
    Answered: number;
    Corrected: number;
}

//==================================================
// トップページ
//==================================================
@Component
export default class StartComponent extends Vue {

    // 画面初期化フラグ
    loaded: boolean = false;

    // ページ番号
    page_no: number = 0;

    // 1ページに表示する成績数
    page_unit: number = 5;

    // 成績データ
    scores: ScoreJson[] = [];

    //==================================================
    // 画面ロード時のイベント
    //==================================================
    mounted() {
        this.page_no = 1;
        this.get_scores();
    }

    //==================================================
    // 成績データの取得
    //==================================================
    get_scores() {
        fetch('api/GetScores')
            .then(response =>
                response.json() as Promise<ScoreJson[]>
            )
            .then(scores => {
                this.scores = scores;
                if (this.page_no > this.page_max) {
                    this.page_no = this.page_max;
                }
            })
            .then(_ => {
                this.loaded = true;
            });
    }

    //==================================================
    // 成績データの削除
    //==================================================
    delete_score(id: string) {
        fetch("api/Delete/" + id, {
            method: 'POST',
        })
            .then(response => {
            })
            .then(
                this.get_scores
            );
    }

    //==================================================
    // 成績データの正答率
    //==================================================
    rate(score: ScoreJson): string {
        var rate = 0.0;
        if (score.Answered > 0) {
            rate = (score.Corrected / score.Answered) * 100;
        }
        return rate.toFixed(1);
    }

    //==================================================
    // 先頭ページへ
    //==================================================
    head_page() {
        if (this.is_prev_disabled) {
            return;
        }
        this.page_no = 1;
    }

    //==================================================
    // 前ページへ
    //==================================================
    prev_page() {
        if (this.is_prev_disabled) {
            return;
        }
        this.page_no -= 1;
    }

    //==================================================
    // 次ページへ
    //==================================================
    next_page() {
        if (this.is_next_disabled) {
            return;
        }
        this.page_no += 1;
    }

    //==================================================
    // 最終ページへ
    //==================================================
    tail_page() {
        if (this.is_next_disabled) {
            return;
        }
        this.page_no = this.page_max;
    }

    //==================================================
    // 最大ページ数
    //==================================================
    get page_max(): number {
        return Math.ceil(this.scores.length / this.page_unit);
    }

    //==================================================
    // headとprevの無効化
    //==================================================
    get is_prev_disabled(): boolean {
        return this.page_no <= 1;
    }

    //==================================================
    // nextとtailの無効化
    //==================================================
    get is_next_disabled(): boolean {
        return this.page_no >= this.page_max;
    }

    //==================================================
    // ページ切り替え
    //==================================================
    get pagination(): ScoreJson[] {
        var str = (this.page_no - 1) * this.page_unit;
        var end = str + this.page_unit;
        if (end > this.scores.length) {
            end = this.scores.length;
        }
        return this.scores.slice(str, end);
    }
}