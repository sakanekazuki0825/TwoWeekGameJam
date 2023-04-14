using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyEnum;

// �D�ԃN���X
public class Train : MonoBehaviour, ITrain
{
	// �ړ��������
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
	float speed = 1;

	// �ŏI�I�ɓ��B���鑬�x
	float afterSpeed;

	// true = �ړ��ł���
	bool canMove = false;

	// �⊮���x
	float complement = 0;

	// �ړI�n
	Vector3 targetPos = new Vector3(10, 0, 0);
	// �ړI�n
	Direction targetDir;

	Vector3 stopBeforSpeed = new Vector3 (0, 0, 0);

	// �Ƃ肠����
	float hokan = 0.1f;

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

		//// ---------------------------------------------
		//// �Ƃ肠��������

		//// ���Ɉړ���
		//if (rb.velocity.x < 0)
		//{
		//	if(transform.position.x < targetPos.x + hokan)
		//	{
		//		switch (direction)
		//		{
		//		case Direction.LEFT:
		//			targetPos = transform.position - new Vector3(1.92f, 0, 0);
		//			break;
		//		case Direction.RIGHT:
		//			targetPos = transform.position + new Vector3(1.92f, 0, 0);
		//			break;
		//		case Direction.UP:
		//			targetPos = transform.position + new Vector3(0, 1.6f, 0);
		//			break;
		//		case Direction.DOWN:
		//			targetPos = transform.position - new Vector3(0, 1.6f, 0);
		//			break;
		//		}
		//	}
		//}
		//// ���Ɉړ���
		//else if (rb.velocity.y < 0)
		//{
		//	if (transform.position.y < targetPos.y + hokan)
		//	{
		//		switch (direction)
		//		{
		//		case Direction.LEFT:
		//			targetPos = transform.position - new Vector3(1.92f, 0, 0);
		//			break;
		//		case Direction.RIGHT:
		//			targetPos = transform.position + new Vector3(1.92f, 0, 0);
		//			break;
		//		case Direction.UP:
		//			targetPos = transform.position + new Vector3(0, 1.6f, 0);
		//			break;
		//		case Direction.DOWN:
		//			targetPos = transform.position - new Vector3(0, 1.6f, 0);
		//			break;
		//		}
		//	}
		//}
		//// �E�Ɉړ���
		//else if (rb.velocity.x > 0)
		//{
		//	if (transform.position.x > targetPos.x - hokan)
		//	{
		//		switch (direction)
		//		{
		//		case Direction.LEFT:
		//			targetPos = transform.position - new Vector3(1.92f, 0, 0);
		//			break;
		//		case Direction.RIGHT:
		//			targetPos = transform.position + new Vector3(1.92f, 0, 0);
		//			break;
		//		case Direction.UP:
		//			targetPos = transform.position + new Vector3(0, 1.6f, 0);
		//			break;
		//		case Direction.DOWN:
		//			targetPos = transform.position - new Vector3(0, 1.6f, 0);
		//			break;
		//		}
		//	}
		//}
		//// ��Ɉړ���
		//else if(rb.velocity.y > 0)
		//{
		//	if (transform.position.y > targetPos.y - hokan)
		//	{
		//		switch (direction)
		//		{
		//		case Direction.LEFT:
		//			targetPos = transform.position - new Vector3(1.92f, 0, 0);
		//			break;
		//		case Direction.RIGHT:
		//			targetPos = transform.position + new Vector3(1.92f, 0, 0);
		//			break;
		//		case Direction.UP:
		//			targetPos = transform.position + new Vector3(0, 1.6f, 0);
		//			break;
		//		case Direction.DOWN:
		//			targetPos = transform.position - new Vector3(0, 1.6f, 0);
		//			break;
		//		}
		//	}			
		//}
		//// ---------------------------------------------
		// �����Ă��Ȃ�����Q�[���I�[�o�[
		//else
		//{
		//	GameObject.FindObjectOfType<GameManager>().GameOver();
		//}



		// �ړ�
		rb.velocity = (targetPos - transform.position).normalized * speed;
		// �ړ����x�X�V
		//speed = Mathf.Lerp(speed, afterSpeed, 0);
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
	void ITrain.Curve(Direction dir)
	{
		direction = dir;
	}
}
