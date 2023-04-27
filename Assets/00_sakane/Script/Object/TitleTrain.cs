using System.Collections;
using UnityEngine;

public class TitleTrain : MonoBehaviour
{
	// �ړ����x
	[SerializeField]
	float speed;

	float nowSpeed = 0;

	// �~�܂�ʒu
	[SerializeField]
	float stopPosX;

	[SerializeField]
	// �S�[���̈ʒu
	float goalPosX;
	// true = �S�[��
	bool isGoal = false;

	RectTransform recTra;

	[SerializeField]
	float time;

	// �I���ł��Ȃ��L�����o�X
	[SerializeField]
	GameObject noSelectedObj;

	private void Awake()
	{
		recTra = transform as RectTransform;
		noSelectedObj.SetActive(false);
		EnterTheScreen();
	}

	private void FixedUpdate()
	{
		recTra.Translate(nowSpeed, 0, 0);
	}

	void EnterTheScreen()
	{
		StartCoroutine(EEnterTheScreen());
	}

	IEnumerator EEnterTheScreen()
	{
		nowSpeed = speed;
		yield return new WaitUntil(
			() =>
			{
				return recTra.localPosition.x > stopPosX;
			});
		GetComponent<Animator>().speed = 0;
		nowSpeed = 0;
	}

	public void Move()
	{
		StartCoroutine(EMove());
	}

	IEnumerator EMove()
	{
		isGoal = false;
		//var time = 0.0f;
		//yield return new WaitUntil(
		//() =>
		//{
		//	recTra.Translate(new Vector3(speed, 0, 0));
		//	time += Time.deltaTime;
		//	return time > this.time;
		//});
		//GameInstance.titleManager.GameStart();
		noSelectedObj.SetActive(true);
		var anim = GetComponent<Animator>();
		var pos = recTra.position.x + goalPosX;

		while (true)
		{
			nowSpeed = speed;
			anim.speed = 1;
			//transform.Translate(new Vector3(speed, 0, 0));
			if (recTra.position.x >= pos && !isGoal)
			{
				isGoal = true;
				GameInstance.titleManager.GameStart();
			}
			yield return null;
		}
	}
}