using System.Collections;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
	// �t�F�[�h�N���X
	FadeIO fade;

	void Start()
	{
		// �t�F�[�h���擾���ăt�F�[�h�C���J�n
		fade = GameInstance.fadeIO;
		fade.FadeIn();
	}

	// �Q�[���J�n
	public void GameStart()
	{
		StartCoroutine(EGameStart());
	}

	// �Q�[���ǂݍ��݃R���[�`��
	IEnumerator EGameStart()
	{
		// �t�F�[�h�A�E�g
		fade.FadeOut();
		// �t�F�[�h�A�E�g���I���܂ő҂�
		while (fade.IsFading)
		{
			yield return null;
		}

		// �t�F�[�h���I����Ă���V�[���ǂݍ��݊J�n
		StartCoroutine(LevelManager.ELoadLevelAsync("Main"));

		// ���[�h���I���܂ő҂i�Ȃɂ��A�j���[�V����������j
		while (LevelManager.IsLoading)
		{
			yield return null;
		}
	}

	// �Q�[���I��
	public void GameQuit()
	{
		LevelManager.GameFinish();
	}
}