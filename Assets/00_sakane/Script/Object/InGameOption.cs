using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameOption : MonoBehaviour
{
	// オプション
	[SerializeField]
	GameObject optionCanvas;

	// リトライキャンバス
	[SerializeField]
	GameObject RetryCanvas;

	// 音量キャンバス
	[SerializeField]
	GameObject SoundSettingCanvas;

	// リタイヤキャンバス
	[SerializeField]
	GameObject retireCanvas;

	[SerializeField]
	List<GameObject> butonList = new List<GameObject>();

	private void Start()
	{
		optionCanvas.SetActive(false);
		RetryCanvas.SetActive(false);
		SoundSettingCanvas.SetActive(false);
		retireCanvas.SetActive(false);
	}

	void ButtonUnActive()
	{
		foreach (var button in butonList)
		{
			button.SetActive(false);
		}
	}

	public void ButtonActive()
	{
		foreach (var button in butonList)
		{
			button.SetActive(true);
		}
	}

	public void OptionClick()
	{
		optionCanvas.SetActive(true);
		GameInstance.gameManager.GameStop();
	}

	public void Retry()
	{
		RetryCanvas.SetActive(true);
		ButtonUnActive();
	}

	public void SoundSetting()
	{
		SoundSettingCanvas.SetActive(true);
		ButtonUnActive();
	}

	public void Retire()
	{
		retireCanvas.SetActive(true);
		ButtonUnActive();
	}

	public void ReStart()
	{
		optionCanvas.SetActive(false);
		GameInstance.gameManager.GameReStart();
	}
}