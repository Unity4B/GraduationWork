using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 11/21 12:50 更新
//-------------------------------------------------------------------------------
/// <summary>
/// キーボード入力
/// </summary>
public class InputKeyBoard : InputBase
{
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
		//下入力なら-1
		if (DownKeyOn()) { return -1.0f; }
		//上入力なら+1
		else if (UpKeyOn()) { return 1.0f; }
		else { return 0.0f; }
	}

	public override float AxisHorizontal()
	{
		//左入力なら-1
		if (LeftKeyOn()) { return -1.0f; }
		//右入力なら+1
		else if (RightKeyOn()) { return 1.0f; }
		else { return 0.0f; }
	}
}
