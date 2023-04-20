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

	private void OnEnable()
	{
		var value = (GameInstance.gameManager != null) ?
			GameInstance.gameManager.ClearTime :
			0;

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
