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
