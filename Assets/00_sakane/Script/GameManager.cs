using System.Collections;
using UnityEngine;

// ���[���Ǘ��N���X
public class GameManager : MonoBehaviour,IGameManager
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

	private void Start()
	{
		// ���U���g���\���ɂ���
		//resultCanvas.SetActive(false);

		// �t�F�[�h�N���X�擾
		fade = GameObject.FindObjectOfType<FadeIO>();

		// �Q�[���J�n
		GameStart();
	}

	// �Q�[���J�n
	public void GameStart()
	{
		// �d�Ԑ���
		train = Instantiate(trainPrefab, new Vector3(0, 0, 0), Quaternion.identity);
		StartCoroutine(EGameStart());
	}

	// �Q�[���J�n�R���[�`��
	IEnumerator EGameStart()
	{
		// �܂�����ł��Ȃ�
		//isInPlay = false;

		// �t�F�[�h�C��
		//fade.FadeIn();

		// �t�F�[�h���I���܂Ń��[�v
		//while (fade.IsFading)
		//{
		yield return null;
		//}

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
		StartCoroutine (EGameFinish());
	}

	// �Q�[���I���R���[�`��
	IEnumerator EGameFinish()
	{
		// �v���C���[�𓮂����Ȃ���Ԃɂ���
		playerObj.GetComponent<IPlayer>().GameFinish();
		// ���U���g��\��
		//resultCanvas.SetActive(true);
		// ����ł��Ȃ���Ԃɂ���
		//isInPlay = false;
		// �t�F�[�h�A�E�g
		//fade.FadeOut();

		// �t�F�[�h���I���܂Ń��[�v
		while (fade.IsFading)
		{
			yield return null;
		}

		// �V�[���ǂݍ���
	}

	// �v���C���[��������O�ꂽ
	void IGameManager.GetOffTheRoad()
	{
		// �Q�[���I�[�o�[
		playerObj.GetComponent<IPlayer>().GameFinish();
		GameFinish();
	}
}