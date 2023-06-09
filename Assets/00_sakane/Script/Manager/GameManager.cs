using System.Collections;
using UnityEngine;

// ルール管理クラス
public class GameManager : MonoBehaviour
{
	// true = ゲーム終了
	//bool isGameFinish = false;

	// true = ゲーム中
	bool isInPlay = false;

	// リザルトを表示するキャンバス
	[SerializeField]
	GameObject resultCanvas;

	[SerializeField]
	GameObject gameOverCanvas;

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
	public GameObject Train { get => train; }
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
	[SerializeField]
	float xOffset = 7;

	// カウントダウン
	[SerializeField]
	Canvas countdownCanvas;
	CountDown countDown;

	// 背景
	[SerializeField]
	BackGroundSpawner backGround;

	// クリア時間
	float clearTime = 0;
	public float ClearTime { get => clearTime; }

	// ゲームクリアのBGM
	[SerializeField]
	AudioClip gameClearAudio;
	// ゲームオーバーのBGM
	[SerializeField]
	AudioClip gameOverAudio;

	// ロード画面
	[SerializeField]
	GameObject loadScreen;

	[SerializeField]
	float time;
	[SerializeField]
	AudioClip clip;
	AudioSource source;

	[SerializeField]
	GameObject cannotSelectUI;

	Coroutine coroutine;

	[SerializeField]
	Texture2D noOperationCursorSprite;
	[SerializeField]
	Texture2D operationCursorSprite;

	private void Awake()
	{
		GameInstance.gameManager = this;
		source = GetComponent<AudioSource>();
	}

	private void Start()
	{
		// リザルトを非表示にする
		resultCanvas.SetActive(false);
		gameOverCanvas.SetActive(false);

		// フェードクラス取得
		fade = GameInstance.fadeIO;
		countDown = GameInstance.countDown;
		countdownCanvas.gameObject.SetActive(false);
		loadScreen.SetActive(false);
		cannotSelectUI.SetActive(true);
		Cursor.SetCursor(noOperationCursorSprite, Vector2.zero, CursorMode.Auto);

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

		if (isInPlay)
		{
			clearTime += Time.deltaTime;
		}
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
		isInPlay = false;

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

		backGround.Spawn();

		GameInstance.distanceToGoal.Train = train;

		// フェードが終わるまでループ
		while (fade.IsFading)
		{
			yield return null;
		}

		//countDown.gameObject.SetActive(true);
		//countDown.StartCountDown(false);

		//while (countDown.IsCounting)
		//{
		//	yield return null;
		//}

		// 電車を動くことができる状態にする
		train.GetComponent<ITrain>().Go();

		while ((Camera.main.transform.position.x - train.transform.position.x) > xOffset)
		{
			yield return null;
		}
		Camera.main.transform.position = new Vector3(train.transform.position.x + xOffset, Camera.main.transform.position.y, -10);
		Camera.main.GetComponent<ICameraMove>().SetCanMove(true);

		// プレイヤーを動くことができる状態にする
		playerObj.GetComponent<IPlayer>().GameStart();
		coroutine = StartCoroutine(EWhistle());

		cannotSelectUI.SetActive(false);
		Cursor.SetCursor(operationCursorSprite, Vector2.zero, CursorMode.Auto);

		isInPlay = true;
	}

	// ゲームオーバー
	public void GameOver()
	{
		var source = Camera.main.GetComponent<AudioSource>();
		source.clip = gameOverAudio;
		source.Play();
		isInPlay = false;
		StopCoroutine(coroutine);
		// プレイヤーを動かせない状態にする
		playerObj.GetComponent<IPlayer>().GameFinish();
		// ゲームオーバーを表示
		gameOverCanvas.SetActive(true);
	}

	// ゲームクリア
	public void GameClear()
	{
		var source = Camera.main.GetComponent<AudioSource>();
		source.clip = gameClearAudio;
		source.Play();
		isInPlay = false;
		StopCoroutine (coroutine);
		// プレイヤーを動かせない状態にする
		playerObj.GetComponent<IPlayer>().GameFinish();
		//train.GetComponent<ITrain>().Stop();
		// リザルトを表示
		resultCanvas.SetActive(true);
		resultCanvas.GetComponent<Result>().Display();
	}

	// ゲーム停止
	public void GameStop()
	{
		playerObj.GetComponent<IPlayer>().GameStop();
		train.GetComponent<ITrain>().Stop();
		//StopCoroutine(coroutine);
		isInPlay = false;
	}

	// ゲーム再開
	public void GameReStart()
	{
		StartCoroutine(IGameReStart());
	}

	// ゲーム再開コルーチン
	IEnumerator IGameReStart()
	{
		countdownCanvas.gameObject.SetActive(true);
		countDown.StartCountDown();
		yield return new WaitUntil(
		() =>
		{
			return !countDown.IsCounting;
		});
		playerObj.GetComponent<IPlayer>().GameStart();
		train.GetComponent<ITrain>().Go();
		isInPlay = true;
	}

	public void LevelMove(string levelName)
	{
		StartCoroutine(ELevelMove(levelName));
	}

	IEnumerator ELevelMove(string levelName)
	{
		fade.FadeOut();
		yield return new WaitUntil(() => { return !fade.IsFading; });

		loadScreen.SetActive(true);

		StartCoroutine(LevelManager.ELoadLevelAsync(levelName));
	}

	IEnumerator EWhistle()
	{
		
		while (true)
		{
			yield return new WaitForSeconds(time);
			source.PlayOneShot(clip);
		}
	}

	public void NotSelectUI()
	{
		cannotSelectUI.SetActive(true);
	}
}