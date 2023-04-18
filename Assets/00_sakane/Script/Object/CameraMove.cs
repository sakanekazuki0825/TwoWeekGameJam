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
		// �J�������Œ�
		transform.position = target.transform.position + offset;
	}

	void ICameraMove.SetTarget(UnityEngine.GameObject target)
	{
		this.target = target;
	}
}
