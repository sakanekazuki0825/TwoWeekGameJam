using UnityEngine;

public class Option : MonoBehaviour
{
	private void Start()
	{
		retireCanvas.SetActive(false);
	}

	// オプション
	public void OptionClick()
	{
		retireCanvas.SetActive(true);
		GameInstance.gameManager.GameStop();
	}

	//-----リタイヤ-----

	// リタイヤキャンバス
	[SerializeField]
	GameObject retireCanvas;

	// リタイヤ
	public void Retire()
	{
		StartCoroutine(LevelManager.ELoadLevelAsync("Title"));
	}

	// リタイヤしない
	public void NotRetire()
	{
		retireCanvas.SetActive(false);
		GameInstance.gameManager.GameReStart();
		GameInstance.countDown.gameObject.SetActive(true);
		GameInstance.countDown.StartCountDown();
	}
	//-----リタイヤ-----
}