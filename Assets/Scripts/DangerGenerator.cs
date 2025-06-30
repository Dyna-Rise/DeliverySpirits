using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DangerGenerator : MonoBehaviour
{
    const float Lanewidth = 3.0f; //レーン幅

    public GameObject dangerPrefab; //生成される危険車

    public bool isRandom; //インターバルをランダムにするかどうか
    public float intervalTime = 10.0f; //インターバルをランダムにしなかった場合
    public float minIntervalTime = 10.0f; //ランダムにした時の最小
    public float maxIntervalTime = 20.0f; //ランダムにした時の最大

    float timer; //時間経過を観測
    float posX; //危険車の出現X座標

    // Start is called before the first frame update
    void Start()
    {
        timer = intervalTime; //一定間隔の時間を代入
        if (isRandom) //ランダムだった場合書き換え
        {
            timer = Random.Range(minIntervalTime,maxIntervalTime + 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.gameState != GameState.playing) return; //ステータスがplayingでなければ何もしない

        timer -= Time.deltaTime; //カウントダウン
        if(timer <= 0){ //0になったら
            DangerCreated(); //危険車の生成
            timer = intervalTime; //一定間隔の時間を代入
            if (isRandom)//ランダムだった場合書き換え
            {
                timer = Random.Range(minIntervalTime, maxIntervalTime + 1);
            }
        }
    }

    //危険車の生成メソッド
    void DangerCreated()
    {
        int rand = Random.Range(-1,2);//レーン番号をランダムに取得
        posX = rand * Lanewidth; //レーン番号とレーン幅で座標を決める

        //プレハブ化した危険車を、ジェネレーターのその時のZの位置に、危険車の向きそのままに生成する
        Instantiate(dangerPrefab,new Vector3(posX,1,transform.position.z), dangerPrefab.transform.rotation);
    }
}
