using System.Collections;
using UnityEngine;

public class CameraMove : MonoBehaviour, ICameraMove
{
	// �ǂ�������I�u�W�F�N�g
	GameObject target;
	// �ǂ�������I�t�Z�b�g
	Vector3 offset = new Vector3(0, 0, 0);
	// true = �������Ƃ��ł���
	bool canMove = false;

	// �S�[���̈ʒu
	float goalPosX;

	void Update()
	{
		if (!canMove)
		{
			return;
		}
		if (target != null)
		{
			var position = target.transform.position + offset;
			position.y = transform.position.y;
			// �J�������Œ�
			transform.position = position;
		}
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

	// �S�[���̈ʒu�ݒ�
	void ICameraMove.SetGoalPos(float goalPos)
	{
		goalPosX = goalPos;
		StartCoroutine(GoGoalPos());
	}

	IEnumerator GoGoalPos()
	{
		yield return new WaitUntil(
			() =>
			{
				return transform.position.x > goalPosX;
			});
		transform.position = new Vector3(goalPosX, transform.position.y, transform.position.z);
		canMove = false;
	}
}
