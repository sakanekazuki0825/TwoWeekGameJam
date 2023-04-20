using UnityEngine;

public class CannotSelectPanel : MonoBehaviour,ICannotSelectPanel
{
	// �ǐՂ���I�u�W�F�N�g
	GameObject target;
	// �ǐՂ���I�t�Z�b�g
	Vector3 offset;

	void Start()
	{
		// �I�t�Z�b�g�̐ݒ�
		offset = transform.position - target.transform.position;
	}

	void Update()
	{
		var position = transform.position + offset;
		position.y = 0;

		transform.position = target.transform.position + offset;
	}

	// �^�[�Q�b�g�ݒ�
	void ICannotSelectPanel.SetTarget(UnityEngine.GameObject target)
	{
		this.target = target;
	}
}
