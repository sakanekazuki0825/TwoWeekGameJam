using System.Collections;
using System.Collections.Generic;
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

	// true = 移動できる
	bool canMove = false;

	// 補完速度
	float complement = 0;

	// 止まった時に速さを保存
	Vector3 stopBeforSpeed = new Vector3 (0, 0, 0);

	// 許容範囲
	[SerializeField]
	float tolerance = 0.1f;
	// true = 真ん中を通過
	bool isCenter = false;
	// 曲がる位置
	Vector3 curvePos = new Vector3(0, 0, 0);
	// 最終目的地
	Vector3 finalDestination = new Vector3(10, 0, 0);
	// 目的地
	Vector3 targetPos = new Vector3(10, 0, 0);

	private void Awake()
	{
		// 物理取得
		rb = GetComponent<Rigidbody>();
		// 初期速度設定
		rb.velocity = new Vector3(startSpeed, 0, 0);
	}

	private void FixedUpdate()
	{
		// 移動できる状態か調べる
		if (!canMove)
		{
			return;
		}

		if (!isCenter && Vector2.Distance(transform.position, curvePos) <= tolerance)
		{
			// 許容範囲に入ったら目的地を代入
			transform.position = curvePos;
			// 目的地更新 (自分の位置 + 目的地の方向 * 画像サイズ)
			targetPos = finalDestination;
			// 真ん中を通過
			isCenter = true;
		}

		// 移動
		rb.velocity = (targetPos - transform.position).normalized * speed;
		// 移動速度更新
		//speed = Mathf.Lerp(speed, afterSpeed, 0);
	}

	// 進む
	void ITrain.Go()
	{
		canMove = true;
		//rb.velocity = stopBeforSpeed;
	}

	// 停止
	void ITrain.Stop()
	{
		canMove = false;
		stopBeforSpeed = rb.velocity;
		rb.velocity = Vector3.zero;

	}

	// 速度を上げる
	void ITrain.AddSpeed(float speed)
	{
		afterSpeed += speed;
		// 補完速度計算
		complement = (speed + afterSpeed) / 2;// / スプライトサイズ

		// とりあえず
		this.speed += speed;
	}

	// 方向を変更
	void ITrain.Curve(Vector3 curvePos, Vector3 targetPos)
	{
		isCenter = false;

		// 曲がる位置とパネルの繋がっている位置を取得
		this.curvePos = curvePos;
		this.finalDestination = targetPos;
	}
}
