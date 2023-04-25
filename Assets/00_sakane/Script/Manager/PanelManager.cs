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
	public int StartHorizontalPanel { get => startHorizontalPanel; }

	// 最初に生成する数
	[SerializeField, Tooltip("x = 最初に横に生成する数（-3した数字）\ny = 縦に生成する数")]
	Vector2 spawnNum = new Vector2(5, 5);

	// 生成開始位置
	[SerializeField]
	Vector2 spawnStartPos = new Vector2(5, 5);

	// 生成したパネル
	List<GameObject> panels = new List<GameObject>();

	// スプライトのサイズ
	Vector2 spriteSize = new Vector2(1.92f, 1.6f);
	public Vector2 SpriteSize { get => spriteSize; }

	// 直線で上がる速度
	[SerializeField]
	float speedUpValue = 0.4f;

	// カーブで下がる速度
	[SerializeField]
	float speedDownValue = -0.4f;

	// ゴールまでのパネルの数（X方向のみ計算）
	[SerializeField]
	int numberToGoal = 100;
	public int NumberToGoal{ get => numberToGoal; }
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

	// 乗っても何も起きないタイル
	[SerializeField]
	GameObject tile;
	[SerializeField]
	int tileNumber = 3;

	// 脱線
	[SerializeField]
	GameObject crashObj;
	// ゴールの背景
	[SerializeField]
	GameObject goalBackGroundObj;

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
		GameInstance.distanceToGoal.Goal = spriteSize * NumberToGoal;
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
		for (int i = 0; i < spawnNum.x; ++i)
		{
			if (i == 2)
			{
				continue;
			}
			Instantiate(crashObj, new Vector2(spawnStartPos.x + spriteSize.x * 2, spawnStartPos.y + spriteSize.y * i), Quaternion.identity);
		}

		// 初期配置
		for (int i = 0; i < spawnNum.x; ++i)
		{
			PanelSpawn();
		}
		for (int i = 0; i < tileNumber; ++i)
		{
			var pos = panels[0].transform.position;
			Instantiate(tile, new Vector3(pos.x - spriteSize.x * i, pos.y, pos.z), Quaternion.identity);
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
				spawnPosX = goalObj[goalObj.Count - 1].transform.position.x + spriteSize.x;
				for (int i = 0; i < spawnNum.y; ++i)
				{
					goalObj.Add(Instantiate(tile, new Vector2(spawnPosX, spriteSize.y * i + spawnStartPos.y), Quaternion.identity));
				}
				return;
			}
			Instantiate(goalBackGroundObj, new Vector2(spawnPosX - spriteSize.x/2, spriteSize.y), Quaternion.identity);
			for (int i = 0; i < spawnNum.y; ++i)
			{
				goalObj.Add(Instantiate(goalPanel, new Vector2(spawnPosX, spriteSize.y * i + spawnStartPos.y), Quaternion.identity));
				Camera.main.GetComponent<ICameraMove>().SetGoalPos(spawnPosX);
			}
			return;
		}
		Instantiate(crashObj, new Vector2(spawnPosX, spawnStartPos.y - spriteSize.y), Quaternion.identity);
		Instantiate(crashObj, new Vector2(spawnPosX, spawnStartPos.y + spriteSize.y * spawnNum.y), Quaternion.identity);
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