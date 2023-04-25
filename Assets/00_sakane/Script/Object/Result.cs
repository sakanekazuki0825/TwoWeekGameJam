using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
		// 小数点?位まで表示する
		int dec = 10;
		for (int i = 0; i < decimalValue; ++i)
		{
			dec *= 10;
		}

		value = Mathf.Floor(value * dec);
		value /= dec;
		nowScoreTxt.text = value.ToString();

		var scores = ScoreManager.scores;

		foreach (var e in scores)
		{
			if (resultTxt.text == "\n")
			{
				resultTxt.text = e.ToString() + "\n";
			}
			else
			{
				resultTxt.text += e.ToString() + "\n";
			}
		}

		scores.Add(value);
		Array.Sort(scores.ToArray());
		if (scores.Count > placePeople)
		{
			scores.Remove(scores[scores.Count - 1]);
		}
	}

	// もう一度遊ぶ
	public void Restart()
	{
		GameInstance.gameManager.LevelMove("Main");
	}

	// タイトルに戻る
	public void ReturnToTitle()
	{
		GameInstance.gameManager.LevelMove("Title");
	}

	IEnumerator EFinish(string levelName)
	{
		GameInstance.gameManager.Train.GetComponent<ITrain>().GoTitle();
		yield return new WaitUntil(() => { return GameInstance.gameManager.Train.GetComponent<ITrain>().IsScreenOut(); });

		// フェード
		FadeIO fade = GameInstance.fadeIO;
		fade.FadeOut();
		yield return new WaitUntil(() => { return fade.IsFading; });

		// 読み込み
		StartCoroutine(LevelManager.ELoadLevelAsync(levelName));
		while (LevelManager.IsLoading)
		{
			// ロード画面

			yield return null;
		}
	}
}