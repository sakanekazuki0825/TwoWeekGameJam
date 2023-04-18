using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour,ICameraMove
{
	// 追いかけるオブジェクト
	GameObject target;
	// 追いかけるオフセット
	Vector3 offset = new Vector3(0, 0, 0);

	private void Start()
	{
		// オフセットの設定
		offset = transform.position - target.transform.position;
	}

	void Update()
	{
		var position = target.transform.position + offset;
		position.y = transform.position.y;
		// カメラを固定
		transform.position = position;
	}

	void ICameraMove.SetTarget(UnityEngine.GameObject target)
	{
		this.target = target;
	}
}
