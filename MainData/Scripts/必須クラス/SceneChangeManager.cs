using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//---------------------------------------------
/// <summary>
/// シーン変移管理
/// </summary>
public class SceneChangeManager : SingletonClass<SceneChangeManager>
{
	//---------------------------------------------
	//シーン切り替え中か
	bool isNowChange;
	/// <summary>
	/// フェード時間
	/// </summary>
	[SerializeField] float timeLimit = 1.0f;
	//---------------------------------------------
	protected override void Awake()
	{
		base.Awake();

		DontDestroyOnLoad(gameObject);

	}
	//---------------------------------------------
	/// <summary>
	/// シーン切り替え
	/// </summary>
	/// <param name="sName">次のシーン名</param>
	public void SceneChange(string sName)
	{
		//動作中なら処理せず
		if (this.isNowChange) { return; }

		StartCoroutine(SceneChangeStart(sName));
	}
	//---------------------------------------------
	IEnumerator SceneChangeStart(string sName)
	{
		this.isNowChange = true;

		//FadeOut
		FadeImageController.Instance.FadeStart(FadeType.Out, this.timeLimit, Color.black);
		yield return null;

		//Fade完了まで待つ
		yield return new WaitUntil(() => !FadeImageController.Instance.CheckFadeNow());
		
		//シーン切り替え
		SceneManager.LoadScene(sName);
		
		//FadeIn
		FadeImageController.Instance.FadeStart(FadeType.In, this.timeLimit, Color.black);
		yield return null;

		//Fade完了まで待つ
		yield return new WaitUntil(() => !FadeImageController.Instance.CheckFadeNow());

		//終了
		this.isNowChange = false;
	}
}
