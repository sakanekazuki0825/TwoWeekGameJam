using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �������
public class OperationExplanation : MonoBehaviour
{
	// �摜��ۑ����Ă���ꏊ
	[SerializeField]
	List<Sprite> explanations = new List<Sprite>();

	// �摜��\������N���X
	[SerializeField]
	Image explanation;

	[SerializeField]
	Text page;

	// �摜�ԍ�
	int number = 0;

	private void Awake()
	{
		// �摜�擾
		//explanation = GetComponentInChildren<Image>();
		// �ꖇ�ڂ��摜�ɕύX
		explanation.sprite = explanations[0];
	}

	private void OnEnable()
	{
		// �摜���ꖇ�ڂ̉摜�ɖ߂�
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

	// �^�C�g���ɖ߂�
	public void ReturnClick()
	{
		gameObject.SetActive(false);
	}
}
