using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
	// ���Ԃ�\������e�L�X�g
	Text timeTxt;
	
	private void Awake()
	{
		timeTxt = GetComponent<Text>();
	}

	private void Update()
	{
		var time = GameInstance.gameManager.ClearTime;
		//if (time % 1 >= 0.5f)
		//{
		//	timeTxt.text = Mathf.Ceil(time).ToString();
		//}
		//else
		//{
			timeTxt.text = Mathf.Floor(time).ToString();
		//}
	}
}
