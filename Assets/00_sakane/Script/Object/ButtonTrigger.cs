using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTrigger : MonoBehaviour
{
    // �{�^���̉摜
    Image img;

	private void Awake()
	{
		img = GetComponent<Image>();
	}

	public void Hovered()
    {
		img.color = Color.gray;
    }

	public void UnHvered()
	{
		img.color = Color.white;
	}
}
