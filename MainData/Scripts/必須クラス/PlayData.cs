using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---------------------------------------------
/// <summary>
/// プレイデータ
/// </summary>
public class PlayData : SingletonClass<PlayData>
{
    //---------------------------------------------
    /// <summary>
    /// アイテムを持てる量
    /// </summary>
    [Header("持てるアイテムの量"),SerializeField] int hasItemValue = 0;
    /// <summary>
    /// 所持アイテムの番号
    /// </summary>
    public int[] itemNums;
    /// <summary>
    /// 所持金
    /// </summary>
    public int money;
    /// <summary>
    /// アイテムをゲットしたことがあるか
    /// </summary>
    public bool[] isGotItems;
    //---------------------------------------------
    void Start()
    {
        //ItemManagerのAwake後に呼び出さないとエラー?

        //初期化
        this.itemNums = new int[this.hasItemValue];
        this.money = 0;
        //アイテムの数だけ生成
        this.isGotItems = new bool[ItemManager.Instance.itemList.Count];


        //セーブデータ読み込み
    }

}
