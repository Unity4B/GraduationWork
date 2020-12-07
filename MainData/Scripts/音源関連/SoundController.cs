using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-------------------------------------------------------------------------------
/// <summary>
/// 音源再生基礎クラス
/// </summary>
public abstract class SoundController<T> : SingletonClass<T> where T: SingletonClass<T>
{
	//-------------------------------------------------------------------------------
	/// <summary>
	/// オーディオコンポーネント
	/// </summary>
	[SerializeField] protected AudioSource audios = null;
	/// <summary>
	/// 音量
	/// </summary>
	float volume;
	/// <summary>
	/// 最大音量 0.0f ～ 1.0f
	/// </summary>
	[Header("最大音量 0.0f ～ 1.0f")]
	[SerializeField] float maxVolume = 1.0f;
	/// <summary>
	/// フェードタイプ
	/// </summary>
	FadeType fType;
	/// <summary>
	/// フェードにかかる時間
	/// </summary>
	float fadeTime;
	//-------------------------------------------------------------------------------
	protected virtual void Start()
	{
		this.volume = this.maxVolume;
		this.fType = FadeType.None;
		this.fadeTime = 1.0f;

		SetVolume(this.volume);
	}

	void Update()
	{
		switch(this.fType)
		{
			case FadeType.In:	FadeIn();	break;
			case FadeType.Out:	FadeOut();	break;
		}
	}
	//-------------------------------------------------------------------------------
	/// <summary>
	/// 音源を再生する
	/// </summary>
	/// <param name="sName">再生したい音源名</param>
	public abstract void Play(string sName);
	/// <summary>
	/// 音源を再生する
	/// </summary>
	/// <param name="sName">再生したい音源名</param>
	/// <param name="volume_">ボリューム 0.0f ～ 1.0f</param>
	public abstract void Play(string sName,float volume_);
	/// <summary>
	/// フェードする音源を再生する
	/// </summary>
	/// <param name="sName">再生したい音源名</param>
	/// <param name="fType_">フェードタイプ</param>
	/// <param name="fadeTime_">フェードにかかる時間</param>
	public virtual void Play(string sName,FadeType fType_,float fadeTime_)
	{
		Play(sName);
		FadeStart(fType_, fadeTime_);
	}
	/// <summary>
	/// フェードする音源を再生する
	/// </summary>
	/// <param name="sName"></param>
	/// <param name="volume_"></param>
	/// <param name="fType_"></param>
	/// <param name="fadeTime_"></param>
	public virtual void Play(string sName, float volume_, FadeType fType_,float fadeTime_)
	{
		Play(sName, volume_);
		FadeStart(fType_, fadeTime_);
	}
	//-------------------------------------------------------------------------------
	/// <summary>
	/// 音量を設定する
	/// </summary>
	/// <param name="volume_">ボリューム 0.0f ～ 1.0f</param>
	public virtual void SetVolume(float volume_)
	{
		this.audios.volume = volume_;
	}
	//-------------------------------------------------------------------------------
	/// <summary>
	/// フェード開始
	/// </summary>
	/// <param name="fType_">フェードタイプ</param>
	/// <param name="fadeTime_">フェードにかかる時間</param>
	public void FadeStart(FadeType fType_,float fadeTime_)
	{
		this.fType = fType_;
		this.fadeTime = fadeTime_;
	}
	//-------------------------------------------------------------------------------
	/// <summary>
	/// 音のフェードイン
	/// </summary>
	public void FadeIn()
	{
		this.volume += Time.deltaTime / this.fadeTime;

		if (this.volume > this.maxVolume)
		{
			this.volume = this.maxVolume;
			//Fade終了
			this.fType = FadeType.None;
		}

		//音量セット
		SetVolume(this.volume);
	}
	//-------------------------------------------------------------------------------
	/// <summary>
	/// 音のフェードアウト
	/// </summary>
	public void FadeOut()
	{
		this.volume -= Time.deltaTime / this.fadeTime;

		if (this.volume < 0.0f) 
		{
			this.volume = 0.0f;
			//Fade終了
			this.fType = FadeType.None;
		}

		//音量セット
		SetVolume(this.volume);
	}
	//-------------------------------------------------------------------------------
}
