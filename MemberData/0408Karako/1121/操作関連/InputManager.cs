using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 入力管理
/// </summary>
public class InputManager : SingletonClass<InputManager>
{
    /// <summary>
    /// 入力機器
    /// </summary>
    public InputBase input;

	protected override void Awake()
	{
		base.Awake();
		this.input = new InputKeyBoard();
	}
}
