using System.Collections;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
	// フェードクラス
	FadeIO fade;

	// オプション
	[SerializeField]
	GameObject optionCanvas;

	void Start()
	{
		// フェードを取得してフェードイン開始
		fade = GameInstance.fadeIO;
		fade.FadeIn();
		// オプションキャンバス非表示
		optionCanvas.SetActive(false);
	}

	// ゲーム開始
	public void GameStart()
	{
		StartCoroutine(EGameStart());
	}

	// ゲーム読み込みコルーチン
	IEnumerator EGameStart()
	{
		// フェードアウト
		fade.FadeOut();
		// フェードアウトが終わるまで待つ
		while (fade.IsFading)
		{
			yield return null;
		}

		// フェードが終わってからシーン読み込み開始
		StartCoroutine(LevelManager.ELoadLevelAsync("Main"));

		// ロードが終わるまで待つ（なにかアニメーションさせる）
		while (LevelManager.IsLoading)
		{
			yield return null;
		}
	}

	// オプション
	public void Option()
	{
		optionCanvas.SetActive(true);
	}

	// ゲーム終了
	public void GameQuit()
	{
		LevelManager.GameFinish();
	}
}