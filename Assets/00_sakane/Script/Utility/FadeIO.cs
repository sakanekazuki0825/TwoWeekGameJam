using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// フェード管理クラス
public class FadeIO : MonoBehaviour
{
	// フェードをする画像
	[SerializeField]
	Image fadeImg;

	// フェード速度
	[SerializeField]
	float fadeSpeed;

	// 許容範囲
	[SerializeField]
	float toleranceValue;

	// フェード中
	bool isFading = false;
	public bool IsFading { get => isFading; }

	// フェードイン
	public void FadeIn()
	{
		StartCoroutine(EFadeIn());
	}

	// フェードインコルーチン
	IEnumerator EFadeIn()
	{
		isFading = true;
		fadeImg.color = Color.black;
		while (fadeImg.color.a >= toleranceValue)
		{
			yield return null;
			// アルファを抜く
			fadeImg.color -= new Color(0, 0, 0, fadeSpeed);
		}
		isFading = false;
	}

	// フェードアウト
	public void FadeOut()
	{
		StartCoroutine(EFadeOut());
	}

	// フェードアウトコルーチン
	IEnumerator EFadeOut()
	{
		isFading = true;
		fadeImg.color = new Color(0, 0, 0, 0);
		while (fadeImg.color.a <= 1 - toleranceValue)
		{
			yield return null;
			// 色を濃くしていく
			fadeImg.color += new Color(0, 0, 0, fadeSpeed);
		}
		isFading = false;
	}
}