using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	// �Q�[���J�n
	public void GameStart()
	{
		StartCoroutine(EGameStart());
	}

	// �Q�[���ǂݍ��݃R���[�`��
	IEnumerator EGameStart()
	{
		// �V�[���ǂݍ��݊J�n
		StartCoroutine(LevelManager.ELoadLevelAsync(""));

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