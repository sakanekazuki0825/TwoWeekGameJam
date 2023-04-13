using System.Collections.Generic;
using UnityEngine;
using MyEnum;

// パネル
public class Panel : MonoBehaviour
{
	// 繋がっている方向
	List<Direction> linkDirections;

	private void OnTriggerEnter(Collider other)
	{
		// プレイヤーが当たった場合
		if (other.gameObject.CompareTag("Train"))
		{
			// 当たった方向に道があるか調べる
			Direction hitDir = HitDirection(other.transform.position);
			var isLoad = IsLoad(hitDir);
			if (!isLoad)
			{
				// ゲーム終了
				GameObject.FindObjectOfType<GameManager>().GameFinish();
			}
			else
			{
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

		if (x < y)
		{
			if (x < 0)
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
			if (y < 0)
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
		foreach (var e in linkDirections)
		{
			if (e == dir)
			{
				return true;
			}
		}
		return false;
	}
}