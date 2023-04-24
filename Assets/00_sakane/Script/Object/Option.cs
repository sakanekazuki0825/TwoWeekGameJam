using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
	[SerializeField]
	Text timeTxt;

	private void Start()
	{
		retireCanvas.SetActive(false);
	}

	private void Update()
	{
		var time = GameInstance.gameManager.ClearTime;
		if(time % 1 >= 0.5f)
		{
			timeTxt.text = Mathf.Ceil(time).ToString();
		}
		else
		{
			timeTxt.text = Mathf.Floor(time).ToString();
		}
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