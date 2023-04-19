using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
	// true = �J�E���g��
	bool isCounting;
	public bool IsCounting{ get => isCounting; }
	// �Q�[���J�n���̎���
	[SerializeField]
	float startCountDownTime = 3;
	// �I�v�V������ʂȂǂ�������̎���
	[SerializeField]
	float inGameCountDownTime = 3;
	// �Ō�̃e�L�X�g��\�����鎞��
	[SerializeField]
	float displayTime = 1;
	// �Ō�ɕ\�����镶��
	[SerializeField]
	string lastStr;
	// �J�E���g�_�E����\������e�L�X�g
	[SerializeField]
	Text countTxt;

	private void Awake()
	{
		GameInstance.countDown = this;
	}

	private void OnDestroy()
	{
		GameInstance.countDown = null;
	}

	// �J�E���g�_�E���J�n
	public void StartCountDown(bool isInGame = true)
	{
		StartCoroutine(IStartCountDown(isInGame));
	}

	// �J�E���g�_�E���R���[�`��
	IEnumerator IStartCountDown(bool isInGame)
	{
		isCounting = true;
		var time = inGameCountDownTime;
		if (!isInGame)
		{
			time = startCountDownTime;
		}
		while (isCounting)
		{
			time -= Time.deltaTime;
			countTxt.text = Mathf.Ceil(time).ToString();
			if (time <= 0)
			{
				countTxt.text = lastStr;
				isCounting = false;
				break;
			}
			yield return null;
		}
		yield return new WaitForSeconds(displayTime);
		gameObject.SetActive(false);
	}
}
