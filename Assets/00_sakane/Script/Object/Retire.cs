using UnityEngine;

public class Retire : MonoBehaviour
{
	// ���^�C��
	public void RetireClick()
	{
		StartCoroutine(LevelManager.ELoadLevelAsync("Title"));
	}

	// ���^�C�����Ȃ�
	public void NotRetireClick()
	{
		gameObject.SetActive(false);
	}
}
