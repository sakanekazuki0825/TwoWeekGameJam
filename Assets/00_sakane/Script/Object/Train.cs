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
	[SerializeField]
	float acceleration = 0.08f;

	// true = �ړ��ł���
	bool canMove = false;

	// �⊮���x
	//float complement = 0;

	// �~�܂������ɑ�����ۑ�
	Vector3 stopBeforeSpeed = new Vector3 (0, 0, 0);

	// ���e�͈�
	[SerializeField]
	//float tolerance = 0.1f;
	// true = �^�񒆂�ʉ�
	//bool isCenter = false;
	// �ŏI�ړI�n
	Vector3 finalDestination = new Vector3(10, 0, 0);
	// �ړI�n
	Vector3 targetPos = new Vector3(10, 0, 0);
	// �^�[�Q�b�g�̕���
	Vector3 beforeTargetDir = new Vector3(1, 0, 0);

	private void Awake()
	{
		// �����擾
		rb = GetComponent<Rigidbody>();
		// �������x�ݒ�
		speed = startSpeed;

		afterSpeed = startSpeed;
		stopBeforeSpeed = new Vector3(startSpeed, 0, 0);
	}

	private void Update()
	{
		if (!canMove)
		{
			return;
		}
		if (rb.velocity.magnitude == 0)
		{
			GameInstance.gameManager.GameOver();
		}
	}

	private void FixedUpdate()
	{
		// �ړ��ł����Ԃ����ׂ�
		if (!canMove)
		{
			return;
		}

		if ((targetPos - transform.position).normalized != beforeTargetDir.normalized)
		//if (!isCenter && Vector2.Distance(transform.position, curvePos) <= (tolerance /** startSpeed / speed*/))
		{
			// ���e�͈͂ɓ�������ړI�n����
			transform.position = targetPos;
			// �ړI�n�X�V (�����̈ʒu + �ړI�n�̕��� * �摜�T�C�Y)
			targetPos = finalDestination;
			// �^�񒆂�ʉ�
			//isCenter = true;

			beforeTargetDir = targetPos - transform.position;
		}

		// �ړ�
		rb.velocity = beforeTargetDir.normalized * speed;
		// �ړ����x�X�V
		speed = Mathf.Lerp(speed, afterSpeed, acceleration);
	}

	// �i��
	void ITrain.Go()
	{
		canMove = true;
		rb.velocity = stopBeforeSpeed;
	}

	// ��~
	void ITrain.Stop()
	{
		canMove = false;
		stopBeforeSpeed = rb.velocity;
		rb.velocity = Vector3.zero;
	}

	// ���x���グ��
	void ITrain.AddSpeed(float speed)
	{
		afterSpeed += speed;
		// �⊮���x�v�Z
		//complement = (speed + afterSpeed) / 2 / 1.92f;
	}

	// ������ύX
	void ITrain.Curve(Vector3 curvePos, Vector3 targetPos)
	{
		//isCenter = false;

		// �Ȃ���ʒu�ƃp�l���̌q�����Ă���ʒu���擾
		this.targetPos = curvePos;
		finalDestination = targetPos;
		beforeTargetDir = curvePos - transform.position;
	}
}