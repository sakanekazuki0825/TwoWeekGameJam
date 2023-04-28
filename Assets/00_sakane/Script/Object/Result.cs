using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
	// ����̃X�R�A
	[SerializeField]
	Text nowScoreTxt;
	// �ߋ��̃X�R�A
	[SerializeField]
	Text resultTxt;

	// �����_���ʂ܂ŕ\������̂�
	[SerializeField]
	int decimalValue = 1;
	// ���l�܂ŕ\�����邩
	[SerializeField]
	int placePeople = 3;

	[SerializeField]
	GameObject noSelectObj;

	private void Awake()
	{
		noSelectObj.SetActive(false);
	}

	// �\��
	public void Display()
	{
		var value = GameInstance.gameManager.ClearTime;
		// �����_?�ʂ܂ŕ\������
		int dec = 10;
		for (int i = 0; i < decimalValue; ++i)
		{
			dec *= 10;
		}

		value = Mathf.Floor(value * dec);
		value /= dec;
		nowScoreTxt.text = value.ToString();

		var scores = ScoreManager.scores;

		foreach (var e in scores)
		{
			resultTxt.text += e.ToString() + "\n";
		}

		scores.Add(value);
		scores.Sort();
		if (scores.Count > placePeople)
		{
			scores.Remove(scores[scores.Count - 1]);
		}
	}

	// ������x�V��
	public void Restart()
	{
		EFinish("Main");
	}

	// �^�C�g���ɖ߂�
	public void ReturnToTitle()
	{
		EFinish("Title");
	}

	void EFinish(string levelName)
	{
		noSelectObj.SetActive(true);
		GameInstance.gameManager.Train.GetComponent<ITrain>().GoTitle(levelName);
		GameInstance.gameManager.NotSelectUI();
		gameObject.SetActive(false);
	}
}