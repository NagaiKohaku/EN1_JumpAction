using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PullingJump : MonoBehaviour
{
    //リジッドボディ
    private Rigidbody rb;

    //クリックした座標
    private Vector3 clickPosition;

    //ジャンプ可能状態
    private bool isCanJump;

    [SerializeField]
    //ジャンプ力
    private float jumpPower = 10;

    [SerializeField]
    //重力
    private float gravity = -9.8f;

    void Start()
    {
        //リジッドボディコンポーネントの取得
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //重力を変更する
        Physics.gravity = new Vector3(0, gravity, 0);

        if (Input.GetMouseButtonDown(0))
        {
            //クリックした座標を取得
            clickPosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            //クリックした座標と現在のマウス座標の差分ベクトルを取得
            Vector3 dist = clickPosition - Input.mousePosition;

            //マウスを動かしていなかったら無視
            if(dist.sqrMagnitude == 0 ) { return; }

            //差分を標準化し、jumpPowerを掛け合わせて移動量とする
            rb.velocity = dist.normalized * jumpPower;
        }
    }
}
