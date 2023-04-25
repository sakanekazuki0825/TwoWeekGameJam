using UnityEngine;

public class HoveredTriangle : MonoBehaviour
{
	[SerializeField]
	float speed;

	void Update()
	{
		transform.Rotate(0, speed, 0);
	}
}