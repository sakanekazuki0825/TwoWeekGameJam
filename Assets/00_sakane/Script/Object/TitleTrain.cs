using System.Collections;
using UnityEngine;

public class TitleTrain : MonoBehaviour
{
	// 移動速度
	[SerializeField]
	float speed;

	// 止まる位置
	[SerializeField]
	float stopPosX;

	[SerializeField]
	// ゴールの位置
	float goalPosX;
	// true = ゴール
	bool isGoal = false;

	RectTransform recTra;

	private void Awake()
	{
		recTra = transform as RectTransform;
		EnterTheScreen();
	}

	void EnterTheScreen()
	{
		StartCoroutine(EEnterTheScreen());
	}

	IEnumerator EEnterTheScreen()
	{
		yield return new WaitUntil(
			() =>
			{
				recTra.Translate(speed, 0, 0);
				return recTra.localPosition.x > stopPosX;
			});
	}

	public void Move()
	{
		StartCoroutine(EMove());
	}

	IEnumerator EMove()
	{
		isGoal = false;

		while (true)
		{
			recTra.Translate(new Vector3(speed, 0, 0));
			//transform.Translate(new Vector3(speed, 0, 0));
			if (recTra.position.x >= goalPosX && !isGoal)
			{
				isGoal = true;
				GameInstance.titleManager.GameStart();
			}
			yield return null;
		}
	}
}