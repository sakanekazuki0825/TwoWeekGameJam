using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 汽車クラス
public class Train : MonoBehaviour, ITrain
{
	//// 移動する向き
	//Vector2 direction;
	//// 向いている方向に対して動く方向
	//[SerializeField]
	//List<Vector2> dir;

	// 曲がる位置
	Vector3 curvePos = new Vector3(0, 0, 0);
	// 目的地
	Vector3 targetPos = new Vector3(10, 0, 0);
	//// 目的地
	//Vector2 targetDir;

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

	// とりあえず
	float hokan = 0.1f;

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

		//// ---------------------------------------------
		//// とりあえず実装

		//// 左に移動中
		//if (rb.velocity.x < 0)
		//{
		//	if(transform.position.x < targetPos.x + hokan)
		//	{
		//		switch (direction)
		//		{
		//		case Direction.LEFT:
		//			targetPos = transform.position - new Vector3(1.92f, 0, 0);
		//			break;
		//		case Direction.RIGHT:
		//			targetPos = transform.position + new Vector3(1.92f, 0, 0);
		//			break;
		//		case Direction.UP:
		//			targetPos = transform.position + new Vector3(0, 1.6f, 0);
		//			break;
		//		case Direction.DOWN:
		//			targetPos = transform.position - new Vector3(0, 1.6f, 0);
		//			break;
		//		}
		//	}
		//}
		//// 下に移動中
		//else if (rb.velocity.y < 0)
		//{
		//	if (transform.position.y < targetPos.y + hokan)
		//	{
		//		switch (direction)
		//		{
		//		case Direction.LEFT:
		//			targetPos = transform.position - new Vector3(1.92f, 0, 0);
		//			break;
		//		case Direction.RIGHT:
		//			targetPos = transform.position + new Vector3(1.92f, 0, 0);
		//			break;
		//		case Direction.UP:
		//			targetPos = transform.position + new Vector3(0, 1.6f, 0);
		//			break;
		//		case Direction.DOWN:
		//			targetPos = transform.position - new Vector3(0, 1.6f, 0);
		//			break;
		//		}
		//	}
		//}
		//// 右に移動中
		//else if (rb.velocity.x > 0)
		//{
		//	if (transform.position.x > targetPos.x - hokan)
		//	{
		//		switch (direction)
		//		{
		//		case Direction.LEFT:
		//			targetPos = transform.position - new Vector3(1.92f, 0, 0);
		//			break;
		//		case Direction.RIGHT:
		//			targetPos = transform.position + new Vector3(1.92f, 0, 0);
		//			break;
		//		case Direction.UP:
		//			targetPos = transform.position + new Vector3(0, 1.6f, 0);
		//			break;
		//		case Direction.DOWN:
		//			targetPos = transform.position - new Vector3(0, 1.6f, 0);
		//			break;
		//		}
		//	}
		//}
		//// 上に移動中
		//else if(rb.velocity.y > 0)
		//{
		//	if (transform.position.y > targetPos.y - hokan)
		//	{
		//		switch (direction)
		//		{
		//		case Direction.LEFT:
		//			targetPos = transform.position - new Vector3(1.92f, 0, 0);
		//			break;
		//		case Direction.RIGHT:
		//			targetPos = transform.position + new Vector3(1.92f, 0, 0);
		//			break;
		//		case Direction.UP:
		//			targetPos = transform.position + new Vector3(0, 1.6f, 0);
		//			break;
		//		case Direction.DOWN:
		//			targetPos = transform.position - new Vector3(0, 1.6f, 0);
		//			break;
		//		}
		//	}			
		//}
		//// ---------------------------------------------
		// 動いていないからゲームオーバー
		//else
		//{
		//	GameObject.FindObjectOfType<GameManager>().GameOver();
		//}



		// 移動
		rb.velocity = (targetPos - transform.position).normalized * speed;
		// 移動速度更新
		//speed = Mathf.Lerp(speed, afterSpeed, 0);
	}

	//private void OnTriggerEnter(Collider other)
	//{
	//	// パネルに当たった場合目的地設定
	//	if (other.gameObject.CompareTag("Panel"))
	//	{
	//		targetPos = other.transform.position;
	//	}
	//}

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
		// (方向 * パネルスプライトのサイズ)パネルスプライトのサイズの取得方法を考える
		this.curvePos = curvePos;
		this.targetPos = targetPos;
	}
}
