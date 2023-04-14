using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour,IResult
{
	Text resultTxt;

	private void Awake()
	{
		resultTxt = GetComponentInChildren<Text>();
	}

	// �Q�[���N���A
	void IResult.GameClear()
	{
		resultTxt.text = "GameClear";
	}

	// �Q�[���I�[�o�[
	void IResult.GameOver()
	{
		resultTxt.text = "GameOver";
	}
}
