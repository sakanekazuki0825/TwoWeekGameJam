using UnityEngine;

public class BackGroundSpawner : MonoBehaviour
{
	// 背景オブジェクト
	[SerializeField]
	GameObject backGroundObj;

	public void Spawn()
	{
		// メインカメラの位置取得
		var startPos = Camera.main.transform.position;
		startPos = new Vector3(startPos.x, startPos.y, 0);
		//// 最初のオブジェクトを生成
		//Instantiate(backGroundObj, startPos, Quaternion.identity);
		// 背景画像のサイズ取得
		var backGroundSize = Instantiate(backGroundObj, startPos, Quaternion.identity).GetComponent<SpriteRenderer>().bounds.size;
		// 線路画像のサイズ取得
		var spriteSize = GameInstance.panelManager.SpriteSize;
		// パネルが最後に生成される位置
		Vector3 lastPosition = spriteSize * (GameInstance.panelManager.NumberToGoal + GameInstance.panelManager.StartHorizontalPanel);
		// 最初の位置からゴールまでの距離を計算
		var dist = lastPosition - startPos;
		// 背景を生成する必要のある数計算
		var backGroundNumber = dist.x / backGroundSize.x;
		for (int i = 0; i < backGroundNumber; ++i)
		{
			Instantiate(backGroundObj, startPos + new Vector3(backGroundSize.x * (i + 1), 0, 0), Quaternion.identity);
		}
	}
}