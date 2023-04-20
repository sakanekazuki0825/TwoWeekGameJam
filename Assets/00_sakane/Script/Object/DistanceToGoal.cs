using UnityEngine;
using UnityEngine.UI;

public class DistanceToGoal : MonoBehaviour
{
	// ゴール
	Vector3 goal;
	public Vector3 Goal
	{
		get => goal;
		set => goal = value;
	}
	// 電車
	GameObject train;
	public GameObject Train
	{
		get => train;
		set => train = value;
	}

	// スライダー
	Slider distanceSlider;

	private void Awake()
	{
		GameInstance.distanceToGoal = this;
		// スライダー取得
		distanceSlider = GetComponentInChildren<Slider>();
	}

	private void Update()
	{
		if ((train != null))
		{
			// 距離を入れる
			distanceSlider.value = train.transform.position.x / goal.x;
		}
	}

	private void OnDestroy()
	{
		GameInstance.distanceToGoal = null;
	}
}