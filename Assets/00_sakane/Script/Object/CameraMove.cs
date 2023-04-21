using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour, ICameraMove
{
	// �ǂ�������I�u�W�F�N�g
	GameObject target;
	// �ǂ�������I�t�Z�b�g
	Vector3 offset = new Vector3(0, 0, 0);
	// true = �������Ƃ��ł���
	bool canMove = false;

	void Update()
	{
		if (!canMove)
		{
			return;
		}
		var position = target.transform.position + offset;
		position.y = transform.position.y;
		// �J�������Œ�
		transform.position = position;
	}

	// �^�[�Q�b�g�̐ݒ�
	void ICameraMove.SetTarget(UnityEngine.GameObject target)
	{
		this.target = target;
	}

	// ��Ԑݒ�
	void ICameraMove.SetCanMove(bool canMove)
	{
		this.canMove = canMove;
		// �I�t�Z�b�g�̐ݒ�
		offset = transform.position - target.transform.position;
	}
}
