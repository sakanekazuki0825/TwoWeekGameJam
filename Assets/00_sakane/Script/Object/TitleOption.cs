using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleOption : MonoBehaviour
{
	// 操作説明キャンバス
	[SerializeField]
	GameObject operationExplanationCanvas;
	// 音量設定キャンバス
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
