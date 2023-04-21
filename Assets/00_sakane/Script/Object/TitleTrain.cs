using System.Collections;
using UnityEngine;

public class TitleTrain : MonoBehaviour
{
	// �ړ����x
	[SerializeField]
	float speed;

	// �S�[���̈ʒu
	Vector3 goalPos;
	// true = �S�[��
	bool isGoal = false;

	private void Start()
	{
		goalPos = transform.parent.GetComponent<RectTransform>().rect.size * 2.6f;
	}

	public void Move()
	{
		StartCoroutine(EMove());
	}

	IEnumerator EMove()
	{
		isGoal = false;


		while (!isGoal)
		{
			transform.Translate(new Vector3(speed, 0, 0));
			if (transform.position.x >= goalPos.x)
			{
				isGoal = true;
			}
			yield return null;
		}
		isGoal = true;
		GameInstance.titleManager.GameStart();
	}
}