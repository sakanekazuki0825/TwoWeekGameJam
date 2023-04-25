using UnityEngine;

public class GameOver : MonoBehaviour
{
	public void ReStart()
	{
		GameInstance.gameManager.LevelMove("Main");
	}

	public void ReturnToTitle()
	{
		GameInstance.gameManager.LevelMove("Title");
	}
}
