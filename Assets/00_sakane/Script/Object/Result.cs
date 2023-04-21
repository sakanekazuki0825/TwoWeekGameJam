using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

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
	}

	// ������x�V��
	public void Restart()
	{
		StartCoroutine(EFinish("Main"));
	}

	// �^�C�g���ɖ߂�
	public void ReturnToTitle()
	{
		StartCoroutine(EFinish("Title"));
	}

	IEnumerator EFinish(string levelName)
	{
		// �t�F�[�h
		FadeIO fade = GameInstance.fadeIO;
		fade.FadeOut();
		while (fade.IsFading)
		{
			yield return null;
		}

		// �ǂݍ���
		StartCoroutine(LevelManager.ELoadLevelAsync(levelName));
		while (LevelManager.IsLoading)
		{
			// ���[�h���

			yield return null;
		}
	}
}