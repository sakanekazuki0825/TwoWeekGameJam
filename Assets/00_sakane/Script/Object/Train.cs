using UnityEngine;

// 汽車クラス
public class Train : MonoBehaviour, ITrain
{
	// 物理
	Rigidbody rb;

	// 最初の速度
	[SerializeField]
	float startSpeed = 4;
	// 現在の速度
	float speed = 1;
	// 最終的に到達する速度
	float afterSpeed;
	[SerializeField]
	float acceleration = 0.08f;

	// true = 移動できる
	bool canMove = false;

	// 補完速度
	//float complement = 0;

	// 止まった時に速さを保存
	Vector3 stopBeforeSpeed = new Vector3 (0, 0, 0);

	// 許容範囲
	[SerializeField]
	//float tolerance = 0.1f;
	// true = 真ん中を通過
	//bool isCenter = false;
	// 最終目的地
	Vector3 finalDestination = new Vector3(10, 0, 0);
	// 目的地
	Vector3 targetPos = new Vector3(10, 0, 0);
	// ターゲットの方向
	Vector3 beforeTargetDir = new Vector3(1, 0, 0);

	private void Awake()
	{
		// 物理取得
		rb = GetComponent<Rigidbody>();
		// 初期速度設定
		speed = startSpeed;

		afterSpeed = startSpeed;
		stopBeforeSpeed = new Vector3(startSpeed, 0, 0);
	}

	private void Update()
	{
		if (!canMove)
		{
			return;
		}
		if (rb.velocity.magnitude == 0)
		{
			GameInstance.gameManager.GameOver();
		}
	}

	private void FixedUpdate()
	{
		// 移動できる状態か調べる
		if (!canMove)
		{
			return;
		}

		if ((targetPos - transform.position).normalized != beforeTargetDir.normalized)
		//if (!isCenter && Vector2.Distance(transform.position, curvePos) <= (tolerance /** startSpeed / speed*/))
		{
			// 許容範囲に入ったら目的地を代入
			transform.position = targetPos;
			// 目的地更新 (自分の位置 + 目的地の方向 * 画像サイズ)
			targetPos = finalDestination;
			// 真ん中を通過
			//isCenter = true;

			beforeTargetDir = targetPos - transform.position;
		}

		// 移動
		rb.velocity = beforeTargetDir.normalized * speed;
		// 移動速度更新
		speed = Mathf.Lerp(speed, afterSpeed, acceleration);
	}

	// 進む
	void ITrain.Go()
	{
		canMove = true;
		rb.velocity = stopBeforeSpeed;
	}

	// 停止
	void ITrain.Stop()
	{
		canMove = false;
		stopBeforeSpeed = rb.velocity;
		rb.velocity = Vector3.zero;
	}

	// 速度を上げる
	void ITrain.AddSpeed(float speed)
	{
		afterSpeed += speed;
		// 補完速度計算
		//complement = (speed + afterSpeed) / 2 / 1.92f;
	}

	// 方向を変更
	void ITrain.Curve(Vector3 curvePos, Vector3 targetPos)
	{
		//isCenter = false;

		// 曲がる位置とパネルの繋がっている位置を取得
		this.targetPos = curvePos;
		finalDestination = targetPos;
		beforeTargetDir = curvePos - transform.position;
	}
}