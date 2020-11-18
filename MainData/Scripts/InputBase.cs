using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBase : MonoBehaviour
{
	//アクションボタンを押した瞬間
	public virtual bool ActionKeyDown()
	{
		return false;
	}

	//アクションボタンを押している瞬間
	public virtual bool ActionKeyOn()
	{
		return false;
	}

	//アクションボタンを離した瞬間
	public virtual bool ActionKeyUp()
	{
		return false;
	}

	//メニュー起動ボタン
	public virtual bool MenuKeyDown()
	{
		return false;
	}

	//キャンセルボタン
	public virtual bool CanselKeyDown()
	{
		return false;
	}
}
