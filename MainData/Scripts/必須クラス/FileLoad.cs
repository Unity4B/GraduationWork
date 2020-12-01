using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

//-------------------------------------------------------------------------------
/// <summary>
/// 読み込みクラス
/// </summary>
public class FileLoad
{
	//-------------------------------------------------------------------------------
	/// <summary>
	/// 文字列の読み込み(リソース)
	/// </summary>
	/// <param name="fileName">読み込むファイル名</param>
	/// <returns>読み込んだ文字列(行単位)</returns>
	public static List<string> DataLoadResource(string fileName)
	{
		List<string> data = new List<string>();

		//パスを設定
		string path = "Text/" + fileName;

		//テキストファイルをセット
		TextAsset t = Resources.Load(path) as TextAsset;

		//エラーチェック
		if (t == null)
		{
			Debug.LogError("リソースからテキストファイルを読み込めませんでした。：" + fileName);
			data.Add("失敗");
			//処理終了
			return data;
		}

		//テキストファイルを読み込む
		StringReader sr = new StringReader(t.text);

		//無記入の行までループする
		while (sr.Peek() > -1)
		{
			string line = sr.ReadLine();
			//1行ずつリストに追加
			data.Add(line);
		}

		Debug.Log("テキストファイル読み込み完了。：" + fileName);


		//リストを返す
		return data;
	}
	//-------------------------------------------------------------------------------
	/// <summary>
	/// 文字列の読み込み(外部)
	/// </summary>
	/// <param name="fileName">読み込むファイル名</param>
	/// <returns>読み込んだ文字列(行単位)</returns>
	public static List<string> DataLoadFile(string fileName)
	{
		List<string> data = new List<string>();

		//パスを設定
		string path = Application.dataPath + "Text/" + fileName;

		//テキストファイルをセット
		FileInfo fi = new FileInfo(path);

		//エラーチェック
		try
		{
			//ファイル読み込み
			using (StreamReader sr = new StreamReader(fi.OpenRead(), Encoding.UTF8))
			{
				//無記入の行までループする
				while (sr.Peek() > -1)
				{
					string line = sr.ReadLine();
					//1行ずつリストに追加
					data.Add(line);
				}
			}
		}
		//エラー処理
		catch (System.Exception e_)
		{
			Debug.LogError(e_.Message);
			Debug.LogError("外部からテキストファイルを読み込めませんでした。：" + fileName);

			data.Add("失敗");
		}


		Debug.Log("テキストファイル読み込み完了。：" + fileName);


		//リストを返す
		return data;
	}
	//-------------------------------------------------------------------------------
	/// <summary>
	/// 音源の読み込み(リソース)
	/// </summary>
	/// <param name="fileName">読み込むファイル名</param>
	/// <returns>読み込んだ音源</returns>
	public static AudioClip SoundDataLoadResource(string fileName)
	{
		//パスを設定
		string path = "Sound/" + fileName;

		//ファイル読み込み
		AudioClip data = Resources.Load(path) as AudioClip;


		//エラーチェック
		if (data == null)
		{
			Debug.LogError("リソースから音源ファイルを読み込めませんでした。：" + fileName);
			//処理終了
			return data;
		}

		Debug.Log("音源ファイル読み込み完了。：" + fileName);


		//リストを返す
		return data;
	}
	//-------------------------------------------------------------------------------
	/// <summary>
	/// 画像の読み込み(リソース)
	/// </summary>
	/// <param name="fileName">読み込むファイル名</param>
	/// <returns>読み込んだ画像</returns>
	public static Sprite SpriteDataLoadResource(string fileName)
	{
		//パスを設定
		string path = "Sprite/" + fileName;

		//ファイル読み込み
		Sprite data = (Sprite)Resources.Load(path,typeof(Sprite));


		//エラーチェック
		if (data == null)
		{
			Debug.LogError("リソースから画像ファイルを読み込めませんでした。：" + fileName);
			//処理終了
			return data;
		}

		Debug.Log("画像ファイル読み込み完了。：" + fileName);


		//リストを返す
		return data;
	}
}
