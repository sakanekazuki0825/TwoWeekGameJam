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

	private void OnEnable()
	{
		var value = (GameInstance.gameManager != null) ?
			GameInstance.gameManager.ClearTime :
			0;

		value = Mathf.Floor(value * 10);
		value /= 10;

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
