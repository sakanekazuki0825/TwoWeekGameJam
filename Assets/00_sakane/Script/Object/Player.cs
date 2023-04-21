using UnityEngine;

// �v���C���[�N���X
public class Player : MonoBehaviour,IPlayer
{
	// �I�𒆂̃I�u�W�F�N�g
	GameObject nowSelectObj;
	public GameObject NowSelectObj{ get => nowSelectObj; }
	// ��������I�u�W�F�N�g
	GameObject changeObj;

	// true = �I�u�W�F�N�g�I��
	bool isSelect = false;
	// �I�������ۂ̃I�u�W�F�N�g�̈ʒu
	Vector3 mousePos;

	// true = �������Ƃ��ł���
	bool canOperation = false;

	// �I�����Ă���p�l���ɕ\������t���[��
	[SerializeField]
	GameObject selectPanelFrame;
	GameObject frame;

	private void Awake()
	{
		GameInstance.player = this;
	}

	private void Update()
	{
		// ����ł����ԂłȂ��ꍇ�͓��͂��Ƃ�Ȃ�
		if (!canOperation)
		{
			return;
		}

		// ���N���b�N�����ۂ̃}�E�X�̈ʒu��ݒ�
		if (Input.GetMouseButtonDown(0))
		{
			mousePos = Input.mousePosition;
			isSelect = true;
		}
	}

	private void FixedUpdate()
	{
		if (!canOperation)
		{
			// ����ł��Ȃ��̂Ŗ߂�
			return;
		}
		if (isSelect)
		{
			// �q�b�g���
			RaycastHit hit;
			// ���C���
			var ray = Camera.main.ScreenPointToRay(mousePos);
			// ���C�ɂ������Ă��邩
			if (Physics.Raycast(ray.origin, ray.direction, out hit))
			{
				var hitObj = hit.collider.gameObject;
				// 
				if (!hitObj.CompareTag("Panel"))
				{
					return;
				}
				if (hitObj.GetComponent<IPanel>().IsOnTrain())
				{
					return;
				}

				// �I�����Ă���I�u�W�F�N�g�������ꍇ���
				if (nowSelectObj == null)
				{
					nowSelectObj = hit.collider.gameObject;
					frame = Instantiate(selectPanelFrame, nowSelectObj.transform.position, Quaternion.identity);
				}
				// �I�u�W�F�N�g��I�����Ă���ꍇ
				else
				{
					// ��������I�u�W�F�N�g�擾
					changeObj = hit.collider.gameObject;
					// �ŏ��ɑI�������I�u�W�F�N�g�̈ʒu�擾
					var selObjPos = nowSelectObj.transform.position;
					// ��ɑI�������I�u�W�F�N�g�̈ʒu�擾
					var changePos = changeObj.transform.position;

					// �ʒu����
					nowSelectObj.transform.position = changePos;
					changeObj.transform.position = selObjPos;

					// �I�����Ă���I�u�W�F�N�g���O��
					nowSelectObj = null;
					changeObj = null;
					// �I�𒆃t���[���폜
					Destroy(frame);
				}
			}
			// �I�����Ă��Ȃ���Ԃɂ���
			isSelect = false;
		}
	}

	private void OnDestroy()
	{
		GameInstance.player = null;
	}

	// �I�𒆂̃p�l��������
	public void SelectPanelRemove(GameObject obj)
	{
		if (obj == nowSelectObj)
		{
			// �I�����Ă���I�u�W�F�N�g���O��
			nowSelectObj = null;
			// �I�𒆃t���[���폜
			Destroy(frame);
		}
	}

	// �Q�[���J�n�ʒm
	void IPlayer.GameStart()
	{
		canOperation = true;
	}

	// �Q�[���I���ʒm
	void IPlayer.GameFinish()
	{
		canOperation = false;
	}

	// �Q�[����~
	void IPlayer.GameStop()
	{
		canOperation = false;
	}
}