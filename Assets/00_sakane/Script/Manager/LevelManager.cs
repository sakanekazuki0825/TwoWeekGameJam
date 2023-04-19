using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// ���x���̓ǂݍ��݁A�Q�[���I��
public class LevelManager
{
	// true = �ǂݍ��ݒ�
	static bool isLoading = false;
	// �ǂݍ��ݏ�Ԏ擾
	public static bool IsLoading
	{
		get => isLoading;
	}

	// �Œ�ł����̎��Ԃ̓��[�h��ʂ�\��������i���������Ă�������I�j
	static float lowestDispTime = 0;

	// �V�[���ǂݍ���
	public static void LoadLevel(string levelName)
	{
		SceneManager.LoadScene(levelName);
	}

	// ���݂̃V�[��������x�ǂݍ���
	public static void LevelReload()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	// �Q�[����v���C��Ԃ̏I��
	public static void GameFinish()
	{
#if UNITY_EDITOR
		// �G�f�B�^�̏ꍇ�̓v���C��Ԃ�����
		UnityEditor.EditorApplication.isPlaying = false;
#else
		// �A�v���̏ꍇ�̓A�v���I��
		Application.Quit();
#endif
	}

	// �񓯊��ł̃V�[���ǂݍ���
	public static IEnumerator ELoadLevelAsync(string levelName)
	{
		// ���[�h��
		isLoading = true;
		// �ǂݍ��݃A�C�R����\������N���X��\��
		//GameObject.FindObjectOfType<LoadScreen>().gameObject.SetActive(true);

		// �����͎~�߂�i�������j
		yield return new WaitForSeconds(lowestDispTime);
		// �ǂݍ��݊J�n
		var async = SceneManager.LoadSceneAsync(levelName);
		// �ǂݍ��݂��I������܂Ń��[�v
		while (!async.isDone)
		{
			yield return null;
		}
		// �ǂݍ��ݏ�Ԃ�����
		isLoading = false;
	}
}