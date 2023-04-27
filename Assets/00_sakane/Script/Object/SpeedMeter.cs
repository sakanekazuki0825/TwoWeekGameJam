using UnityEngine;
using UnityEngine.UI;

public class SpeedMeter : MonoBehaviour
{
	// スピードを表示するテキスト
	Text meterTxt;

	// 電車のリジッドボディ
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
				meterTxt.color = new Color(61f / 255f, 131f / 255f, 38f / 255f);
			}
			else if(speed < 99)
			{
				meterTxt.color = new Color(205f / 255f, 127f / 255f, 9f / 255f);
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

	// リジッドボディ設定
	public void SetTrainRigidbody(Rigidbody rigidbody)
	{
		trainRb = rigidbody;
	}
}
