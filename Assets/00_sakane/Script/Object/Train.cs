using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyEnum;

// 汽車クラス
public class Train : MonoBehaviour, ITrain
{
	// 現在の向き
	Direction direction;
	// 向いている方向に対して動く方向
	[SerializeField]
	List<Vector2> dir;

	// 物理
	Rigidbody rb;

	// 最初の速度
	[SerializeField]
	float startSpeed = 4;
	// 現在の速度
	float speed = 0;

	// 最終的に到達する速度
	float afterSpeed;

	// true = 移動できる
	bool canMove = false;

	// 補完速度
	float complement = 0;

	// 目的地
	Vector3 targetPos;

	private void Start()
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
		// 移動
		rb.velocity = (targetPos - transform.position) * speed;
		// 移動速度更新
		speed = Mathf.Lerp(speed, afterSpeed, complement);
	}

	private void OnTriggerEnter(Collider other)
	{
		// パネルに当たった場合目的地設定
		if (other.gameObject.CompareTag("Panel"))
		{
			targetPos = other.transform.position;
		}
	}

	// 進む
	void ITrain.Go()
	{
		canMove = true;
	}

	// 停止
	void ITrain.Stop()
	{
		canMove = false;
	}

	// 速度を上げる
	void ITrain.AddSpeed(float speed)
	{
		afterSpeed = speed;
		// 補完速度計算
		complement = (speed + afterSpeed) / 2;// / スプライトサイズ
	}

	// 方向を変更
	void ITrain.Curve(Direction dir)
	{
		direction = dir;
	}
}
