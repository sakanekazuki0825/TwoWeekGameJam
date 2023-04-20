using System.Collections.Generic;
using UnityEngine;

// パネル管理クラス
public class PanelManager : MonoBehaviour
{
	// パネルのスプライト
	[SerializeField, Tooltip("0.UP & RIGHT\n1.UP & Left\n2.LEFT & RIGHT\n3.RIGHT & DOWN\n4.LEFT & DOWN\n5.UP & DOWN")]
	List<Sprite> panelSprites = new List<Sprite>();
	// 生成するパネルプレハブ
	[SerializeField]
	GameObject panel;
	// ゴールパネルプレハブ
	[SerializeField]
	GameObject goalPanel;

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
	public static Vector2 SpriteSize { get => spriteSize; }

	// 直線で上がる速度
	[SerializeField]
	float speedUpValue = 0.4f;

	// カーブで下がる速度
	[SerializeField]
	float speedDownValue = -0.4f;

	// ゴールまでのパネルの数（X方向のみ計算）
	[SerializeField]
	int numberToGoal = 100;
	int nowNumber = 0;
	// 生成したゴールオブジェクト
	List<GameObject> goalObj = new List<GameObject>();

	// パネル生成時に使用するボックスガチャみたいなやつ
	[SerializeField]
	PanelBoxData panelBoxData;
	// 1ボックスのパネルの数
	[SerializeField]
	int oneBoxPanelNum = 20;
	// パネルボックス
	List<int> panelBox = new List<int>();
	// ボックスの番号
	int boxNumber = 0;

	private void Awake()
	{
		GameInstance.panelManager = this;
		for (int i = 0; i < oneBoxPanelNum; ++i)
		{
			panelBox.Add(panelBoxData.PanelDataList[i]);
		}
	}

	private void Start()
	{

		// スプライトのサイズ設定
		spriteSize = panel.GetComponent<SpriteRenderer>().bounds.size;
		InitSpawn();
	}

	private void Update()
	{
		// スクリーンの右下の座標を取得
		var screenRightDownPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10));
		// 最後のパネルの左端の位置を取得
		var panelLeftPos = panels[panels.Count - 1].transform.position.x - spriteSize.x / 2;
		// パネルの左が画面に入っていた場合生成
		if (panelLeftPos < screenRightDownPos.x)
		{
			PanelSpawn();
		}
	}

	private void OnDestroy()
	{
		GameInstance.panelManager = null;
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
		var spawnPosX = lastObjPos.x + spriteSize.x;
		// ゴール
		if (nowNumber == (numberToGoal - 1))
		{
			// ゴールの生成は1回のみ
			if (goalObj.Count > 0)
			{
				return;
			}
			for (int i = 0; i < spawnNum.y; ++i)
			{
				goalObj.Add(Instantiate(goalPanel, new Vector2(spawnPosX, spriteSize.y * i + spawnStartPos.y), Quaternion.identity));
			}
			return;
		}
		for (int i = 0; i < spawnNum.y; ++i)
		{
			// パネル生成
			var insObj = Instantiate(panel, new Vector2(spawnPosX, spriteSize.y * i + spawnStartPos.y), Quaternion.identity);
			// 生成
			panels.Add(insObj);

			//// 繋がっている方向を決める（-1して直進パネルを1枚として扱う）
			//var lineNumber = Random.Range(0, panelSprites.Count - 1);

			//if(lineNumber == 2)
			//{
			//	// 直線の場合ランダムで縦横決める
			//	var straight = Random.Range(0, 2);
			//	// 3の倍数にして、-1すると縦と横の番号になる
			//	lineNumber = (straight + 1) * 3 - 1;
			//}

			// ボックスガチャシステム
			var number = Random.Range(0, panelBox.Count);
			var lineNumber = panelBox[number];

#if UNITY_EDITOR
			// デバッグ
			if (GameInstance.isDebug)
			{
				lineNumber = 2;
			}
#endif

			// スプライトの設定
			insObj.GetComponent<IPanel>().SetSprite(panelSprites[lineNumber]);
			// 繋がっている場所を指定
			insObj.GetComponent<IPanel>().SetLinkDirection(GameInstance.linePattern[lineNumber]);

			// 変化させる速度の設定
			if (lineNumber == 2 || lineNumber == 5)
			{
				insObj.GetComponent<IPanel>().SetChangeSpeedValue(speedUpValue);
			}
			else
			{
				insObj.GetComponent<IPanel>().SetChangeSpeedValue(speedDownValue);
			}

			// 生成したパネルは削除
			panelBox.Remove(panelBox[number]);
			// パネルがなくなった場合
			if (panelBox.Count <= 0)
			{
				++boxNumber;
				var dataNumber = boxNumber * oneBoxPanelNum;
				if (panelBoxData.PanelDataList.Count <= dataNumber)
				{
					dataNumber = 0;
				}
				for (int pn = 0; pn < oneBoxPanelNum; ++pn)
				{
					panelBox.Add(panelBoxData.PanelDataList[dataNumber + pn]);
				}
			}
		}

		// 生成した数を追加
		++nowNumber;
	}
}