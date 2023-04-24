using UnityEngine;

public class Retire : MonoBehaviour
{
	// リタイヤ
	public void RetireClick()
	{
		StartCoroutine(LevelManager.ELoadLevelAsync("Title"));
	}

	// リタイヤしない
	public void NotRetireClick()
	{
		gameObject.SetActive(false);
	}
}
