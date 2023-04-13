using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyEnum;

// �D�ԃN���X
public class Train : MonoBehaviour, ITrain
{
	// ���݂̌���
	Direction direction;
	// �����Ă�������ɑ΂��ē�������
	[SerializeField]
	List<Vector2> dir;

	// ����
	Rigidbody rb;

	// �ŏ��̑��x
	[SerializeField]
	float startSpeed = 4;
	// ���݂̑��x
	float speed = 0;

	// �ŏI�I�ɓ��B���鑬�x
	float afterSpeed;

	// true = �ړ��ł���
	bool canMove = false;

	// �⊮���x
	float complement = 0;

	// �ړI�n
	Vector3 targetPos;

	private void Start()
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
		// �ړ�
		rb.velocity = (targetPos - transform.position) * speed;
		// �ړ����x�X�V
		speed = Mathf.Lerp(speed, afterSpeed, complement);
	}

	private void OnTriggerEnter(Collider other)
	{
		// �p�l���ɓ��������ꍇ�ړI�n�ݒ�
		if (other.gameObject.CompareTag("Panel"))
		{
			targetPos = other.transform.position;
		}
	}

	// �i��
	void ITrain.Go()
	{
		canMove = true;
	}

	// ��~
	void ITrain.Stop()
	{
		canMove = false;
	}

	// ���x���グ��
	void ITrain.AddSpeed(float speed)
	{
		afterSpeed = speed;
		// �⊮���x�v�Z
		complement = (speed + afterSpeed) / 2;// / �X�v���C�g�T�C�Y
	}

	// ������ύX
	void ITrain.Curve(Direction dir)
	{
		direction = dir;
	}
}
