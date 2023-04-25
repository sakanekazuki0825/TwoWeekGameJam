using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTrigger : MonoBehaviour
{
	// �O�p�`
	[SerializeField]
	GameObject triangle;

    // �{�^���̉摜
    Image img;

	private void Awake()
	{
		img = GetComponent<Image>();
		if (triangle != null)
		{
			triangle.SetActive(false);
		}
	}

	public void Hovered()
    {
		img.color = Color.gray;
		if (triangle != null)
		{
			triangle.SetActive(true);
		}
	}

	public void UnHvered()
	{
		img.color = Color.white;
		if (triangle != null)
		{
			triangle.SetActive(false);
		}
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
