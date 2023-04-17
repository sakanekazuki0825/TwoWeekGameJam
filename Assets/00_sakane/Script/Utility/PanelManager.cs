using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// パネル管理クラス
public class PanelManager : MonoBehaviour
{
	// 生成するパネルプレハブ
	[SerializeField]
	GameObject panel;

	// 最初に生成する横に移動するパネルの数
	[SerializeField]
	int startHorizontalPanel = 3;

	// 最初に生成する数
	[SerializeField, Tooltip("x = 最初に横に生成する数（-3した数字）\ny = 縦に生成する数")]
	Vector2 spawnNum = new Vector2(5, 5);

	// 生成開始位置
	[SerializeField]
	Vector2 spawnStartPos = new Vector2(5, 5);

	// 生成したパネル
	List<GameObject> panels = new List<GameObject>();

	// スプライトのサイズ
	static Vector2 spriteSize = new Vector2(1.92f, 1.6f);
	public static Vector2 SpriteSize { get { return spriteSize; } }

	private void Start()
	{
		// スプライトのサイズ設定
		spriteSize = panel.GetComponent<SpriteRenderer>().bounds.size;
		InitSpawn();
	}

	private void Update()
	{
		// スクリーンの右下の座標を取得
		var screenRightDownPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
		// 最後のパネルの左端の位置を取得
		var panelLeftPos = panels[panels.Count - 1].transform.position.x - spriteSize.x / 2;
		// パネルの左が画面に入っていた場合生成
		if(panelLeftPos < screenRightDownPos.x)
		{
			PanelSpawn();
		}
	}

	// 初期配置
	void InitSpawn()
	{
		// 最初に横に移動するパネルの生成
		for (int i = 0; i < startHorizontalPanel; ++i)
		{
			// パネル生成
			var insObj = Instantiate(panel, new Vector3(spawnStartPos.x + spriteSize.x * i, spawnStartPos.y + spriteSize.y * Mathf.Floor(spawnNum.y / 2), 0), Quaternion.identity);
			// 生成したパネル保存
			panels.Add(insObj);
			// パネルに方向を設定
			insObj.GetComponent<IPanel>().SetLinkDirection(new List<Vector2> { Vector2.left, Vector2.right });
		}
		// 初期配置
		for (int i = 0; i < spawnNum.x; ++i)
		{
			PanelSpawn();
		}
	}

	// パネル生成
	public void PanelSpawn()
	{
		// 最後に追加したオブジェクトの位置取得
		var lastObjPos = panels[panels.Count - 1].transform.position;
		// 生成位置取得
		var spawnPos = new Vector2(lastObjPos.x + spriteSize.x, spriteSize.y);
		for (int i = 0; i < spawnNum.y; ++i)
		{
			// パネル生成
			var insObj = Instantiate(panel, new Vector2(lastObjPos.x + spriteSize.x, spriteSize.y * i + spawnStartPos.y), Quaternion.identity);
			// 生成
			panels.Add(insObj);

			// 繋がっている方向を決める
		}
	}
}