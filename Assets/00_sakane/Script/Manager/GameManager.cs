using System.Collections;
using UnityEngine;

// ���[���Ǘ��N���X
public class GameManager : MonoBehaviour
{
	// true = �Q�[���I��
	//bool isGameFinish = false;

	// true = �Q�[����
	//bool isInPlay = false;

	// ���U���g��\������L�����o�X
	[SerializeField]
	GameObject resultCanvas;

	[SerializeField]
	GameObject gameOverCanvas;

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
	Canvas countdownCanvas;
	CountDown countDown;

	// �ĊJ����܂ł̗P�\
	[SerializeField]
	float reStartTime = 3;

	private void Awake()
	{
		GameInstance.gameManager = this;
	}

	private void Start()
	{
		// ���U���g���\���ɂ���
		resultCanvas.SetActive(false);
		gameOverCanvas.SetActive(false);

		// �t�F�[�h�N���X�擾
		fade = GameInstance.fadeIO;
		countDown = GameInstance.countDown;

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

		countDown.gameObject.SetActive(true);
		countDown.StartCountDown(false);

		while (countDown.IsCounting)
		{
			yield return null;
		}

		// �v���C���[�𓮂����Ƃ��ł����Ԃɂ���
		playerObj.GetComponent<IPlayer>().GameStart();
		// �d�Ԃ𓮂����Ƃ��ł����Ԃɂ���
		train.GetComponent<ITrain>().Go();
	}

	// �Q�[���I��
	public void GameFinish()
	{
		
	}

	// �Q�[���I�[�o�[
	public void GameOver()
	{
		GameFinish();
		// �v���C���[�𓮂����Ȃ���Ԃɂ���
		playerObj.GetComponent<IPlayer>().GameFinish();
		train.GetComponent<ITrain>().Stop();
		// �Q�[���I�[�o�[��\��
		gameOverCanvas.SetActive(true);
	}

	// �Q�[���N���A
	public void GameClear()
	{
		GameFinish();
		// �v���C���[�𓮂����Ȃ���Ԃɂ���
		playerObj.GetComponent<IPlayer>().GameFinish();
		train.GetComponent<ITrain>().Stop();
		// ���U���g��\��
		resultCanvas.SetActive(true);
	}

	// �Q�[����~
	public void GameStop(){
		playerObj.GetComponent<IPlayer>().GameStop();
		train.GetComponent<ITrain>().Stop();
		//isInPlay = false;
	}

	// �Q�[���ĊJ
	public void GameReStart()
	{
		StartCoroutine(IGameReStart());
	}

	// �Q�[���ĊJ�R���[�`��
	IEnumerator IGameReStart()
	{
		var isReStarting = true;
		var time = reStartTime;
		while (isReStarting)
		{
			time -= Time.deltaTime;
			if (time <= 0)
			{
				isReStarting = false;
				//isInPlay = true;
				playerObj.GetComponent<IPlayer>().GameStart();
				train.GetComponent<ITrain>().Go();
			}
			yield return null;
		}
	}
}