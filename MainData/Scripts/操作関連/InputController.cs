using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 12/01 23:14 更新
//-------------------------------------------------------------------------------
/// <summary>
/// コントローラー入力
/// </summary>
public class InputController : InputBase
{
	public override bool ActionKeyDown()
	{
		return false;
	}

	public override bool ActionKeyOn()
	{
		return false;
	}

	public override bool ActionKeyUp()
	{
		return false;
	}

	public override bool CanselKeyDown()
	{
		return false;
	}

	public override bool DownKeyDown()
	{
		return false;
	}

	public override bool DownKeyOn()
	{
		return false;
	}

	public override bool DownKeyUp()
	{
		return false;
	}

	public override bool JumpKeyDown()
	{
		return false;
	}

	public override bool LeftKeyDown()
	{
		return false;
	}

	public override bool LeftKeyOn()
	{
		return false;
	}

	public override bool LeftKeyUp()
	{
		return false;
	}

	public override bool MenuKeyDown()
	{
		return false;
	}

	public override bool RightKeyDown()
	{
		return false;
	}

	public override bool RightKeyOn()
	{
		return false;
	}

	public override bool RightKeyUp()
	{
		return false;
	}

	public override bool UpKeyDown()
	{
		return false;
	}

	public override bool UpKeyOn()
	{
		return false;
	}

	public override bool UpKeyUp()
	{
		return false;
	}

	public override float AxisVertical()
	{
		return 0.0f;
	}

	public override float AxisHorizontal()
	{
		return 0.0f;
	}
	//伊東　12/2　追加
	public override float CurveHorizontal()
	{
		return 0.0f;
	}

	public override bool ExitKeyDown()
	{
		return false;
	}
}
