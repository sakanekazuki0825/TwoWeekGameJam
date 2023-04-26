using System.Collections;
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
	float speed = 0.1f;
	// �ŏI�I�ɓ��B���鑬�x
	float afterSpeed;
	[SerializeField]
	float acceleration = 0.08f;

	// true = �ړ��ł���
	bool canMove = false;

	// �⊮���x
	//float complement = 0;

	// �~�܂������ɑ�����ۑ�
	Vector3 stopBeforeSpeed = new Vector3(0, 0, 0);

	// �ŏI�ړI�n
	Vector3 finalDestination = new Vector3(10, 0, 0);
	// �ړI�n
	Vector3 targetPos = new Vector3(10, 0, 0);
	// �^�[�Q�b�g�̕���
	Vector3 beforeTargetDir = new Vector3(1, 0, 0);

	// ���������o�����
	[SerializeField]
	ParticleSystem dashEffect;
	[SerializeField]
	float velMag;

	// �A�j���[�^�[
	Animator animator;

	// true = �X�N���[�����o��
	bool isScreenOut = false;
	public bool IsScreenOut { get => isScreenOut; }

	// �����G�t�F�N�g
	[SerializeField]
	GameObject crashEffect;

	AudioSource source;

	BoxCollider boxCol;

	Vector3 leftFacingCenter = new Vector3(0, 0.8f, 0);
	Vector3 leftFacingSize = new Vector3(8, 4, 0.2f);

	Vector3 upFacingCenter = new Vector3(0, 0, 0);
	Vector3 upFacingSize = new Vector3(3, 6.5f, 0.2f);

	private void Awake()
	{
		// �����擾
		rb = GetComponent<Rigidbody>();
		// �������x�ݒ�
		//speed = startSpeed;

		afterSpeed = startSpeed;
		stopBeforeSpeed = new Vector3(0.01f, 0, 0);

		// �A�j���[�^�[�擾
		animator = GetComponent<Animator>();
		animator.speed = 0;
		source = GetComponent<AudioSource>();
		boxCol = GetComponent<BoxCollider>();
	}

	private void Start()
	{
		dashEffect.gameObject.SetActive(false);
		GameObject.FindObjectOfType<SpeedMeter>().SetTrainRigidbody(rb);
	}

	private void Update()
	{
		if (!canMove)
		{
			return;
		}
		if (rb.velocity == Vector3.zero)
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

			if((beforeTargetDir.normalized.y > 0.5f) || (beforeTargetDir.normalized.y < -0.5f))
			{
				boxCol.center = upFacingCenter;
				boxCol.size = upFacingSize;
			}
			else
			{
				boxCol.center = leftFacingCenter;
				boxCol.size = leftFacingSize;
			}
		}

		animator.SetFloat("speed", beforeTargetDir.y);
		// �A�j���[�V�������x�X�V
		animator.speed = (speed / startSpeed);
		var pmain = dashEffect.main;
		pmain.simulationSpeed = (speed / (startSpeed * 5));
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
		source.UnPause();
	}

	// ��~
	void ITrain.Stop()
	{
		canMove = false;
		stopBeforeSpeed = rb.velocity;
		rb.velocity = Vector3.zero;
		animator.speed = 0;
		var pmain = dashEffect.main;
		pmain.simulationSpeed = 0;
		source.Pause();
	}

	// ���x���グ��
	void ITrain.AddSpeed(float speed)
	{
		afterSpeed += speed;

		if (afterSpeed > velMag)
		{
			dashEffect.gameObject.SetActive(true);
		}
		else
		{
			dashEffect.gameObject.SetActive(false);
		}

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

	// �Q�[���N���A
	void ITrain.GameClear(Vector3 goalPos)
	{
		StartCoroutine(EGameClear(goalPos));
		source.Pause();
	}

	// �Q�[���N���A�R���[�`��
	IEnumerator EGameClear(Vector3 goalPos)
	{
		canMove = false;
		var pmain = dashEffect.main;
		pmain.simulationSpeed = 0;
		//while(goalPos.x < transform.position.x)
		//{
		yield return new WaitUntil(
			() =>
			{
				rb.velocity = (goalPos - transform.position) * speed;
				animator.speed = rb.velocity.magnitude / startSpeed;
				return goalPos.x <= transform.position.x + 0.01f;
			});
		transform.position = new Vector3(goalPos.x, transform.position.y, transform.position.z);
		animator.speed = 0;
		dashEffect.gameObject.SetActive(false);
		rb.velocity = Vector3.zero;
		//}
	}

	// �^�C�g����
	void ITrain.GoTitle()
	{
		StartCoroutine(EGoTitle());
	}

	IEnumerator EGoTitle()
	{
		source.UnPause();
		var pos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.75f, 0, -Camera.main.transform.position.z));
		animator.speed = startSpeed;
		rb.velocity = new Vector3(startSpeed, 0, 0);
		yield return new WaitUntil(
			() =>
			{
				return transform.position.x >= pos.x;
			});
		isScreenOut = true;
	}

	void ITrain.Crash()
	{
		Instantiate(crashEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	bool ITrain.IsScreenOut()
	{
		return isScreenOut;
	}
}