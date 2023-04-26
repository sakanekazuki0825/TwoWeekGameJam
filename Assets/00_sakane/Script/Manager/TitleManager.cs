using System.Collections;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
	// �t�F�[�h�N���X
	FadeIO fade;

	// �I�v�V����
	[SerializeField]
	GameObject optionCanvas;

	[SerializeField]
	GameObject operationCanvas;

	// ���[�h���
	[SerializeField]
	GameObject loadScreen;

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
		loadScreen.SetActive(false);
		operationCanvas.SetActive(false);
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

		loadScreen.SetActive(true);

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

	// �������
	public void Operation()
	{
		operationCanvas.SetActive(true);
	}

	// �Q�[���I��
	public void GameQuit()
	{
		LevelManager.GameFinish();
	}
}