using UnityEngine;

public class LoadingGear : MonoBehaviour
{
	// ��]���x
	[SerializeField]
	float speed;

	private void Update()
	{
		transform.Rotate(0, 0, speed);
	}
}
