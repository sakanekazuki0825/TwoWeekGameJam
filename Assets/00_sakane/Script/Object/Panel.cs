using System.Collections.Generic;
using UnityEngine;

// パネル
public class Panel : MonoBehaviour,IPanel
{
	[SerializeField]
	// 繋がっている方向
	List<Direction> linkDirections;

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
			Direction hitDir = HitDirection(other.transform.position);
			var isLoad = IsLoad(hitDir);
			if (!isLoad)
			{
				// ゲーム終了
				GameObject.FindObjectOfType<GameManager>().GameOver();
			}
			else
			{
				// 直線か調べる
				if ((linkDirections.IndexOf(Direction.LEFT) != -1) && (linkDirections.IndexOf(Direction.RIGHT) != -1) || (linkDirections.IndexOf(Direction.DOWN) != -1) && (linkDirections.IndexOf(Direction.UP) != -1))
				{
					other.GetComponent<ITrain>().AddSpeed(speedUpValue);
				}
				else
				{
					other.GetComponent<ITrain>().AddSpeed(speedDownValue);
				}

				// 電車の移動方向を設定
				other.GetComponent<ITrain>().Curve(linkDirections[(linkDirections.IndexOf(hitDir) + 1) % 2]);
			}
		}
	}

	// 当たった方向を調べる
	Direction HitDirection(Vector3 position)
	{
		var dir = transform.position - position;
		var x = Mathf.Abs(dir.x);
		var y = Mathf.Abs(dir.y);

		if (x > y)
		{
			if (x > 0)
			{
				return Direction.LEFT;
			}
			else
			{
				return Direction.RIGHT;
			}
		}
		else
		{
			if (y > 0)
			{
				return Direction.DOWN;
			}
			else
			{
				return Direction.UP;
			}
		}
	}

	// 道があるか調べる
	bool IsLoad(Direction dir)
	{
		if(linkDirections.IndexOf(dir) != -1)
		{
			return true;
		}
		return false;
	}

	// 方向設定
	void IPanel.SetLinkDirection(List<Direction> directions)
	{
		linkDirections = directions;
	}

	// 電車が乗っているか判定
	bool IPanel.IsOnTrain()
	{
		return isTrain;
	}
}