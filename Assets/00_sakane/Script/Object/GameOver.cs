using UnityEngine;

public class GameOver : MonoBehaviour
{
	public void ReStart()
	{
		StartCoroutine(LevelManager.ELoadLevelAsync("Main"));
	}

	public void ReturnToTitle()
	{
		StartCoroutine(LevelManager.ELoadLevelAsync("Title"));
	}
}
