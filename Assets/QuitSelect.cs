using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitSelect : MonoBehaviour
{
	public void YesClick()
	{
		LevelManager.GameFinish();
	}

	public void NoClick()
	{
		gameObject.SetActive(false);
	}
}