using UnityEngine;

// �ǂݍ��ݒ��ɕ\���������
public class LoadScreen : MonoBehaviour
{
	// �ǂݍ��ݒ��\������A�C�R��
	[SerializeField]
	GameObject loadIcon;

	// ��]���x
	[SerializeField]
	float rotSpeed = 5.0f;

	private void Update()
	{
		// ��]������
		loadIcon.transform.Rotate(new Vector3(0, 0, rotSpeed));
	}
}
