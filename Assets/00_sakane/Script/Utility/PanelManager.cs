using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �p�l���Ǘ��N���X
public class PanelManager : MonoBehaviour
{
	// ��������p�l���v���n�u
	[SerializeField]
	GameObject panel;

	// �ŏ��ɐ������鉡�Ɉړ�����p�l���̐�
	[SerializeField]
	int startHorizontalPanel = 3;

	// �ŏ��ɐ������鐔
	[SerializeField, Tooltip("x = �ŏ��ɉ��ɐ������鐔�i-3���������j\ny = �c�ɐ������鐔")]
	Vector2 spawnNum = new Vector2(5, 5);

	// �����J�n�ʒu
	[SerializeField]
	Vector2 spawnStartPos = new Vector2(5, 5);

	// ���������p�l��
	List<GameObject> panels = new List<GameObject>();

	// �X�v���C�g�̃T�C�Y
	static Vector2 spriteSize = new Vector2(1.92f, 1.6f);
	public static Vector2 SpriteSize { get { return spriteSize; } }

	private void Start()
	{
		// �X�v���C�g�̃T�C�Y�ݒ�
		spriteSize = panel.GetComponent<SpriteRenderer>().bounds.size;
		InitSpawn();
	}

	private void Update()
	{
		// �X�N���[���̉E���̍��W���擾
		var screenRightDownPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
		// �Ō�̃p�l���̍��[�̈ʒu���擾
		var panelLeftPos = panels[panels.Count - 1].transform.position.x - spriteSize.x / 2;
		// �p�l���̍�����ʂɓ����Ă����ꍇ����
		if(panelLeftPos < screenRightDownPos.x)
		{
			PanelSpawn();
		}
	}

	// �����z�u
	void InitSpawn()
	{
		// �ŏ��ɉ��Ɉړ�����p�l���̐���
		for (int i = 0; i < startHorizontalPanel; ++i)
		{
			// �p�l������
			var insObj = Instantiate(panel, new Vector3(spawnStartPos.x + spriteSize.x * i, spawnStartPos.y + spriteSize.y * Mathf.Floor(spawnNum.y / 2), 0), Quaternion.identity);
			// ���������p�l���ۑ�
			panels.Add(insObj);
			// �p�l���ɕ�����ݒ�
			insObj.GetComponent<IPanel>().SetLinkDirection(new List<Vector2> { Vector2.left, Vector2.right });
		}
		// �����z�u
		for (int i = 0; i < spawnNum.x; ++i)
		{
			PanelSpawn();
		}
	}

	// �p�l������
	public void PanelSpawn()
	{
		// �Ō�ɒǉ������I�u�W�F�N�g�̈ʒu�擾
		var lastObjPos = panels[panels.Count - 1].transform.position;
		// �����ʒu�擾
		var spawnPos = new Vector2(lastObjPos.x + spriteSize.x, spriteSize.y);
		for (int i = 0; i < spawnNum.y; ++i)
		{
			// �p�l������
			var insObj = Instantiate(panel, new Vector2(lastObjPos.x + spriteSize.x, spriteSize.y * i + spawnStartPos.y), Quaternion.identity);
			// ����
			panels.Add(insObj);

			// �q�����Ă�����������߂�
		}
	}
}