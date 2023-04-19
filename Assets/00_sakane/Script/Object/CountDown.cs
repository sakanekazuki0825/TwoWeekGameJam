using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
	// true = カウント中
	bool isCounting;
	public bool IsCounting{ get => isCounting; }
	// ゲーム開始時の時間
	[SerializeField]
	float startCountDownTime = 3;
	// オプション画面などを閉じた時の時間
	[SerializeField]
	float inGameCountDownTime = 3;
	// 最後のテキストを表示する時間
	[SerializeField]
	float displayTime = 1;
	// 最後に表示する文字
	[SerializeField]
	string lastStr;
	// カウントダウンを表示するテキスト
	[SerializeField]
	Text countTxt;

	private void Awake()
	{
		GameInstance.countDown = this;
	}

	private void OnDestroy()
	{
		GameInstance.countDown = null;
	}

	// カウントダウン開始
	public void StartCountDown(bool isInGame = true)
	{
		StartCoroutine(IStartCountDown(isInGame));
	}

	// カウントダウンコルーチン
	IEnumerator IStartCountDown(bool isInGame)
	{
		isCounting = true;
		var time = inGameCountDownTime;
		if (!isInGame)
		{
			time = startCountDownTime;
		}
		while (isCounting)
		{
			time -= Time.deltaTime;
			countTxt.text = Mathf.Ceil(time).ToString();
			if (time <= 0)
			{
				countTxt.text = lastStr;
				isCounting = false;
				break;
			}
			yield return null;
		}
		yield return new WaitForSeconds(displayTime);
		gameObject.SetActive(false);
	}
}
