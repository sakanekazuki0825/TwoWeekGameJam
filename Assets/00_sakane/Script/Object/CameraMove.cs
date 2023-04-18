using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour,ICameraMove
{
	// �ǂ�������I�u�W�F�N�g
	GameObject target;
	// �ǂ�������I�t�Z�b�g
	Vector3 offset = new Vector3(0, 0, 0);

	private void Start()
	{
		// �I�t�Z�b�g�̐ݒ�
		offset = transform.position - target.transform.position;
	}

	void Update()
	{
		var position = target.transform.position + offset;
		position.y = transform.position.y;
		// �J�������Œ�
		transform.position = position;
	}

	void ICameraMove.SetTarget(UnityEngine.GameObject target)
	{
		this.target = target;
	}
}
