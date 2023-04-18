using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// ルール管理クラス
public class GameManager : MonoBehaviour, IGameManager
{
	// true = ゲーム終了
	//bool isGameFinish = false;

	// true = ゲーム中
	bool isInPlay = false;

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
	// 電車生成位置
	[SerializeField]
	Vector3 trainPos;

	// 選択できない状態にするオブジェクト
	[SerializeField]
	GameObject cannotSelectObj;
	// 選択できない状態にするオブジェクトの位置
	[SerializeField]
	Vector3 cannotSelectObjPos = new Vector3(-11, 0, 0);

	// カメラオブジェクト
	[SerializeField]
	GameObject cameraObj;
	// カメラの生成位置
	[SerializeField]
	Vector3 cameraPos;

	// カウントダウン
	[SerializeField]
	float maxCountdownTime = 3;
	float countdownTime;
	[SerializeField]
	Canvas countdownCanvas;
	[SerializeField]
	Text countdownTxt;
	[SerializeField]
	string goStr = "Go";
	[SerializeField]
	float goTime = 1;

	// リタイヤ
	[SerializeField]
	Canvas retireCanvas;

	private void Awake()
	{
		GameInstance.gameManager = this;

		// カウントダウン時間設定
		countdownTime = maxCountdownTime;
	}

	private void Start()
	{
		// リザルトを非表示にする
		resultCanvas.SetActive(false);
		// リタイヤを非表示にする
		retireCanvas.gameObject.SetActive(false);

		// フェードクラス取得
		fade = GameInstance.fadeIO;

		// ゲーム開始
		GameStart();
	}

	private void Update()
	{
#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			LevelManager.GameFinish();
		}
#endif
	}

	private void OnDestroy()
	{
		GameInstance.gameManager = null;
	}

	// ゲーム開始
	public void GameStart()
	{
		// ゲーム開始
		StartCoroutine(EGameStart());
	}

	// ゲーム開始コルーチン
	IEnumerator EGameStart()
	{
		// まだ操作できない
		//isInPlay = false;

		// フェードイン
		fade.FadeIn();

		// プレイヤー生成
		playerObj = Instantiate(playerPrefab);
		// プレイヤーを止める
		playerObj.GetComponent<IPlayer>().GameStop();
		// 電車生成
		train = Instantiate(trainPrefab, trainPos, Quaternion.identity);
		Instantiate(cameraObj, cameraPos, Quaternion.identity).GetComponent<ICameraMove>().SetTarget(train);
		Instantiate(cannotSelectObj, cannotSelectObjPos, Quaternion.identity).GetComponent<ICannotSelectPanel>().SetTarget(train);

		// フェードが終わるまでループ
		while (fade.IsFading)
		{
			yield return null;
		}

		// カウントダウン
		countdownCanvas.gameObject.SetActive(true);
		while (!isInPlay)
		{
			countdownTime -= Time.deltaTime;
			countdownTxt.text = Mathf.Ceil(countdownTime).ToString();
			if (countdownTime < 0)
			{
				countdownTxt.text = goStr;
				isInPlay = true;
			}
			yield return null;
		}

		// プレイヤーを動くことができる状態にする
		playerObj.GetComponent<IPlayer>().GameStart();
		// 電車を動くことができる状態にする
		train.GetComponent<ITrain>().Go();

		yield return new WaitForSeconds(goTime);
		countdownCanvas.gameObject.SetActive(false);

		// フェードが終わったら操作できる
		// = true;
	}

	// リタイヤ
	public void Retire()
	{
		retireCanvas.gameObject.SetActive(true);
	}

	// ゲーム終了
	public void GameFinish()
	{
		StartCoroutine(EGameFinish());
	}

	// ゲーム終了コルーチン
	IEnumerator EGameFinish()
	{
		// プレイヤーを動かせない状態にする
		playerObj.GetComponent<IPlayer>().GameFinish();
		// リザルトを表示
		resultCanvas.SetActive(true);
		// 操作できない状態にする
		isInPlay = false;
		// フェードアウト
		fade.FadeOut();

		// フェードが終わるまでループ
		while (fade.IsFading)
		{
			yield return null;
		}

		// シーン読み込み
		StartCoroutine(LevelManager.ELoadLevelAsync("Title"));
	}

	public void GameOver()
	{
		resultCanvas.GetComponent<IResult>().GameOver();
		GameFinish();
		train.GetComponent<ITrain>().Stop();
	}

	public void GameClear()
	{
		resultCanvas.GetComponent<IResult>().GameClear();
		GameFinish();
		train.GetComponent<ITrain>().Stop();
	}

	// プレイヤーが道から外れた
	void IGameManager.GetOffTheRoad()
	{
		//// ゲームオーバー
		//playerObj.GetComponent<IPlayer>().GameFinish();
		//GameFinish();
	}
}