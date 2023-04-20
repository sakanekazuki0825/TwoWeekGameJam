using UnityEngine;
using UnityEngine.UI;

public class DistanceToGoal : MonoBehaviour
{
	// �S�[��
	Vector3 goal;
	public Vector3 Goal
	{
		get => goal;
		set => goal = value;
	}
	// �d��
	GameObject train;
	public GameObject Train
	{
		get => train;
		set => train = value;
	}

	// �X���C�_�[
	Slider distanceSlider;

	private void Awake()
	{
		GameInstance.distanceToGoal = this;
		// �X���C�_�[�擾
		distanceSlider = GetComponentInChildren<Slider>();
	}

	private void Update()
	{
		if ((train != null))
		{
			// ����������
			distanceSlider.value = train.transform.position.x / goal.x;
		}
	}

	private void OnDestroy()
	{
		GameInstance.distanceToGoal = null;
	}
}