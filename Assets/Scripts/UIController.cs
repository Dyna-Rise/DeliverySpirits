using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TimeController timeCnt; //TimeControllerスクリプトの情報
    public TextMeshProUGUI timeText; //TimeTextオブジェクトについているTMPのコンポーネントの情報

    public GameObject gameOverPanel; //ゲームオーバーUIを参照

    //ステージポイント関連
    int currentPoint; //UIが管理しているポイント
    public TextMeshProUGUI pointText;

    // Start is called before the first frame update
    void Start()
    {
        timeCnt = GetComponent<TimeController>();
    }

    // Update is called once per frame
    void Update()
    {
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
