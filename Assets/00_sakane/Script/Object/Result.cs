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
			if (resultTxt.text == "\n")
			{
				resultTxt.text = e.ToString() + "\n";
			}
			else
			{
				resultTxt.text += e.ToString() + "\n";
			}
		}

		scores.Add(value);
		Array.Sort(scores.ToArray());
		if (scores.Count > placePeople)
		{
			scores.Remove(scores[scores.Count - 1]);
		}
	}

	// ������x�V��
	public void Restart()
	{
		GameInstance.gameManager.LevelMove("Main");
	}

	// �^�C�g���ɖ߂�
	public void ReturnToTitle()
	{
		GameInstance.gameManager.LevelMove("Title");
	}

	IEnumerator EFinish(string levelName)
	{
		GameInstance.gameManager.Train.GetComponent<ITrain>().GoTitle();
		yield return new WaitUntil(() => { return GameInstance.gameManager.Train.GetComponent<ITrain>().IsScreenOut(); });

		// �t�F�[�h
		FadeIO fade = GameInstance.fadeIO;
		fade.FadeOut();
		yield return new WaitUntil(() => { return fade.IsFading; });

		// �ǂݍ���
		StartCoroutine(LevelManager.ELoadLevelAsync(levelName));
		while (LevelManager.IsLoading)
		{
			// ���[�h���

			yield return null;
		}
	}
}