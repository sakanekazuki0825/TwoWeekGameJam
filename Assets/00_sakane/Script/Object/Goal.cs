using UnityEngine;

// ÉSÅ[Éã
public class Goal : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Train"))
		{
			GameObject.FindObjectOfType<GameManager>().GameClear();
		}
	}
}
