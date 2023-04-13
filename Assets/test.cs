using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		Debug.Log(GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10)));
	}
}
