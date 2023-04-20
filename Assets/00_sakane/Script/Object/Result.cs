using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class Result : MonoBehaviour
{
	// 今回のスコア
	[SerializeField]
	Text nowScoreTxt;
	// 過去のスコア
	[SerializeField]
	Text resultTxt;

	// 小数点何位まで表示するのか
	[SerializeField]
	int decimalValue = 1;
	// 何人まで表示するか
	[SerializeField]
	int placePeople = 3;

	// 表示
	public void Display()
	{
		var value = GameInstance.gameManager.ClearTime;
		// 小数点1位まで表示する
		value = Mathf.Floor(value * 10);
		value /= 10;
		nowScoreTxt.text = value.ToString();
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
}