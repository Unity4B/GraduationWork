using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-------------------------------------------------------------------------------
/// <summary>
/// SE
/// </summary>
public class SEController : SoundController<SEController>
{
	//-------------------------------------------------------------------------------
	protected override void Start()
	{
		base.Start();
		this.audios.loop = false;
	}
	//-------------------------------------------------------------------------------
	public override void Play(string sName)
	{
		//再生
		this.audios.PlayOneShot(SoundManager.Instance.soundList[sName]);
	}
	public override void Play(string sName, float volume_)
	{
		//音量設定
		SetVolume(volume_);

		//再生
		this.audios.PlayOneShot(SoundManager.Instance.soundList[sName]);

	}
	//-------------------------------------------------------------------------------
}
