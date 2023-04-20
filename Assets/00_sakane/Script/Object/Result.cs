using System.Collections;
using UnityEngine;

public class Result : MonoBehaviour
{
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
