using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    //--------------------------------------------------------------------
    [SerializeField] private GameObject player = default;    //プレイヤ
    [SerializeField] private Vector2[] CamPos = default;     //カメラ位置y座標を指定
    private float dif;                             //カメラとのy座標差
    private int camNum;                            //配列の中身
    //初期化
    //--------------------------------------------------------------------
    void Start()
    {
        camNum = 0;
    }
    //--------------------------------------------------------------------
    void Update()
    {
        dif = this.transform.position.y - player.transform.position.y;
        //Debug.Log(dif);
        //--------------------------------
        //下へ
        if (dif > 8)
        {
            camNum = camNum - 1;
            transform.position = new Vector3(0, CamPos[camNum].y, -10);
        }
        //--------------------------------
        //上へ
        if (dif < -8)
        {
            camNum = camNum + 1;
            transform.position = new Vector3(0, CamPos[camNum].y, -10);
        }
    }
    //--------------------------------------------------------------------
}
