using UnityEngine;
using UnityEngine.UI;

public class SpeedMeter : MonoBehaviour
{
	// �X�s�[�h��\������e�L�X�g
	Text meterTxt;

	// �d�Ԃ̃��W�b�h�{�f�B
	Rigidbody trainRb;

	[SerializeField]
	float n = 50;

	private void Awake()
	{
		meterTxt = GetComponent<Text>();
	}

	private void Update()
	{
		if (trainRb != null)
		{
			var speed = 0f;
			if (trainRb.velocity.magnitude % 1 >= 0.5f)
			{
				speed = Mathf.Ceil(trainRb.velocity.magnitude * n);
			}
			else
			{
				speed = Mathf.Floor(trainRb.velocity.magnitude * n);
			}
			if(speed < 49)
			{
				meterTxt.color = Color.green;
			}
			else if(speed < 99)
			{
				meterTxt.color = Color.yellow;
			}
			else
			{
				meterTxt.color = Color.red;
			}
			meterTxt.text = speed.ToString();
		}
		else
		{
			meterTxt.text = "0";
		}
	}

	// ���W�b�h�{�f�B�ݒ�
	public void SetTrainRigidbody(Rigidbody rigidbody)
	{
		trainRb = rigidbody;
	}
}
