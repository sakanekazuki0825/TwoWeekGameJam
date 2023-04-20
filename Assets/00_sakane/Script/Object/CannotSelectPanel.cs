using UnityEngine;

public class CannotSelectPanel : MonoBehaviour,ICannotSelectPanel
{
	// 追跡するオブジェクト
	GameObject target;
	// 追跡するオフセット
	Vector3 offset;

	void Start()
	{
		// オフセットの設定
		offset = transform.position - target.transform.position;
	}

	void Update()
	{
		var position = transform.position + offset;
		position.y = 0;

		transform.position = target.transform.position + offset;
	}

	// ターゲット設定
	void ICannotSelectPanel.SetTarget(UnityEngine.GameObject target)
	{
		this.target = target;
	}
}
