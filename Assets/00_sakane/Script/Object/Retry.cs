using UnityEngine;

public class Retry : MonoBehaviour
{
	public void RetryClick()
	{
		StartCoroutine(LevelManager.ELoadLevelAsync("Main"));
	}

	public void NoRetryClick()
	{
		gameObject.SetActive(false);
	}
}
