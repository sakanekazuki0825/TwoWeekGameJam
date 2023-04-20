using UnityEngine;

public class BackGroundSpawner : MonoBehaviour
{
	// �w�i�I�u�W�F�N�g
	[SerializeField]
	GameObject backGroundObj;

	public void Spawn()
	{
		// ���C���J�����̈ʒu�擾
		var startPos = Camera.main.transform.position;
		startPos = new Vector3(startPos.x, startPos.y, 0);
		//// �ŏ��̃I�u�W�F�N�g�𐶐�
		//Instantiate(backGroundObj, startPos, Quaternion.identity);
		// �w�i�摜�̃T�C�Y�擾
		var backGroundSize = Instantiate(backGroundObj, startPos, Quaternion.identity).GetComponent<SpriteRenderer>().bounds.size;
		// ���H�摜�̃T�C�Y�擾
		var spriteSize = GameInstance.panelManager.SpriteSize;
		// �p�l�����Ō�ɐ��������ʒu
		Vector3 lastPosition = spriteSize * (GameInstance.panelManager.NumberToGoal + GameInstance.panelManager.StartHorizontalPanel);
		// �ŏ��̈ʒu����S�[���܂ł̋������v�Z
		var dist = lastPosition - startPos;
		// �w�i�𐶐�����K�v�̂��鐔�v�Z
		var backGroundNumber = dist.x / backGroundSize.x;
		for (int i = 0; i < backGroundNumber; ++i)
		{
			Instantiate(backGroundObj, startPos + new Vector3(backGroundSize.x * (i + 1), 0, 0), Quaternion.identity);
		}
	}
}