using System.Collections.Generic;
using UnityEngine;

// パネル
public class Panel : MonoBehaviour,IPanel
{
	[SerializeField]
	// 繋がっている方向
	List<Vector2> linkDirections;

	// true = 電車が乗っている
	bool isTrain = false;

	// 直線で上がる速度
	[SerializeField]
	float speedUpValue = 0.4f;
	// カーブで下がる速度
	[SerializeField]
	float speedDownValue = 0.4f;

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
				// ゲーム終了
				GameObject.FindObjectOfType<GameManager>().GameOver();
			}
			else
			{
				// 直線か調べる
				if ((linkDirections[0] - linkDirections[1]).magnitude > 1.8f)
				{
					other.GetComponent<ITrain>().AddSpeed(speedUpValue);
				}
				else
				{
					other.GetComponent<ITrain>().AddSpeed(speedDownValue);
				}

				var targetPos = linkDirections[(linkDirections.IndexOf(hitDir) + 1) % 2] * PanelManager.SpriteSize;
				// 電車の目的地（パネルが目的地設定は違和感がある）
				other.GetComponent<ITrain>().Curve(transform.position, transform.position + new Vector3(targetPos.x, targetPos.y));
			}
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
}