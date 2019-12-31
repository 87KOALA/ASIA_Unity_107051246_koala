using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragon : MonoBehaviour
{
    [Header("移動速度")]
    [Range(1, 2000)]
    public int speed = 10;            
    [Header("旋轉速度"), Tooltip("Dragon的旋轉速度"), Range(1.5f, 200f)]
    public float turn = 100f;         
    [Header("是否完成任務")]
    public bool mission;               
    [Header("玩家名稱")]
    public string _name = "dragon";      
    public Transform tran;  //跑步
    public Rigidbody rig;   //旋轉
    public Animator ani;   //動畫
    [Header("檢物品位置")]
    public Rigidbody rigCatch;  



    // Update is called once per frame
    void Update()
    {
        Turn();
        Run();
        Pick();
    }

    private void OnTriggerStay(Collider other)
    {
        //碰撞物件為 gun時播放動畫pick
        if (other.name == "gun" && ani.GetCurrentAnimatorStateInfo(0).IsName("pick"))
        {
            Physics.IgnoreCollision(other, GetComponent<Collider>());   //兩物間不產生碰撞
            other.GetComponent<HingeJoint>().connectedBody = rigCatch;   
        }
    }

    private void Turn()
    {
        float h = Input.GetAxis("Horizontal");   //A D鍵控制左右
        tran.Rotate(0, turn * h * Time.deltaTime, 0);   //Y軸為旋轉軸
    }

    private void Run()   //往前飛
    {
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("pick")) return;   //撿東西時撥放動畫

        float v = Input.GetAxis("Vertical");   //W S鍵控制前後
        rig.AddForce(tran.forward * speed * v * Time.deltaTime);   

        ani.SetBool("run", v != 0);
    }

    private void Pick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ani.SetTrigger("pick");   //按下滑鼠左鍵就會觸發撿東西動畫
        }
    }
}
