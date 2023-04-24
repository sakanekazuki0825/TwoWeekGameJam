using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
	// ÉXÉRÉA
	public static List<float> scores = new List<float>();

	private void Awake()
	{
		SceneManager.sceneLoaded += OnSceneChange;
	}

	private void Start()
	{
		DontDestroyOnLoad(gameObject);
	}

	[RuntimeInitializeOnLoadMethod]
	static void Initialize()
	{
		for (int i = 0; i < 10; ++i)
		{
			if (PlayerPrefs.HasKey(i.ToString()))
			{
				scores.Add(PlayerPrefs.GetFloat(i.ToString()));
			}
		}
	}

	void OnApplicationQuit()
	{
		foreach (var item in scores)
		{
			PlayerPrefs.SetFloat(scores.IndexOf(item).ToString(), item);
		}
#if UNITY_EDITOR
		PlayerPrefs.DeleteAll();
#endif
	}

	void OnSceneChange(Scene scene, LoadSceneMode mode)
	{
		SceneManager.MoveGameObjectToScene(gameObject, scene);
	}

	private void OnDestroy()
	{
		SceneManager.sceneLoaded -= OnSceneChange;
	}
}