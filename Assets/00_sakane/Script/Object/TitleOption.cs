using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleOption : MonoBehaviour
{
	// ��������L�����o�X
	[SerializeField]
	GameObject operationExplanationCanvas;
	// ���ʐݒ�L�����o�X
	[SerializeField]
	GameObject soundSettingCanvas;

	private void Awake()
	{
		operationExplanationCanvas.SetActive(false);
		soundSettingCanvas.SetActive(false);
	}

	public void OperationExplanation()
	{
		operationExplanationCanvas.SetActive(true);
	}

	public void SoundSettingClick()
	{
		soundSettingCanvas.SetActive(true);
	}
}
