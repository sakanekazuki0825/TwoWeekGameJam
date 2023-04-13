using System.Collections.Generic;
using UnityEngine;
using MyEnum;

// �p�l��
public class Panel : MonoBehaviour
{
	// �q�����Ă������
	List<Direction> linkDirections;

	private void OnTriggerEnter(Collider other)
	{
		// �v���C���[�����������ꍇ
		if (other.gameObject.CompareTag("Train"))
		{
			// �������������ɓ������邩���ׂ�
			Direction hitDir = HitDirection(other.transform.position);
			var isLoad = IsLoad(hitDir);
			if (!isLoad)
			{
				// �Q�[���I��
				GameObject.FindObjectOfType<GameManager>().GameFinish();
			}
			else
			{
				// �d�Ԃ̈ړ�������ݒ�
				other.GetComponent<ITrain>().Curve(linkDirections[(linkDirections.IndexOf(hitDir) + 1) % 2]);
			}
		}
	}

	// �������������𒲂ׂ�
	Direction HitDirection(Vector3 position)
	{
		var dir = transform.position - position;
		var x = Mathf.Abs(dir.x);
		var y = Mathf.Abs(dir.y);

		if (x < y)
		{
			if (x < 0)
			{
				return Direction.LEFT;
			}
			else
			{
				return Direction.RIGHT;
			}
		}
		else
		{
			if (y < 0)
			{
				return Direction.DOWN;
			}
			else
			{
				return Direction.UP;
			}
		}
	}

	// �������邩���ׂ�
	bool IsLoad(Direction dir)
	{
		foreach (var e in linkDirections)
		{
			if (e == dir)
			{
				return true;
			}
		}
		return false;
	}
}