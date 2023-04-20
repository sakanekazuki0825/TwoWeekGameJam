using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		// メインカメラの位置取得
		var startPos = Camera.main.transform.position;
		// 位置指定
		transform.position = new Vector3(startPos.x, startPos.y, 0);
	}

	
}
