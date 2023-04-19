using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour,IResult
{
// 結果を表示するテキスト
	Text resultTxt;

	private void Awake()
	{
		resultTxt = GetComponentInChildren<Text>();
	}

	// もう一度遊ぶ
	public void Restart()
	{
		StartCoroutine(EFinish("Main"));
	}

	// タイトルに戻る
	public void ReturnToTitle()
	{
		StartCoroutine(EFinish("Title"));
	}

	IEnumerator EFinish(string levelName)
	{
		// フェード
		FadeIO fade = GameInstance.fadeIO;
		fade.FadeOut();
		while (fade.IsFading)
		{
			yield return null;
		}

		// 読み込み
		StartCoroutine(LevelManager.ELoadLevelAsync(levelName));
		while (LevelManager.IsLoading)
		{
			// ロード画面

			yield return null;
		}
	}

	// ゲームクリア
	void IResult.GameClear()
	{
		resultTxt.text = "Result";
	}
}
