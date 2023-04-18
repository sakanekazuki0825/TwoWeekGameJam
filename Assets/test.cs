using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		Debug.Log("0|1 = " + (0 & 1));
		Debug.Log("0|2 = " + (0 & 2));
		Debug.Log("0|3 = " + (0 & 3));
		Debug.Log("0|4 = " + (0 & 4));
		Debug.Log("1|2 = " + (1 & 2));
		Debug.Log("1|3 = " + (1 & 3));
		Debug.Log("1|4 = " + (1 & 4));
		Debug.Log("2|3 = " + (2 & 3));
		Debug.Log("2|4 = " + (2 & 4));
		Debug.Log("3|4 = " + (3 & 4));
	}
}