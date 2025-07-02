using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //切替先のシーン名
    public string sceneName;

    public void Load()
    {
        //ポイントのリセットをしておく
        GameController.stagePoints = 0;

        //シーンの切り替え
        SceneManager.LoadScene(sceneName);
    }
}
