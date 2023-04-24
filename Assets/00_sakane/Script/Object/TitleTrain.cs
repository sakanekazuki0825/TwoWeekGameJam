using System.Collections;
using UnityEngine;

public class TitleTrain : MonoBehaviour
{
	// ˆÚ“®‘¬“x
	[SerializeField]
	float speed;

	// Ž~‚Ü‚éˆÊ’u
	[SerializeField]
	float stopPosX;

	[SerializeField]
	// ƒS[ƒ‹‚ÌˆÊ’u
	float goalPosX;
	// true = ƒS[ƒ‹
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