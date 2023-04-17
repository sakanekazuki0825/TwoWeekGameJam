using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �D�ԃN���X
public class Train : MonoBehaviour, ITrain
{
	// ����
	Rigidbody rb;

	// �ŏ��̑��x
	[SerializeField]
	float startSpeed = 4;
	// ���݂̑��x
	float speed = 1;

	// �ŏI�I�ɓ��B���鑬�x
	float afterSpeed;

	// true = �ړ��ł���
	bool canMove = false;

	// �⊮���x
	float complement = 0;

	// �~�܂������ɑ�����ۑ�
	Vector3 stopBeforSpeed = new Vector3 (0, 0, 0);

	// ���e�͈�
	[SerializeField]
	float tolerance = 0.1f;
	// true = �^�񒆂�ʉ�
	bool isCenter = false;
	// �Ȃ���ʒu
	Vector3 curvePos = new Vector3(0, 0, 0);
	// �ŏI�ړI�n
	Vector3 finalDestination = new Vector3(10, 0, 0);
	// �ړI�n
	Vector3 targetPos = new Vector3(10, 0, 0);

	private void Awake()
	{
		// �����擾
		rb = GetComponent<Rigidbody>();
		// �������x�ݒ�
		rb.velocity = new Vector3(startSpeed, 0, 0);
	}

	private void FixedUpdate()
	{
		// �ړ��ł����Ԃ����ׂ�
		if (!canMove)
		{
			return;
		}

		if (!isCenter && Vector2.Distance(transform.position, curvePos) <= tolerance)
		{
			// ���e�͈͂ɓ�������ړI�n����
			transform.position = curvePos;
			// �ړI�n�X�V (�����̈ʒu + �ړI�n�̕��� * �摜�T�C�Y)
			targetPos = finalDestination;
			// �^�񒆂�ʉ�
			isCenter = true;
		}

		// �ړ�
		rb.velocity = (targetPos - transform.position).normalized * speed;
		// �ړ����x�X�V
		//speed = Mathf.Lerp(speed, afterSpeed, 0);
	}

	// �i��
	void ITrain.Go()
	{
		canMove = true;
		//rb.velocity = stopBeforSpeed;
	}

	// ��~
	void ITrain.Stop()
	{
		canMove = false;
		stopBeforSpeed = rb.velocity;
		rb.velocity = Vector3.zero;

	}

	// ���x���グ��
	void ITrain.AddSpeed(float speed)
	{
		afterSpeed += speed;
		// �⊮���x�v�Z
		complement = (speed + afterSpeed) / 2;// / �X�v���C�g�T�C�Y

		// �Ƃ肠����
		this.speed += speed;
	}

	// ������ύX
	void ITrain.Curve(Vector3 curvePos, Vector3 targetPos)
	{
		isCenter = false;

		// �Ȃ���ʒu�ƃp�l���̌q�����Ă���ʒu���擾
		this.curvePos = curvePos;
		this.finalDestination = targetPos;
	}
}
