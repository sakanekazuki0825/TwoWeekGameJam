using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	// ゲーム開始
	public void GameStart()
	{
		StartCoroutine(EGameStart());
	}

	// ゲーム読み込みコルーチン
	IEnumerator EGameStart()
	{
		// シーン読み込み開始
		StartCoroutine(LevelManager.ELoadLevelAsync(""));

		// ロードが終わるまで待つ（なにかアニメーションさせる）
		while (LevelManager.IsLoading)
		{
			yield return null;
		}
	}

	// ゲーム終了
	public void GameQuit()
	{
		LevelManager.GameFinish();
	}
}