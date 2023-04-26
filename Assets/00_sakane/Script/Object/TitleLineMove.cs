using UnityEngine;
using UnityEngine.UI;

public class TitleLineMove : MonoBehaviour
{
	[SerializeField]
	float speed;

	Image img;

	private void Start()
	{
		img = GetComponent<Image>();
		//img.
	}

	private void Update()
	{
		var pos = Camera.main.ScreenToWorldPoint(transform.position);
		transform.Translate(speed, 0, 0);
		if (transform.position.x < pos.x)
		{
			
		}
	}
}
