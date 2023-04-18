using System.Collections.Generic;
using UnityEngine;

// パネル
public class Panel : MonoBehaviour, IPanel
{
	[SerializeField]
	// 繋がっている方向
	List<Vector2> linkDirections;

	// true = 電車が乗っている
	bool isTrain = false;

	// スプライトを表示するクラス
	SpriteRenderer spriteRenderer;

	// 変化させる速度
	float speedChangeValue = 0.4f;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void OnTriggerEnter(Collider other)
	{
		// プレイヤーが当たった場合
		if (other.gameObject.CompareTag("Train"))
		{
			var hitPoint = other.ClosestPoint(transform.position);

			// 当たった方向に道があるか調べる
			Vector2 hitDir = HitDirection(other.transform.position);
			var isLoad = IsLoad(hitDir);
			if (!isLoad)
			{
				// ゲームオーバー
				GameInstance.gameManager.GameOver();
			}
			else
			{
				other.GetComponent<ITrain>().AddSpeed(speedChangeValue);

				var targetPos = linkDirections[(linkDirections.IndexOf(hitDir) + 1) % 2] * PanelManager.SpriteSize;
				// 電車の目的地（パネルが目的地設定は違和感がある）
				other.GetComponent<ITrain>().Curve(transform.position, transform.position + new Vector3(targetPos.x, targetPos.y));
			}
		}
		else if (other.gameObject.CompareTag("CannotSelectPanel"))
		{
			GameInstance.player.SelectPanelRemove(gameObject);
		}
	}

	// 当たった方向を調べる
	Vector3 HitDirection(Vector3 position)
	{
		var dir = transform.position - position;
		var x = Mathf.Abs(dir.x);
		var y = Mathf.Abs(dir.y);

		if (x > y)
		{
			if (dir.x > 0)
			{
				return Vector2.left;
			}
			else
			{
				return Vector2.right;
			}
		}
		else
		{
			if (dir.y > 0)
			{
				return Vector2.down;
			}
			else
			{
				return Vector2.up;
			}
		}
	}

	// 道があるか調べる
	bool IsLoad(Vector2 dir)
	{
		return linkDirections.Contains(dir);
	}

	// 方向設定
	void IPanel.SetLinkDirection(List<Vector2> directions)
	{
		linkDirections = directions;
	}

	// 電車が乗っているか判定
	bool IPanel.IsOnTrain()
	{
		return isTrain;
	}

	// スプライトの設定
	void IPanel.SetSprite(Sprite sprite)
	{
		spriteRenderer.sprite = sprite;
	}

	// 変化させる速度の設定
	void IPanel.SetChangeSpeedValue(float speed)
	{
		speedChangeValue = speed;
	}
}