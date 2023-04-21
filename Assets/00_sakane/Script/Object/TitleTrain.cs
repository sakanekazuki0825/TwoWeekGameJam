using System.Collections;
using UnityEngine;

public class TitleTrain : MonoBehaviour
{
	// �ړ����x
	[SerializeField]
	float speed;

	[SerializeField]
	// �S�[���̈ʒu
	float goalPosX;
	// true = �S�[��
	bool isGoal = false;

	RectTransform recTra;

	private void Awake()
	{
		recTra = transform as RectTransform;
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
			if (recTra.position.x >= goalPosX)
			{
				isGoal = true;
			}
			yield return null;
		}
		isGoal = true;
		GameInstance.titleManager.GameStart();
	}
}