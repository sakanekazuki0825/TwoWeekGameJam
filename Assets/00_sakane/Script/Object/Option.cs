using UnityEngine;

public class Option : MonoBehaviour
{
	private void Start()
	{
		retireCanvas.SetActive(false);
	}

	// �I�v�V����
	public void OptionClick()
	{
		retireCanvas.SetActive(true);
		GameInstance.gameManager.GameStop();
	}

	//-----���^�C��-----

	// ���^�C���L�����o�X
	[SerializeField]
	GameObject retireCanvas;

	// ���^�C��
	public void Retire()
	{
		StartCoroutine(LevelManager.ELoadLevelAsync("Title"));
	}

	// ���^�C�����Ȃ�
	public void NotRetire()
	{
		retireCanvas.SetActive(false);
		GameInstance.gameManager.GameReStart();
		GameInstance.countDown.gameObject.SetActive(true);
		GameInstance.countDown.StartCountDown();
	}
	//-----���^�C��-----
}