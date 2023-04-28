using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 操作説明
public class OperationExplanation : MonoBehaviour
{
	// 画像を保存している場所
	[SerializeField]
	List<Sprite> explanations = new List<Sprite>();

	// 画像を表示するクラス
	[SerializeField]
	Image explanation;

	[SerializeField]
	Text page;

	// 画像番号
	int number = 0;

	private void Awake()
	{
		// 画像取得
		//explanation = GetComponentInChildren<Image>();
		// 一枚目も画像に変更
		explanation.sprite = explanations[0];
	}

	private void OnEnable()
	{
		// 画像を一枚目の画像に戻す
		explanation.sprite = explanations[0];
	}

	public void Change()
	{
		++number;
		number = number % explanations.Count;
		explanation.sprite = explanations[number];
		if (page != null)
		{
			page.text = (number + 1).ToString() + "/" + explanations.Count.ToString();
		}
	}

	// タイトルに戻る
	public void ReturnClick()
	{
		gameObject.SetActive(false);
	}
}
