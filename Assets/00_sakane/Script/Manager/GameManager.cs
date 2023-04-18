using System.Collections;
using UnityEngine;

// ���[���Ǘ��N���X
public class GameManager : MonoBehaviour, IGameManager
{
	// true = �Q�[���I��
	//bool isGameFinish = false;

	// true = �Q�[����
	//bool isInPlay = false;

	// ���U���g��\������L�����o�X
	[SerializeField]
	GameObject resultCanvas;

	// �t�F�[�h������N���X
	FadeIO fade;

	// �v���C���[�v���n�u
	[SerializeField]
	GameObject playerPrefab;
	// ���������I�u�W�F�N�g
	GameObject playerObj;
	// ��������d��
	[SerializeField]
	GameObject trainPrefab;
	// ���������d��
	GameObject train;
	// �d�Ԑ����ʒu
	[SerializeField]
	Vector3 trainPos;

	// �J�����I�u�W�F�N�g
	[SerializeField]
	GameObject cameraObj;
	// �J�����̐����ʒu
	[SerializeField]
	Vector3 cameraPos;

	private void Awake()
	{
		GameInstance.gameManager = this;
	}

	private void Start()
	{
		// ���U���g���\���ɂ���
		resultCanvas.SetActive(false);

		// �t�F�[�h�N���X�擾
		fade = GameInstance.fadeIO;

		// �Q�[���J�n
		GameStart();
	}

	private void Update()
	{
#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			LevelManager.GameFinish();
		}
#endif
	}

	private void OnDestroy()
	{
		GameInstance.gameManager = null;
	}

	// �Q�[���J�n
	public void GameStart()
	{
		// �d�Ԑ���
		train = Instantiate(trainPrefab, trainPos, Quaternion.identity);
		Instantiate(cameraObj, cameraPos, Quaternion.identity).GetComponent<ICameraMove>().SetTarget(train);
		// �d�Ԃ𓮂����Ƃ��ł����Ԃɂ���
		train.GetComponent<ITrain>().Go();
		// �Q�[���J�n
		StartCoroutine(EGameStart());
	}

	// �Q�[���J�n�R���[�`��
	IEnumerator EGameStart()
	{
		// �܂�����ł��Ȃ�
		//isInPlay = false;

		// �t�F�[�h�C��
		fade.FadeIn();

		// �t�F�[�h���I���܂Ń��[�v
		while (fade.IsFading)
		{
			yield return null;
		}

		// �v���C���[����
		playerObj = Instantiate(playerPrefab);
		// �v���C���[���~�߂�
		playerObj.GetComponent<IPlayer>().GameStop();
		// �v���C���[�𓮂����Ƃ��ł����Ԃɂ���
		playerObj.GetComponent<IPlayer>().GameStart();

		// �t�F�[�h���I������瑀��ł���
		//isInPlay = true;
	}

	// �Q�[���I��
	public void GameFinish()
	{
		StartCoroutine(EGameFinish());
	}

	// �Q�[���I���R���[�`��
	IEnumerator EGameFinish()
	{
		// �v���C���[�𓮂����Ȃ���Ԃɂ���
		playerObj.GetComponent<IPlayer>().GameFinish();
		// ���U���g��\��
		resultCanvas.SetActive(true);
		// ����ł��Ȃ���Ԃɂ���
		//isInPlay = false;
		// �t�F�[�h�A�E�g
		fade.FadeOut();

		// �t�F�[�h���I���܂Ń��[�v
		while (fade.IsFading)
		{
			yield return null;
		}

		// �V�[���ǂݍ���
		StartCoroutine(LevelManager.ELoadLevelAsync("Title"));
	}

	public void GameOver()
	{
		resultCanvas.GetComponent<IResult>().GameOver();
		GameFinish();
		train.GetComponent<ITrain>().Stop();
	}

	public void GameClear()
	{
		resultCanvas.GetComponent<IResult>().GameClear();
		GameFinish();
		train.GetComponent<ITrain>().Stop();
	}

	// �v���C���[��������O�ꂽ
	void IGameManager.GetOffTheRoad()
	{
		//// �Q�[���I�[�o�[
		//playerObj.GetComponent<IPlayer>().GameFinish();
		//GameFinish();
	}
}