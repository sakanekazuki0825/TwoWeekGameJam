using UnityEngine;

public class Crash : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Train"))
		{
			other.GetComponent<ITrain>().Crash();
		}
	}
}
