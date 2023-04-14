using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour,IResult
{
	Text resultTxt;

	private void Awake()
	{
		resultTxt = GetComponentInChildren<Text>();
	}

	// ゲームクリア
	void IResult.GameClear()
	{
		resultTxt.text = "GameClear";
	}

	// ゲームオーバー
	void IResult.GameOver()
	{
		resultTxt.text = "GameOver";
	}
}
