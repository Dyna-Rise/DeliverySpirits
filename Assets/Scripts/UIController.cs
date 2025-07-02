using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public TimeController timeCnt; //TimeControllerスクリプトの情報
    public TextMeshProUGUI timeText; //TimeTextオブジェクトについているTMPのコンポーネントの情報

    public GameObject gameOverPanel; //ゲームオーバーUIを参照

    //ステージポイント関連
    int currentPoint; //UIが管理しているポイント
    public TextMeshProUGUI pointText;

    //宅配BOXのUI関連
    public Image boxImage; //対象コンポーネント
    public Sprite[] boxPics; //BOXの絵
    int currentBoxNum; //UIが把握している宅配BOX番号

    public string currentStageName; //ステージ名を入力

    public TextMeshProUGUI thisScoreText; //現在スコアのUI
    public TextMeshProUGUI HighScoreText; //ハイスコアのUI
    public TextMeshProUGUI[] boxTexts; //ボックスの個別成績の文字列の配列

    public GameObject resultPanel; //リザルトパネル




    // Start is called before the first frame update
    void Start()
    {
        timeCnt = GetComponent<TimeController>();
    }

    // Update is called once per frame
    void Update()
    {
        //右クリックして選択したBOX番号がUIの把握しているBOX番号と違うなら
        if(currentBoxNum != Shooter.boxNum)
        {
            currentBoxNum = Shooter.boxNum; //最新のBOX番号に更新
            //UIが把握している最新のBOX番号に対応した絵を
            //対象のImageコンポーネントのspriteに代入
            boxImage.sprite = boxPics[currentBoxNum];
        }

        //ステージポイントがUIの把握しているポイントと違うなら
        if(currentPoint != GameController.stagePoints)
        {
            //UIの把握しているポイントに最新情報を反映
            currentPoint = GameController.stagePoints;
            //int型を文字列型に変換して目的のtext欄に代入
            pointText.text = currentPoint.ToString();
        }

        //プレイ状態ならタイムカウントする
        if(GameController.gameState == GameState.playing)
        {
            //切り上げたdisplayTimeをstringに変換してtextに差し替え
            timeText.text = Mathf.Ceil(timeCnt.displayTime).ToString();
            
            if(timeText.text == "0")
            {
                //ゲームステータスをtimeoverに
                GameController.gameState = GameState.timeover;
                //過去のハイスコアを取得
                int highScore = PlayerPrefs.GetInt(currentStageName);

                //もし現在スコアが過去スコアを上回ったら
                if(GameController.stagePoints > highScore)
                {
                    highScore = GameController.stagePoints;
                    PlayerPrefs.SetInt(currentStageName, highScore);
                }

                //ThisTimesScoreへの表示
                thisScoreText.text = "This Time's Score：" + currentPoint.ToString();

                //HighScoreへの表示
                HighScoreText.text = "Hig Score：" + highScore.ToString();

                //各ボックスの成功率を表示
                for(int i = 0; i < boxTexts.Length; i++)
                {
                    float successRate;
                    //分母が0だと計算出来ないので成功率は強制0
                    if (Shooter.shootCounts[i] == 0) successRate = 0;
                    else
                    {
                        //Rateの計算 ※分子・分母両方ともint
                        //どちらか片方をfloatにキャストしておけば成立
                        successRate = ((float)Post.successCounts[i] / Shooter.shootCounts[i]) * 100f;
                    }

                    boxTexts[i].text = "Box" + (i+1) + " " + Post.successCounts[i] + "/" + Shooter.shootCounts[i] + " success rate "+ successRate.ToString("F1") + "%";
                }

                //リザルトパネルを出す
                resultPanel.SetActive(true);
                //カーソルの復活
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                //ゲーム状態をendに
                GameController.gameState = GameState.end;
            }

        }

        //ゲームオーバーになったら
        if (GameController.gameState == GameState.gameover)
        {
            //ゲームオーバーパネルを表示
            gameOverPanel.SetActive(true);

            //カーソルの復活
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            //ゲーム状態をendに
            GameController.gameState = GameState.end;
        }
    }
}
