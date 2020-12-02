﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 12/01 23:14 更新
//-------------------------------------------------------------------------------
/// <summary>
/// キーボード入力
/// </summary>
public class InputKeyBoard : InputBase
{
	//-------------------------------------------------------------------------------
	/// <summary>
	/// 縦の入力値
	/// </summary>
	float vertical;
	/// <summary>
	/// 横の入力値
	/// </summary>
	float horizontal;
	//-------------------------------------------------------------------------------
	public override bool ActionKeyDown()
	{
		return Input.GetKeyDown(KeyCode.Return);
	}

	public override bool ActionKeyOn()
	{
		return Input.GetKey(KeyCode.Return);
	}

	public override bool ActionKeyUp()
	{
		return Input.GetKeyUp(KeyCode.Return);
	}

	public override bool CanselKeyDown()
	{
		return Input.GetKeyDown(KeyCode.Escape);
	}

	public override bool MenuKeyDown()
	{
		return false;
	}

	public override bool JumpKeyDown()
	{
		return Input.GetKeyDown(KeyCode.Space);
	}

	public override bool DownKeyDown()
	{
		return Input.GetKeyDown(KeyCode.S);
	}

	public override bool DownKeyOn()
	{
		return Input.GetKey(KeyCode.S);
	}

	public override bool DownKeyUp()
	{
		return Input.GetKeyUp(KeyCode.S);
	}


	public override bool LeftKeyDown()
	{
		return Input.GetKeyDown(KeyCode.A);
	}

	public override bool LeftKeyOn()
	{
		return Input.GetKey(KeyCode.A);
	}

	public override bool LeftKeyUp()
	{
		return Input.GetKeyUp(KeyCode.A);
	}

	public override bool RightKeyDown()
	{
		return Input.GetKeyDown(KeyCode.D);
	}

	public override bool RightKeyOn()
	{
		return Input.GetKey(KeyCode.D);
	}

	public override bool RightKeyUp()
	{
		return Input.GetKeyUp(KeyCode.D);
	}

	public override bool UpKeyDown()
	{
		return Input.GetKeyDown(KeyCode.W);
	}

	public override bool UpKeyOn()
	{
		return Input.GetKey(KeyCode.W);
	}

	public override bool UpKeyUp()
	{
		return Input.GetKeyUp(KeyCode.W);
	}

	public override float AxisVertical()
	{
		return Input.GetAxis("Vertical");
	}

	public override float AxisHorizontal()
	{
		return Input.GetAxis("Horizontal");
	}
	//伊東　12/2　追加
	public override float CurveHorizontal()
	{
		return Input.GetAxisRaw("Horizontal");
	}

	public override bool ExitKeyDown()
	{
		return Input.GetKeyDown(KeyCode.Escape);
	}
}
