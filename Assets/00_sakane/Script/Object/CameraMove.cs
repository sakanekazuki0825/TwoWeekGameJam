using System.Collections;
using UnityEngine;

public class CameraMove : MonoBehaviour, ICameraMove
{
	// 追いかけるオブジェクト
	GameObject target;
	// 追いかけるオフセット
	Vector3 offset = new Vector3(0, 0, 0);
	// true = 動くことができる
	bool canMove = false;

	// ゴールの位置
	float goalPosX;

	void Update()
	{
		if (!canMove)
		{
			return;
		}
		if (target != null)
		{
			var position = target.transform.position + offset;
			position.y = transform.position.y;
			// カメラを固定
			transform.position = position;
		}
	}

	// ターゲットの設定
	void ICameraMove.SetTarget(UnityEngine.GameObject target)
	{
		this.target = target;
	}

	// 状態設定
	void ICameraMove.SetCanMove(bool canMove)
	{
		this.canMove = canMove;
		// オフセットの設定
		offset = transform.position - target.transform.position;
	}

	// ゴールの位置設定
	void ICameraMove.SetGoalPos(float goalPos)
	{
		goalPosX = goalPos;
		StartCoroutine(GoGoalPos());
	}

	IEnumerator GoGoalPos()
	{
		yield return new WaitUntil(
			() =>
			{
				return transform.position.x > goalPosX;
			});
		transform.position = new Vector3(goalPosX, transform.position.y, transform.position.z);
		canMove = false;
	}
}
