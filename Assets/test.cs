using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
	[SerializeField]
	float n;

	// Start is called before the first frame update
	void Start()
	{
		var speed = 0f;
		if (n % 1 >= 0.5f)
		{
			speed = Mathf.Ceil(n);
		}
		else
		{
			speed = Mathf.Floor(n);
		}
		Debug.Log(speed);
	}
}