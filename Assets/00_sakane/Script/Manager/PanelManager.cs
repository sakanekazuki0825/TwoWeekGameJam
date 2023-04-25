using System.Collections.Generic;
using UnityEngine;

// �p�l���Ǘ��N���X
public class PanelManager : MonoBehaviour
{
	// �p�l���̃X�v���C�g
	[SerializeField, Tooltip("0.UP & RIGHT\n1.UP & Left\n2.LEFT & RIGHT\n3.RIGHT & DOWN\n4.LEFT & DOWN\n5.UP & DOWN")]
	List<Sprite> panelSprites = new List<Sprite>();
	// ��������p�l���v���n�u
	[SerializeField]
	GameObject panel;
	// �S�[���p�l���v���n�u
	[SerializeField]
	GameObject goalPanel;

	// �ŏ��ɐ������鉡�Ɉړ�����p�l���̐�
	[SerializeField]
	int startHorizontalPanel = 3;
	public int StartHorizontalPanel { get => startHorizontalPanel; }

	// �ŏ��ɐ������鐔
	[SerializeField, Tooltip("x = �ŏ��ɉ��ɐ������鐔�i-3���������j\ny = �c�ɐ������鐔")]
	Vector2 spawnNum = new Vector2(5, 5);

	// �����J�n�ʒu
	[SerializeField]
	Vector2 spawnStartPos = new Vector2(5, 5);

	// ���������p�l��
	List<GameObject> panels = new List<GameObject>();

	// �X�v���C�g�̃T�C�Y
	Vector2 spriteSize = new Vector2(1.92f, 1.6f);
	public Vector2 SpriteSize { get => spriteSize; }

	// �����ŏオ�鑬�x
	[SerializeField]
	float speedUpValue = 0.4f;

	// �J�[�u�ŉ����鑬�x
	[SerializeField]
	float speedDownValue = -0.4f;

	// �S�[���܂ł̃p�l���̐��iX�����̂݌v�Z�j
	[SerializeField]
	int numberToGoal = 100;
	public int NumberToGoal{ get => numberToGoal; }
	int nowNumber = 0;
	// ���������S�[���I�u�W�F�N�g
	List<GameObject> goalObj = new List<GameObject>();

	// �p�l���������Ɏg�p����{�b�N�X�K�`���݂����Ȃ��
	[SerializeField]
	PanelBoxData panelBoxData;
	// 1�{�b�N�X�̃p�l���̐�
	[SerializeField]
	int oneBoxPanelNum = 20;
	// �p�l���{�b�N�X
	List<int> panelBox = new List<int>();
	// �{�b�N�X�̔ԍ�
	int boxNumber = 0;

	// ����Ă������N���Ȃ��^�C��
	[SerializeField]
	GameObject tile;
	[SerializeField]
	int tileNumber = 3;

	// �E��
	[SerializeField]
	GameObject crashObj;
	// �S�[���̔w�i
	[SerializeField]
	GameObject goalBackGroundObj;

	private void Awake()
	{
		GameInstance.panelManager = this;
		for (int i = 0; i < oneBoxPanelNum; ++i)
		{
			panelBox.Add(panelBoxData.PanelDataList[i]);
		}
	}

	private void Start()
	{
		// �X�v���C�g�̃T�C�Y�ݒ�
		spriteSize = panel.GetComponent<SpriteRenderer>().bounds.size;
		GameInstance.distanceToGoal.Goal = spriteSize * NumberToGoal;
		InitSpawn();
	}

	private void Update()
	{
		// �X�N���[���̉E���̍��W���擾
		var screenRightDownPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10));
		// �Ō�̃p�l���̍��[�̈ʒu���擾
		var panelLeftPos = panels[panels.Count - 1].transform.position.x - spriteSize.x / 2;
		// �p�l���̍�����ʂɓ����Ă����ꍇ����
		if (panelLeftPos < screenRightDownPos.x)
		{
			PanelSpawn();
		}
	}

	private void OnDestroy()
	{
		GameInstance.panelManager = null;
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
		for (int i = 0; i < spawnNum.x; ++i)
		{
			if (i == 2)
			{
				continue;
			}
			Instantiate(crashObj, new Vector2(spawnStartPos.x + spriteSize.x * 2, spawnStartPos.y + spriteSize.y * i), Quaternion.identity);
		}

		// �����z�u
		for (int i = 0; i < spawnNum.x; ++i)
		{
			PanelSpawn();
		}
		for (int i = 0; i < tileNumber; ++i)
		{
			var pos = panels[0].transform.position;
			Instantiate(tile, new Vector3(pos.x - spriteSize.x * i, pos.y, pos.z), Quaternion.identity);
		}
	}

	// �p�l������
	public void PanelSpawn()
	{
		// �Ō�ɒǉ������I�u�W�F�N�g�̈ʒu�擾
		var lastObjPos = panels[panels.Count - 1].transform.position;
		// �����ʒu�擾
		var spawnPosX = lastObjPos.x + spriteSize.x;
		// �S�[��
		if (nowNumber == (numberToGoal - 1))
		{
			// �S�[���̐�����1��̂�
			if (goalObj.Count > 0)
			{
				spawnPosX = goalObj[goalObj.Count - 1].transform.position.x + spriteSize.x;
				for (int i = 0; i < spawnNum.y; ++i)
				{
					goalObj.Add(Instantiate(tile, new Vector2(spawnPosX, spriteSize.y * i + spawnStartPos.y), Quaternion.identity));
				}
				return;
			}
			Instantiate(goalBackGroundObj, new Vector2(spawnPosX - spriteSize.x/2, spriteSize.y), Quaternion.identity);
			for (int i = 0; i < spawnNum.y; ++i)
			{
				goalObj.Add(Instantiate(goalPanel, new Vector2(spawnPosX, spriteSize.y * i + spawnStartPos.y), Quaternion.identity));
				Camera.main.GetComponent<ICameraMove>().SetGoalPos(spawnPosX);
			}
			return;
		}
		Instantiate(crashObj, new Vector2(spawnPosX, spawnStartPos.y - spriteSize.y), Quaternion.identity);
		Instantiate(crashObj, new Vector2(spawnPosX, spawnStartPos.y + spriteSize.y * spawnNum.y), Quaternion.identity);
		for (int i = 0; i < spawnNum.y; ++i)
		{
			// �p�l������
			var insObj = Instantiate(panel, new Vector2(spawnPosX, spriteSize.y * i + spawnStartPos.y), Quaternion.identity);
			// ����
			panels.Add(insObj);

			//// �q�����Ă�����������߂�i-1���Ē��i�p�l����1���Ƃ��Ĉ����j
			//var lineNumber = Random.Range(0, panelSprites.Count - 1);

			//if(lineNumber == 2)
			//{
			//	// �����̏ꍇ�����_���ŏc�����߂�
			//	var straight = Random.Range(0, 2);
			//	// 3�̔{���ɂ��āA-1����Əc�Ɖ��̔ԍ��ɂȂ�
			//	lineNumber = (straight + 1) * 3 - 1;
			//}

			// �{�b�N�X�K�`���V�X�e��
			var number = Random.Range(0, panelBox.Count);
			var lineNumber = panelBox[number];

#if UNITY_EDITOR
			// �f�o�b�O
			if (GameInstance.isDebug)
			{
				lineNumber = 2;
			}
#endif

			// �X�v���C�g�̐ݒ�
			insObj.GetComponent<IPanel>().SetSprite(panelSprites[lineNumber]);
			// �q�����Ă���ꏊ���w��
			insObj.GetComponent<IPanel>().SetLinkDirection(GameInstance.linePattern[lineNumber]);

			// �ω������鑬�x�̐ݒ�
			if (lineNumber == 2 || lineNumber == 5)
			{
				insObj.GetComponent<IPanel>().SetChangeSpeedValue(speedUpValue);
			}
			else
			{
				insObj.GetComponent<IPanel>().SetChangeSpeedValue(speedDownValue);
			}

			// ���������p�l���͍폜
			panelBox.Remove(panelBox[number]);
			// �p�l�����Ȃ��Ȃ����ꍇ
			if (panelBox.Count <= 0)
			{
				++boxNumber;
				var dataNumber = boxNumber * oneBoxPanelNum;
				if (panelBoxData.PanelDataList.Count <= dataNumber)
				{
					dataNumber = 0;
				}
				for (int pn = 0; pn < oneBoxPanelNum; ++pn)
				{
					panelBox.Add(panelBoxData.PanelDataList[dataNumber + pn]);
				}
			}
		}

		// ������������ǉ�
		++nowNumber;
	}
}