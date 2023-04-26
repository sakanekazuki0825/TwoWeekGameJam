using UnityEngine;

public class CannotSelectPanel : MonoBehaviour,ICannotSelectPanel
{
	// �ǐՂ���I�u�W�F�N�g
	GameObject target;
	// �ǐՂ���I�t�Z�b�g
	Vector3 offset;

	void Start()
	{
		// �I�t�Z�b�g�̐ݒ�
		offset = transform.position - target.transform.position;
	}

	void Update()
	{
		var position = transform.position + offset;
		position.y = 0;

		if (target != null)
		{
			//var pos = target.transform.position + offset;
			//transform.position = new Vector3(pos.x, transform.position.y, pos.z);

			var pos = target.transform.position - new Vector3(0.1f, 0, 0);
			var number = pos.x / GameInstance.panelManager.SpriteSize.x;
			number = Mathf.Floor(number);
			pos = new Vector3(GameInstance.panelManager.SpriteSize.x * number - GameInstance.panelManager.SpriteSize.x / 2, 0, pos.z);

			transform.position = new Vector3(pos.x - 0.01f,transform.position.y, pos.z);
		}
	}

	// �^�[�Q�b�g�ݒ�
	void ICannotSelectPanel.SetTarget(UnityEngine.GameObject target)
	{
		this.target = target;
	}
}
