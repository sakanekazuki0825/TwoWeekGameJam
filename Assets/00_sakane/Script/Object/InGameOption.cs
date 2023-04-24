using UnityEngine;

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

	private void Start()
	{
		optionCanvas.SetActive(false);
		RetryCanvas.SetActive(false);
		SoundSettingCanvas.SetActive(false);
		retireCanvas.SetActive(false);
	}

	public void OptionClick()
	{
		optionCanvas.SetActive(true);
		GameInstance.gameManager.GameStop();
	}

	public void ReStart()
	{
		optionCanvas.SetActive(false);
		GameInstance.gameManager.GameReStart();
	}

	public void Retry()
	{
		RetryCanvas.SetActive(true);
	}

	public void SoundSetting()
	{
		SoundSettingCanvas.SetActive(true);
	}

	public void Retire()
	{
		retireCanvas.SetActive(true);
	}
}