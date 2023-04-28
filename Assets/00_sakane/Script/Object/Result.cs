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

	[SerializeField]
	GameObject noSelectObj;

	private void Awake()
	{
		noSelectObj.SetActive(false);
	}

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
			resultTxt.text += e.ToString() + "\n";
		}

		scores.Add(value);
		scores.Sort();
		if (scores.Count > placePeople)
		{
			scores.Remove(scores[scores.Count - 1]);
		}
	}

	// もう一度遊ぶ
	public void Restart()
	{
		EFinish("Main");
	}

	// タイトルに戻る
	public void ReturnToTitle()
	{
		EFinish("Title");
	}

	void EFinish(string levelName)
	{
		noSelectObj.SetActive(true);
		GameInstance.gameManager.Train.GetComponent<ITrain>().GoTitle(levelName);
		GameInstance.gameManager.NotSelectUI();
		gameObject.SetActive(false);
	}
}