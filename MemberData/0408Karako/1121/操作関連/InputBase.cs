using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 11/21 12:50 更新
//-------------------------------------------------------------------------------
/// <summary>
/// 入力基礎クラス
/// </summary>
public abstract class InputBase
{
	//-------------------------------------------------------------------------------
	/// <summary>
	/// アクションボタンを押した瞬間
	/// </summary>
	/// <returns></returns>
	abstract public bool ActionKeyDown();
	//-------------------------------------------------------------------------------
	/// <summary>
	/// アクションボタンを押している瞬間
	/// </summary>
	/// <returns></returns>
	abstract public bool ActionKeyOn();
	//-------------------------------------------------------------------------------
	/// <summary>
	/// アクションボタンを離した瞬間
	/// </summary>
	/// <returns></returns>
	abstract public bool ActionKeyUp();
	//-------------------------------------------------------------------------------
	/// <summary>
	/// メニュー起動ボタン
	/// </summary>
	/// <returns></returns>
	abstract public bool MenuKeyDown();
	//-------------------------------------------------------------------------------
	/// <summary>
	/// キャンセルボタン
	/// </summary>
	/// <returns></returns>
	abstract public bool CanselKeyDown();
	//-------------------------------------------------------------------------------
	/// <summary>
	/// 上入力を押した瞬間
	/// </summary>
	/// <returns></returns>
	abstract public bool UpKeyDown();
	//-------------------------------------------------------------------------------
	/// <summary>
	/// 上入力を押している間
	/// </summary>
	/// <returns></returns>
	abstract public bool UpKeyOn();
	//-------------------------------------------------------------------------------
	/// <summary>
	/// 上入力を離した瞬間
	/// </summary>
	/// <returns></returns>
	abstract public bool UpKeyUp();
	//-------------------------------------------------------------------------------
	/// <summary>
	/// 下入力を押した瞬間
	/// </summary>
	/// <returns></returns>
	abstract public bool DownKeyDown();
	//-------------------------------------------------------------------------------
	/// <summary>
	/// 下入力を押している間
	/// </summary>
	/// <returns></returns>
	abstract public bool DownKeyOn();
	//-------------------------------------------------------------------------------
	/// <summary>
	/// 下入力を離した瞬間
	/// </summary>
	/// <returns></returns>
	abstract public bool DownKeyUp();
	//-------------------------------------------------------------------------------
	/// <summary>
	/// 右入力を押した瞬間
	/// </summary>
	/// <returns></returns>
	abstract public bool RightKeyDown();
	//-------------------------------------------------------------------------------
	/// <summary>
	/// 右入力を押している間
	/// </summary>
	/// <returns></returns>
	abstract public bool RightKeyOn();
	//-------------------------------------------------------------------------------
	/// <summary>
	/// 右入力を離した瞬間
	/// </summary>
	/// <returns></returns>
	abstract public bool RightKeyUp();
	//-------------------------------------------------------------------------------
	/// <summary>
	/// 左入力を押した瞬間
	/// </summary>
	/// <returns></returns>
	abstract public bool LeftKeyDown();
	//-------------------------------------------------------------------------------
	/// <summary>
	/// 左入力を押している間
	/// </summary>
	/// <returns></returns>
	abstract public bool LeftKeyOn();
	//-------------------------------------------------------------------------------
	/// <summary>
	/// 左入力を離した瞬間
	/// </summary>
	/// <returns></returns>
	abstract public bool LeftKeyUp();
	//-------------------------------------------------------------------------------
	/// <summary>
	/// 垂直方向の入力値
	/// </summary>
	/// <returns></returns>
	abstract public float AxisVertical();
	//-------------------------------------------------------------------------------
	/// <summary>
	/// 水平方向の入力値
	/// </summary>
	/// <returns></returns>
	abstract public float AxisHorizontal();
	//-------------------------------------------------------------------------------
	/// <summary>
	/// ジャンプ入力
	/// </summary>
	/// <returns></returns>
	abstract public bool JumpKeyDown();
	//-------------------------------------------------------------------------------
}
