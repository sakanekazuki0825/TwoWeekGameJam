using System.Collections;
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
	float speed = 0.1f;
	// 最終的に到達する速度
	float afterSpeed;
	[SerializeField]
	float acceleration = 0.08f;

	// true = 移動できる
	bool canMove = false;

	// 補完速度
	//float complement = 0;

	// 止まった時に速さを保存
	Vector3 stopBeforeSpeed = new Vector3(0, 0, 0);

	// 最終目的地
	Vector3 finalDestination = new Vector3(10, 0, 0);
	// 目的地
	Vector3 targetPos = new Vector3(10, 0, 0);
	// ターゲットの方向
	Vector3 beforeTargetDir = new Vector3(1, 0, 0);

	// 疾走感を出すやつ
	[SerializeField]
	ParticleSystem dashEffect;
	[SerializeField]
	float velMag;

	// アニメーター
	Animator animator;

	// true = スクリーンを出た
	bool isScreenOut = false;
	public bool IsScreenOut { get => isScreenOut; }

	// 爆発エフェクト
	[SerializeField]
	GameObject crashEffect;

	AudioSource source;

	BoxCollider boxCol;

	Vector3 leftFacingCenter = new Vector3(0, 0.8f, 0);
	Vector3 leftFacingSize = new Vector3(8, 4, 0.2f);

	Vector3 upFacingCenter = new Vector3(0, 0, 0);
	Vector3 upFacingSize = new Vector3(3, 6.5f, 0.2f);

	private void Awake()
	{
		// 物理取得
		rb = GetComponent<Rigidbody>();
		// 初期速度設定
		//speed = startSpeed;

		afterSpeed = startSpeed;
		stopBeforeSpeed = new Vector3(0.01f, 0, 0);

		// アニメーター取得
		animator = GetComponent<Animator>();
		animator.speed = 0;
		source = GetComponent<AudioSource>();
		boxCol = GetComponent<BoxCollider>();
	}

	private void Start()
	{
		dashEffect.gameObject.SetActive(false);
		GameObject.FindObjectOfType<SpeedMeter>().SetTrainRigidbody(rb);
	}

	private void Update()
	{
		if (!canMove)
		{
			return;
		}
		if (rb.velocity == Vector3.zero)
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
			if((beforeTargetDir.normalized.y > 0.5f) || (beforeTargetDir.normalized.y < -0.5f))
			{
				boxCol.center = upFacingCenter;
				boxCol.size = upFacingSize;
			}
			else
			{
				boxCol.center = leftFacingCenter;
				boxCol.size = leftFacingSize;
			}
		}

		animator.SetFloat("speed", beforeTargetDir.y);
		// アニメーション速度更新
		animator.speed = (speed / startSpeed);
		var pmain = dashEffect.main;
		pmain.simulationSpeed = (speed / (startSpeed * 5));
		// 移動
		rb.velocity = beforeTargetDir.normalized * speed;
		// 移動速度更新
		speed = Mathf.Lerp(speed, afterSpeed, acceleration);

		animator.SetFloat("speed", beforeTargetDir.y);
	}

	// 進む
	void ITrain.Go()
	{
		canMove = true;
		rb.velocity = stopBeforeSpeed;
		source.UnPause();
	}

	// 停止
	void ITrain.Stop()
	{
		canMove = false;
		stopBeforeSpeed = rb.velocity;
		rb.velocity = Vector3.zero;
		animator.speed = 0;
		var pmain = dashEffect.main;
		pmain.simulationSpeed = 0;
		source.Pause();
	}

	// 速度を上げる
	void ITrain.AddSpeed(float speed)
	{
		afterSpeed += speed;

		if (afterSpeed > velMag)
		{
			dashEffect.gameObject.SetActive(true);
		}
		else
		{
			dashEffect.gameObject.SetActive(false);
		}

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

	// ゲームクリア
	void ITrain.GameClear(Vector3 goalPos)
	{
		StartCoroutine(EGameClear(goalPos));
		source.Pause();
	}

	// ゲームクリアコルーチン
	IEnumerator EGameClear(Vector3 goalPos)
	{
		canMove = false;
		var pmain = dashEffect.main;
		pmain.simulationSpeed = 0;
		//while(goalPos.x < transform.position.x)
		//{
		yield return new WaitUntil(
			() =>
			{
				rb.velocity = (goalPos - transform.position) * speed;
				animator.speed = rb.velocity.magnitude / startSpeed;
				return goalPos.x <= transform.position.x + 0.01f;
			});
		transform.position = new Vector3(goalPos.x, transform.position.y, transform.position.z);
		animator.speed = 0;
		dashEffect.gameObject.SetActive(false);
		rb.velocity = Vector3.zero;
		//}
	}

	// タイトルへ
	void ITrain.GoTitle()
	{
		StartCoroutine(EGoTitle());
	}

	IEnumerator EGoTitle()
	{
		source.UnPause();
		var pos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.75f, 0, -Camera.main.transform.position.z));
		animator.speed = startSpeed;
		rb.velocity = new Vector3(startSpeed, 0, 0);
		yield return new WaitUntil(
			() =>
			{
				return transform.position.x >= pos.x;
			});
		isScreenOut = true;
	}

	void ITrain.Crash()
	{
		Instantiate(crashEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	bool ITrain.IsScreenOut()
	{
		return isScreenOut;
	}
}