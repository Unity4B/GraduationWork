using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-------------------------------------------------------------------------------
/// <summary>
/// BGM
/// </summary>
public class BGMController : SoundController<BGMController>
{
	//-------------------------------------------------------------------------------
	protected override void Start()
	{
		base.Start();
		//ループ音
		this.audios.loop = true;	
	}
	//-------------------------------------------------------------------------------
	public override void Play(string sName)
	{
		//前の音声をストップ
		this.audios.Stop();

		//新規に音声をセット
		this.audios.clip = SoundManager.Instance.soundList[sName];

		//再生
		this.audios.Play();
	}
	public override void Play(string sName,float volume_)
	{
		//前の音声をストップ
		this.audios.Stop();

		//新規に音声をセット
		this.audios.clip = SoundManager.Instance.soundList[sName];

		//音量設定
		SetVolume(volume_);

		//再生
		this.audios.Play();
	}
	//-------------------------------------------------------------------------------
}
