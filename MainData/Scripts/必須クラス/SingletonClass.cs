using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//---------------------------------------------
/// <summary>
/// シングルトンクラス
/// </summary>
public abstract class SingletonClass<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// 生成されたクラス
    /// </summary>
    private static T instance;

    //プロパティ
    /// <summary>
    /// 生成されたクラス
    /// </summary>
    public static T Instance
    {
        //戻り値
        get
        {
            if (instance == null)
            {
                Type t = typeof(T);

                instance = (T)FindObjectOfType(t);

                if (instance == null)
                {
                    Debug.LogError(t.ToString() + "をアタッチしているGameObjectはありません");
                }
            }

            return instance;
        }
    }
    //---------------------------------------------
    virtual protected void Awake()
    {
        //他のGameObjectにアタッチされているか調べる
        //アタッチされている場合
        if(CheckInstance())
        {
            //アタッチされているのが自分以外なら、自分を破壊
            if(instance != this) { Destroy(this); }

        }
        //アタッチされていない場合
        else
        {
            //生成
            instance = this as T;

            DontDestroyOnLoad(gameObject);
        }
    }
    //---------------------------------------------
    /// <summary>
    /// 他のGameObjectにアタッチされているか調べる
    /// </summary>
    /// <returns></returns>
    protected bool CheckInstance()
    {
        if (instance == null)   { return false; }
        else                    { return true; }
    }
    //---------------------------------------------
}
