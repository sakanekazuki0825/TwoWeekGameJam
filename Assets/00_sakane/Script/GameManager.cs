using System.Collections;
using UnityEngine;

// ルール管理クラス
public class GameManager : MonoBehaviour,IGameManager
{
	// true = ゲーム終了
	//bool isGameFinish = false;

	// true = ゲーム中
	//bool isInPlay = false;

	// リザルトを表示するキャンバス
	[SerializeField]
	GameObject resultCanvas;

	// フェードをするクラス
	FadeIO fade;

	// プレイヤープレハブ
	[SerializeField]
	GameObject playerPrefab;
	// 生成したオブジェクト
	GameObject playerObj;
	// 生成する電車
	[SerializeField]
	GameObject trainPrefab;
	// 生成した電車
	GameObject train;

	private void Start()
	{
		// リザルトを非表示にする
		//resultCanvas.SetActive(false);

		// フェードクラス取得
		fade = GameObject.FindObjectOfType<FadeIO>();

		// ゲーム開始
		GameStart();
	}

	// ゲーム開始
	public void GameStart()
	{
		// 電車生成
		train = Instantiate(trainPrefab, new Vector3(0, 0, 0), Quaternion.identity);
		StartCoroutine(EGameStart());
	}

	// ゲーム開始コルーチン
	IEnumerator EGameStart()
	{
		// まだ操作できない
		//isInPlay = false;

		// フェードイン
		//fade.FadeIn();

		// フェードが終わるまでループ
		//while (fade.IsFading)
		//{
		yield return null;
		//}

		// プレイヤー生成
		playerObj = Instantiate(playerPrefab);
		// プレイヤーを止める
		playerObj.GetComponent<IPlayer>().GameStop();
		// プレイヤーを動くことができる状態にする
		playerObj.GetComponent<IPlayer>().GameStart();

		// フェードが終わったら操作できる
		//isInPlay = true;
	}

	// ゲーム終了
	public void GameFinish()
	{
		StartCoroutine (EGameFinish());
	}

	// ゲーム終了コルーチン
	IEnumerator EGameFinish()
	{
		// プレイヤーを動かせない状態にする
		playerObj.GetComponent<IPlayer>().GameFinish();
		// リザルトを表示
		//resultCanvas.SetActive(true);
		// 操作できない状態にする
		//isInPlay = false;
		// フェードアウト
		//fade.FadeOut();

		// フェードが終わるまでループ
		while (fade.IsFading)
		{
			yield return null;
		}

		// シーン読み込み
	}

	// プレイヤーが道から外れた
	void IGameManager.GetOffTheRoad()
	{
		// ゲームオーバー
		playerObj.GetComponent<IPlayer>().GameFinish();
		GameFinish();
	}
}