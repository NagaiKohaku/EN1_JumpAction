using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowDraw : MonoBehaviour
{
    //クリックした座標
    private Vector3 clickPosition;

    [SerializeField]
    //矢印の画像
    private Image arrowImage;
    [SerializeField]
    private GameObject player;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //右クリックしたとき

            //クリックした座標を取得
            clickPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            //右クリックを押しているとき

            //クリックした座標と現在のマウス座標の差分ベクトルをとる
            Vector3 dist = clickPosition - Input.mousePosition;

            //ベクトルの長さを取得
            float size = dist.magnitude;

            //ベクトルから角度(弧度法)を取得
            float angleRad = Mathf.Atan2(dist.y, dist.x);

            //矢印の画像を表示させる
            arrowImage.enabled = true;

            //矢印の画像をスクリーン座標に変換したプレイヤーの座標に移動させる
            arrowImage.rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main,player.transform.position);

            //矢印の画像をベクトルから取得した角度を度数法に変換してZ軸回転させる
            arrowImage.rectTransform.rotation =
                Quaternion.Euler(0, 0, angleRad * Mathf.Rad2Deg);

            //矢印の画像の大きさをドラッグしたきりに合わせる
            arrowImage.rectTransform.sizeDelta = new Vector2(size, size);
        }

        if(Input.GetMouseButtonUp(0))
        {
            //右クリックを離したとき

            //矢印の画像を非表示にする
            arrowImage.enabled = false;
        }
    }
}
