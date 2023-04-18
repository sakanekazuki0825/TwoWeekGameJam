using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// ���[���Ǘ��N���X
public class GameManager : MonoBehaviour, IGameManager
{
	// true = �Q�[���I��
	//bool isGameFinish = false;

	// true = �Q�[����
	bool isInPlay = false;

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

	// �I���ł��Ȃ���Ԃɂ���I�u�W�F�N�g
	[SerializeField]
	GameObject cannotSelectObj;
	// �I���ł��Ȃ���Ԃɂ���I�u�W�F�N�g�̈ʒu
	[SerializeField]
	Vector3 cannotSelectObjPos = new Vector3(-11, 0, 0);

	// �J�����I�u�W�F�N�g
	[SerializeField]
	GameObject cameraObj;
	// �J�����̐����ʒu
	[SerializeField]
	Vector3 cameraPos;

	// �J�E���g�_�E��
	[SerializeField]
	float maxCountdownTime = 3;
	float countdownTime;
	[SerializeField]
	Canvas countdownCanvas;
	[SerializeField]
	Text countdownTxt;
	[SerializeField]
	string goStr = "Go";
	[SerializeField]
	float goTime = 1;

	// ���^�C��
	[SerializeField]
	Canvas retireCanvas;

	private void Awake()
	{
		GameInstance.gameManager = this;

		// �J�E���g�_�E�����Ԑݒ�
		countdownTime = maxCountdownTime;
	}

	private void Start()
	{
		// ���U���g���\���ɂ���
		resultCanvas.SetActive(false);
		// ���^�C�����\���ɂ���
		retireCanvas.gameObject.SetActive(false);

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

		// �v���C���[����
		playerObj = Instantiate(playerPrefab);
		// �v���C���[���~�߂�
		playerObj.GetComponent<IPlayer>().GameStop();
		// �d�Ԑ���
		train = Instantiate(trainPrefab, trainPos, Quaternion.identity);
		Instantiate(cameraObj, cameraPos, Quaternion.identity).GetComponent<ICameraMove>().SetTarget(train);
		Instantiate(cannotSelectObj, cannotSelectObjPos, Quaternion.identity).GetComponent<ICannotSelectPanel>().SetTarget(train);

		// �t�F�[�h���I���܂Ń��[�v
		while (fade.IsFading)
		{
			yield return null;
		}

		// �J�E���g�_�E��
		countdownCanvas.gameObject.SetActive(true);
		while (!isInPlay)
		{
			countdownTime -= Time.deltaTime;
			countdownTxt.text = Mathf.Ceil(countdownTime).ToString();
			if (countdownTime < 0)
			{
				countdownTxt.text = goStr;
				isInPlay = true;
			}
			yield return null;
		}

		// �v���C���[�𓮂����Ƃ��ł����Ԃɂ���
		playerObj.GetComponent<IPlayer>().GameStart();
		// �d�Ԃ𓮂����Ƃ��ł����Ԃɂ���
		train.GetComponent<ITrain>().Go();

		yield return new WaitForSeconds(goTime);
		countdownCanvas.gameObject.SetActive(false);

		// �t�F�[�h���I������瑀��ł���
		// = true;
	}

	// ���^�C��
	public void Retire()
	{
		retireCanvas.gameObject.SetActive(true);
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
		isInPlay = false;
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