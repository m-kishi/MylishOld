import Vue from 'vue';
import { Component } from 'vue-property-decorator';

//==================================================
// api/GetSentence のレスポンス
//==================================================
class SentenceJson {
    No: number = 0;
    En: string = "";
    Ja: string = "";
}

//==================================================
// 解答ページ
//==================================================
@Component
export default class QuestComponent extends Vue {

    // 画面初期化フラグ
    loaded: boolean = false;

    // 成績ID
    score_id: string = "";

    // 例文データ
    sentence: SentenceJson = new SentenceJson();

    // 解答
    answer: string = "";

    // 解答結果
    // 0:未解答 1:正解 -1:不正解
    result: number = 0;

    // 解答件数
    counter: number = 0;

    //==================================================
    // 画面ロード時のイベント
    //==================================================
    mounted() {
        fetch("api/Start/", {
            method: 'POST'
        })
            .then(response =>
                response.text()
            )
            .then(id => {
                this.score_id = id;
            })
            .then(
                this.fetch_problem
            );
    }

    //==================================================
    // 例文データの取得
    //==================================================
    fetch_problem() {
        this.loaded = false;
        fetch("api/GetSentence/" + this.score_id)
            .then(response =>
                response.json() as Promise<SentenceJson>
            )
            .then(sentence => {
                this.result = 0;
                this.answer = "";
                this.counter += 1;
                this.sentence = sentence;
            })
            .then(_ => {
                // 次の問題がないなら終了
                if (this.sentence.No == 0) {
                    this.exit();
                }
            })
            .then(_ => {
                if (this.sentence.No != 0) {
                    this.loaded = true;
                }
            })
            .then(_ => {
                if (this.sentence.No != 0) {
                    (<HTMLInputElement>this.$refs.input_answer).focus();
                }
            });
    }

    //==================================================
    // 解答の送信
    //==================================================
    submit() {
        fetch("api/Submit/" + this.score_id, {
            method: 'POST',
            body: JSON.stringify({ No: this.sentence.No, Answer: this.answer }),
            headers: new Headers({ "Content-type": "application/json" }),
        })
            .then(response => {
                var en = this.sentence.En;
                if (this.answer == en) {
                    this.result = 1;
                } else {
                    this.result = -1;
                }
                // $nextTickでDOMの更新後にフォーカスを移動させる
                this.$nextTick(() => {
                    (<HTMLInputElement>this.$refs.btn_next).focus();
                });
            });
    }

    //==================================================
    // 次の出題
    //==================================================
    next() {
        this.fetch_problem();
    }

    //==================================================
    // 結果画面へ遷移
    //==================================================
    exit() {
        this.$router.push({ path: "/score/" + this.score_id });
    }
}