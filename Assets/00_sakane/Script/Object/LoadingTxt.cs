using UnityEngine;
using UnityEngine.UI;

public class LoadingTxt : MonoBehaviour
{
	Text message;

	float time = 0;

	Color baseColor;

	private void Awake()
	{
		message = GetComponent<Text>();
		baseColor = message.color;
	}

	private void Update()
	{
		time += Time.deltaTime;

		message.color = new Color(baseColor.r, baseColor.g, baseColor.b, Mathf.Sin(time));
	}
}
