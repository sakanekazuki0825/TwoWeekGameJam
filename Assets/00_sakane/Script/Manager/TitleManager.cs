using System.Collections;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
	// フェードクラス
	FadeIO fade;

	// オプション
	[SerializeField]
	GameObject optionCanvas;

	[SerializeField]
	GameObject operationCanvas;

	// ロード画面
	[SerializeField]
	GameObject loadScreen;

	private void Awake()
	{
		GameInstance.titleManager = this;
	}

	void Start()
	{
		// フェードを取得してフェードイン開始
		fade = GameInstance.fadeIO;
		fade.FadeIn();
		// オプションキャンバス非表示
		loadScreen.SetActive(false);
		operationCanvas.SetActive(false);
		optionCanvas.SetActive(false);
	}

	private void OnDestroy()
	{
		GameInstance.titleManager = null;
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

		loadScreen.SetActive(true);

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

	// 操作説明
	public void Operation()
	{
		operationCanvas.SetActive(true);
	}

	// ゲーム終了
	public void GameQuit()
	{
		LevelManager.GameFinish();
	}
}