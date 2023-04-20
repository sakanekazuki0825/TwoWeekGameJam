using System.Collections.Generic;
using UnityEngine;

public struct GameInstance
{
	// 方向
	public enum Direction
	{
		UP    = 0,
		RIGHT = 1,
		LEFT  = 2,
		DOWN  = 3
	}

	// 線路のパターン
	public static List<List<Vector2>> linePattern =
		new List<List<Vector2>>()
		{
			new List<Vector2> { Vector2.up,		Vector2.right }, // 右と上
			new List<Vector2> { Vector2.up,		Vector2.left },  // 上と左
			new List<Vector2> { Vector2.right,  Vector2.left },  // 右と左
			new List<Vector2> { Vector2.right,	Vector2.down },  // 右と下
			new List<Vector2> { Vector2.down,	Vector2.left },  // 下と左
			new List<Vector2> { Vector2.down,	Vector2.up },    // 下と上
		};

#if UNITY_EDITOR
	public static bool isDebug = true;
#endif

	public static Player player = null;
	// マネージャー
	public static GameManager gameManager = null;
	public static PanelManager panelManager = null;

	public static FadeIO fadeIO = null;
	public static CountDown countDown = null;
	public static DistanceToGoal distanceToGoal = null;
}