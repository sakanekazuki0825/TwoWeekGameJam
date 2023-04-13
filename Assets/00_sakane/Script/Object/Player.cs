using UnityEngine;

// �v���C���[�N���X
public class Player : MonoBehaviour,IPlayer
{
	// �I�𒆂̃I�u�W�F�N�g
	GameObject nowSelectObj;
	// ��������I�u�W�F�N�g
	GameObject changeObj;

	// true = �I�u�W�F�N�g�I��
	bool isSelect = false;
	// �I�������ۂ̃I�u�W�F�N�g�̈ʒu
	Vector3 mousePos;

	// true = �������Ƃ��ł���
	bool canOperation = false;

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
				// �I�����Ă���I�u�W�F�N�g�������ꍇ���
				if (nowSelectObj == null)
				{
					nowSelectObj = hit.collider.gameObject;
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
				}
			}
			// �I�����Ă��Ȃ���Ԃɂ���
			isSelect = false;
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