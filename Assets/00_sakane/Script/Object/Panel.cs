using System.Collections.Generic;
using UnityEngine;

// �p�l��
public class Panel : MonoBehaviour,IPanel
{
	[SerializeField]
	// �q�����Ă������
	List<Direction> linkDirections;

	// true = �d�Ԃ�����Ă���
	bool isTrain = false;

	// �����ŏオ�鑬�x
	[SerializeField]
	float speedUpValue = 0.4f;
	// �J�[�u�ŉ����鑬�x
	[SerializeField]
	float speedDownValue = 0.4f;

	private void OnTriggerEnter(Collider other)
	{
		// �v���C���[�����������ꍇ
		if (other.gameObject.CompareTag("Train"))
		{
			var hitPoint = other.ClosestPoint(transform.position);

















			// �������������ɓ������邩���ׂ�
			Direction hitDir = HitDirection(other.transform.position);
			var isLoad = IsLoad(hitDir);
			if (!isLoad)
			{
				// �Q�[���I��
				GameObject.FindObjectOfType<GameManager>().GameOver();
			}
			else
			{
				// ���������ׂ�
				if ((linkDirections.IndexOf(Direction.LEFT) != -1) && (linkDirections.IndexOf(Direction.RIGHT) != -1) || (linkDirections.IndexOf(Direction.DOWN) != -1) && (linkDirections.IndexOf(Direction.UP) != -1))
				{
					other.GetComponent<ITrain>().AddSpeed(speedUpValue);
				}
				else
				{
					other.GetComponent<ITrain>().AddSpeed(speedDownValue);
				}

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

		if (x > y)
		{
			if (x > 0)
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
			if (y > 0)
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
		if(linkDirections.IndexOf(dir) != -1)
		{
			return true;
		}
		return false;
	}

	// �����ݒ�
	void IPanel.SetLinkDirection(List<Direction> directions)
	{
		linkDirections = directions;
	}

	// �d�Ԃ�����Ă��邩����
	bool IPanel.IsOnTrain()
	{
		return isTrain;
	}
}