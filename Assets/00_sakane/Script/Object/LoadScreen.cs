using UnityEngine;

// 読み込み中に表示される画面
public class LoadScreen : MonoBehaviour
{
	// 読み込み中表示するアイコン
	[SerializeField]
	GameObject loadIcon;

	// 回転速度
	[SerializeField]
	float rotSpeed = 5.0f;

	private void Update()
	{
		// 回転させる
		loadIcon.transform.Rotate(new Vector3(0, 0, rotSpeed));
	}
}
