using UnityEngine;
using UnityEngine.UI;

public class TitleLineMove : MonoBehaviour
{
	[SerializeField]
	float speed = 1;

	Image img;

	RectTransform rectTransform;
	float xsize;

	private void Awake()
	{
		rectTransform = transform as RectTransform;
	}

	private void Start()
	{
		img = GetComponent<Image>();
		xsize = rectTransform.rect.size.x;
	}

	private void Update()
	{
		//var lsftPos = Camera.main.viewportpoint(new Vector3(0, 0, 10));
		
		var nowPos = transform.position;
		transform.Translate(-speed, 0, 0);
		if (rectTransform.position.x + xsize / 2 < 0)
		{
			rectTransform.position = new Vector3(Screen.width + xsize / 2, nowPos.y, nowPos.z);
		}
	}
}
