using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// レベルの読み込み、ゲーム終了
public class LevelManager
{
	// true = 読み込み中
	static bool isLoading = false;
	// 読み込み状態取得
	public static bool IsLoading
	{
		get => isLoading;
	}

	// 最低でもこの時間はロード画面を表示させる
	[SerializeField]
	float lowestDispTime = 0.5f;

	// シーン読み込み
	public static void LoadLevel(string levelName)
	{
		SceneManager.LoadScene(levelName);
	}

	// ゲームやプレイ状態の終了
	public static void GameFinish()
	{
#if UNITY_EDITOR
		// エディタの場合はプレイ状態を解除
		UnityEditor.EditorApplication.isPlaying = false;
#else
		// アプリの場合はアプリ終了
		Application.Quit();
#endif
	}

	// 非同期でのシーン読み込み
	public IEnumerator ELoadLevelAsync(string levelName)
	{
		isLoading = true;
		// 読み込みアイコンを表示するクラスを表示
		GameObject.FindObjectOfType<LoadScreen>().gameObject.SetActive(true);
		// 少しは止める（無理やり）
		yield return new WaitForSeconds(lowestDispTime);
		// 読み込み開始
		var async = SceneManager.LoadSceneAsync(levelName);
		// 読み込みが終了するまでループ
		while (!async.isDone)
		{
			yield return null;
		}
		// 読み込み状態を解除
		isLoading = false;
	}
}
