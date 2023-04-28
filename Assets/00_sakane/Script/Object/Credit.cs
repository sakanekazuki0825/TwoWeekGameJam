using UnityEngine;

public class Credit : MonoBehaviour
{
	[SerializeField]
	GameObject creditTxt;

	[SerializeField]
	float speed;

	[SerializeField]
	float goalPos;

	Vector3 startPos;

	private void OnEnable()
	{
		startPos = creditTxt.transform.position;
	}

	private void FixedUpdate()
	{
		if(creditTxt.transform.position.y > goalPos)
		{
			Invoke("Close", 1);
		}
		else
		{
			creditTxt.transform.Translate(0, speed, 0);
		}
	}

	private void OnDisable()
	{
		creditTxt.transform.position = startPos;
	}


	void Close()
	{
		gameObject.SetActive(false);
	}
}
