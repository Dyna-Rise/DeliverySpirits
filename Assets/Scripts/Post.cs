using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PostType
{
    box1,
    box2,
    box3
}

public class Post : MonoBehaviour
{
    public PostType type; //自作した列挙型を扱う変数　自分のタイプを決める
    bool posted; //配達済みかどうかフラグ

    public int getPoint = 50; //ポイント

    private void OnTriggerEnter(Collider other)
    {
        if (!posted)
        {
            switch (type)
            {
                case PostType.box1:
                    if (other.gameObject.CompareTag("Box1"))
                        //宅配完了の処理
                        PostComp();
                        break;
                case PostType.box2:
                    if (other.gameObject.CompareTag("Box2"))
                        //宅配完了の処理
                        PostComp();
                        break;
                case PostType.box3:
                    if (other.gameObject.CompareTag("Box3"))
                        //宅配完了の処理
                        PostComp();
                        break;
            }                        
        }
    }

    void PostComp()
    {
        posted = true;

        GameController.stagePoints += getPoint; //ステージポイント加算

        Destroy(transform.parent.gameObject, 1.0f);
    }
}
