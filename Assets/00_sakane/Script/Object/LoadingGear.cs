using UnityEngine;

public class LoadingGear : MonoBehaviour
{
	// ‰ñ“]‘¬“x
	[SerializeField]
	float speed;

	private void Update()
	{
		transform.Rotate(0, 0, speed);
	}
}
