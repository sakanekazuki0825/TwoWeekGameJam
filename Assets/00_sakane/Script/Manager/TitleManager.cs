using System.Collections;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
	// �t�F�[�h�N���X
	FadeIO fade;

	// �I�v�V����
	[SerializeField]
	GameObject optionCanvas;

	private void Awake()
	{
		GameInstance.titleManager = this;
	}

	void Start()
	{
		// �t�F�[�h���擾���ăt�F�[�h�C���J�n
		fade = GameInstance.fadeIO;
		fade.FadeIn();
		// �I�v�V�����L�����o�X��\��
		optionCanvas.SetActive(false);
	}

	private void OnDestroy()
	{
		GameInstance.titleManager = null;
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

	// �I�v�V����
	public void Option()
	{
		optionCanvas.SetActive(true);
	}

	// �Q�[���I��
	public void GameQuit()
	{
		LevelManager.GameFinish();
	}
}