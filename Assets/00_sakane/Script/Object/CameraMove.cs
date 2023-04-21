using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour, ICameraMove
{
	// 追いかけるオブジェクト
	GameObject target;
	// 追いかけるオフセット
	Vector3 offset = new Vector3(0, 0, 0);
	// true = 動くことができる
	bool canMove = false;

	void Update()
	{
		if (!canMove)
		{
			return;
		}
		var position = target.transform.position + offset;
		position.y = transform.position.y;
		// カメラを固定
		transform.position = position;
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
}
