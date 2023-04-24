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

		if (target != null)
		{
			var pos = target.transform.position + offset;
			transform.position = new Vector3(pos.x, transform.position.y, pos.z);
		}
	}

	// ターゲット設定
	void ICannotSelectPanel.SetTarget(UnityEngine.GameObject target)
	{
		this.target = target;
	}
}
