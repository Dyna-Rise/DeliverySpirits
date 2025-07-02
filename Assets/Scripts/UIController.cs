using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TimeController timeCnt; //TimeControllerスクリプトの情報
    public TextMeshProUGUI timeText; //TimeTextオブジェクトについているTMPのコンポーネントの情報

    public GameObject gameOverPanel; //ゲームオーバーUIを参照

    // Start is called before the first frame update
    void Start()
    {
        timeCnt = GetComponent<TimeController>();
    }

    // Update is called once per frame
    void Update()
    {
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
