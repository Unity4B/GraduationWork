using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-------------------------------------------------------------------------------
/// <summary>
/// 音源管理クラス
/// </summary>
public class SoundManager : SingletonClass<SoundManager>
{
	//-------------------------------------------------------------------------------
	/// <summary>
	/// 登録したい音源名
	/// </summary>
	[SerializeField] string[] soundNames = null;
	/// <summary>
	/// 音源リスト
	/// </summary>
	public Dictionary<string, AudioClip> soundList;
	//-------------------------------------------------------------------------------
	protected override void Awake()
	{
		base.Awake();
		//破棄処理が呼ばれている場合、処理終了
		if (this.isDestroy) { return; }

		DontDestroyOnLoad(gameObject);

		this.soundList = new Dictionary<string, AudioClip>();

		//リソースから読み込む		
		foreach(string sName in this.soundNames)
		{
			this.soundList[sName] = FileLoad.SoundDataLoadResource(sName);
		}
	}
	//-------------------------------------------------------------------------------
}
