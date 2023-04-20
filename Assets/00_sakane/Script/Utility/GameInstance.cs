using System.Collections.Generic;
using UnityEngine;

public struct GameInstance
{
	// ����
	public enum Direction
	{
		UP    = 0,
		RIGHT = 1,
		LEFT  = 2,
		DOWN  = 3
	}

	// ���H�̃p�^�[��
	public static List<List<Vector2>> linePattern =
		new List<List<Vector2>>()
		{
			new List<Vector2> { Vector2.up,		Vector2.right }, // �E�Ə�
			new List<Vector2> { Vector2.up,		Vector2.left },  // ��ƍ�
			new List<Vector2> { Vector2.right,  Vector2.left },  // �E�ƍ�
			new List<Vector2> { Vector2.right,	Vector2.down },  // �E�Ɖ�
			new List<Vector2> { Vector2.down,	Vector2.left },  // ���ƍ�
			new List<Vector2> { Vector2.down,	Vector2.up },    // ���Ə�
		};

#if UNITY_EDITOR
	public static bool isDebug = true;
#endif

	public static Player player = null;
	// �}�l�[�W���[
	public static GameManager gameManager = null;
	public static PanelManager panelManager = null;

	public static FadeIO fadeIO = null;
	public static CountDown countDown = null;
	public static DistanceToGoal distanceToGoal = null;
}