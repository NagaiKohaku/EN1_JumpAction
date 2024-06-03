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
        if (isCanJump && Input.GetMouseButtonUp(0))
        {
            //クリックした座標と現在のマウス座標の差分ベクトルを取得
            Vector3 dist = clickPosition - Input.mousePosition;

            //マウスを動かしていなかったら無視
            if (dist.sqrMagnitude == 0) { return; }

            //差分を標準化し、jumpPowerを掛け合わせて移動量とする
            rb.velocity = dist.normalized * jumpPower;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //衝突している点の情報を取得
        ContactPoint[] contacts = collision.contacts;

        //0番目の衝突情報から、衝突している点の法線を取得
        Vector3 otherNormal = contacts[0].normal;

        //上方向を示すベクトル
        Vector3 upVector = new Vector3(0, 1, 0);

        //上方向と法線の内積を計算
        float dotUN = Vector3.Dot(upVector, otherNormal);

        //内積値に逆三角関数を掛けて角度を計算。それを度数法へ変換する
        float dotDeg = Mathf.Acos(dotUN) * Mathf.Rad2Deg;

        //角度が45度より小さければジャンプを可能とする
        if (dotDeg <= 45)
        {
            isCanJump = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isCanJump = false;
    }
}