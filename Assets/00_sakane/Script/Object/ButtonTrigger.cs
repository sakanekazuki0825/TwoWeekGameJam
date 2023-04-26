using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTrigger : MonoBehaviour
{
	// 三角形
	[SerializeField]
	GameObject triangle;

    // ボタンの画像
    Image img;

	float animSpeed = 0.02f;
	bool isAnim = false;

	Vector3 defaultScale;

	private void Awake()
	{
		img = GetComponent<Image>();
		if (triangle != null)
		{
			triangle.SetActive(false);
		}
		defaultScale = transform.localScale;
	}

	public void Hovered()
    {
		var colorValue = 0.75f;
		img.color = new Color(colorValue, colorValue, colorValue);
		StartCoroutine(EHoveredAnim());
	}

	IEnumerator EHoveredAnim()
	{
		isAnim = true;
		var time = 0.0f;
		while (isAnim)
		{
			time += animSpeed;
			var value = (Mathf.Sin(time) + 1) * 0.1f + defaultScale.x;
			transform.localScale = new Vector3(value, value, transform.localScale.z);
			yield return null;
		}
	}

	public void UnHvered()
	{
		img.color = Color.white;
		isAnim = false;
		transform.localScale = defaultScale;
	}

	public void Click()
	{
		img.color= Color.white;
		if (triangle != null)
		{
			triangle.SetActive(false);
		}
	}
}
