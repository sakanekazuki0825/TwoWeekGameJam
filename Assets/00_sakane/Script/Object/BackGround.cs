using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		// ���C���J�����̈ʒu�擾
		var startPos = Camera.main.transform.position;
		// �ʒu�w��
		transform.position = new Vector3(startPos.x, startPos.y, 0);
	}

	
}
