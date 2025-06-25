using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const int MinLane = -2;
    const int MaxLane = 2;
    const float LaneWidth = 1.0f;

    CharacterController controller;
    //Animator animator;

    Vector3 moveDirection = Vector3.zero;
    int targetLane;

    public float gravity;

    public float speedZ; //前進方向のスピードの上限値
    public float accelerationZ; //加速度

    public float speedX;
    public float speedJump;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) MoveToLeft();
        if (Input.GetKeyDown(KeyCode.D)) MoveToRight();
        //if (Input.GetKeyDown("space")) Jump();

        //徐々に加速しZ方向に常に前進させる
        float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
        moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);

        //X方向は目標のポジションまでの差分の割合で速度を計算
        float ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth;
        moveDirection.x = ratioX * speedX;

        //重力分の力をフレーム追加
        moveDirection.y -= gravity * Time.deltaTime;

        //移動実行
        Vector3 globalDirection = transform.TransformDirection(moveDirection);
        controller.Move(globalDirection * Time.deltaTime);
        //controller.Move(moveDirection * Time.deltaTime); //前方向に前進するだけならこれで良し

        //移動後接地してたらY方向の速度はリセットする
        if (controller.isGrounded) moveDirection.y = 0;

        //速度が0以上なら走っているフラグをtrueにする
        //animator.SetBool("run", moveDirection.z > 0.0f);

    }

    //左のレーンに移動を開始
    public void MoveToLeft()
    {
        if (controller.isGrounded && targetLane > MinLane) 
            targetLane--;
    }

    //⇒のレーンに移動を開始
    public void MoveToRight()
    {
        if (controller.isGrounded && targetLane < MaxLane)
            targetLane++;
    }

    public void Jump()
    {
        if(controller.isGrounded)
        {
            moveDirection.y = speedJump;

            //ジャンプトリガーを設定
             
        }
    }


}
