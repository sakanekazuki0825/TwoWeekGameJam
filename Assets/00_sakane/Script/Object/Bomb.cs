using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
	Animator animator;
	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	public void DelObject()
	{
		Destroy(gameObject);
	}
}
