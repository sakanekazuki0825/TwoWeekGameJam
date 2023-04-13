using UnityEngine;

// プレイヤークラス
public class Player : MonoBehaviour,IPlayer
{
	// 選択中のオブジェクト
	GameObject nowSelectObj;
	// 交換するオブジェクト
	GameObject changeObj;

	// true = オブジェクト選択中
	bool isSelect = false;
	// 選択した際のオブジェクトの位置
	Vector3 mousePos;

	// true = 動くことができる
	bool canOperation = false;

	private void Update()
	{
		// 操作できる状態でない場合は入力をとらない
		if (!canOperation)
		{
			return;
		}

		// 左クリックした際のマウスの位置を設定
		if (Input.GetMouseButtonDown(0))
		{
			mousePos = Input.mousePosition;
			isSelect = true;
		}
	}

	private void FixedUpdate()
	{
		if (!canOperation)
		{
			// 操作できないので戻る
			return;
		}
		if (isSelect)
		{
			// ヒット情報
			RaycastHit hit;
			// レイ情報
			var ray = Camera.main.ScreenPointToRay(mousePos);
			// レイにあたっているか
			if (Physics.Raycast(ray.origin, ray.direction, out hit))
			{
				// 選択しているオブジェクトが無い場合代入
				if (nowSelectObj == null)
				{
					nowSelectObj = hit.collider.gameObject;
				}
				// オブジェクトを選択している場合
				else
				{
					// 交換するオブジェクト取得
					changeObj = hit.collider.gameObject;
					// 最初に選択したオブジェクトの位置取得
					var selObjPos = nowSelectObj.transform.position;
					// 後に選択したオブジェクトの位置取得
					var changePos = changeObj.transform.position;

					// 位置交換
					nowSelectObj.transform.position = changePos;
					changeObj.transform.position = selObjPos;

					// 選択しているオブジェクトを外す
					nowSelectObj = null;
					changeObj = null;
				}
			}
			// 選択していない状態にする
			isSelect = false;
		}
	}

	// ゲーム開始通知
	void IPlayer.GameStart()
	{
		canOperation = true;
	}

	// ゲーム終了通知
	void IPlayer.GameFinish()
	{
		canOperation = false;
	}

	// ゲーム停止
	void IPlayer.GameStop()
	{
		canOperation = false;
	}
}
