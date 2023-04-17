using System.Collections.Generic;
using UnityEngine;

// �p�l��
public class Panel : MonoBehaviour,IPanel
{
	[SerializeField]
	// �q�����Ă������
	List<Vector2> linkDirections;

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
			Vector2 hitDir = HitDirection(other.transform.position);
			var isLoad = IsLoad(hitDir);
			if (!isLoad)
			{
				// �Q�[���I��
				GameObject.FindObjectOfType<GameManager>().GameOver();
			}
			else
			{
				// ���������ׂ�
				if ((linkDirections[0] - linkDirections[1]).magnitude > 1.8f)
				{
					other.GetComponent<ITrain>().AddSpeed(speedUpValue);
				}
				else
				{
					other.GetComponent<ITrain>().AddSpeed(speedDownValue);
				}

				var targetPos = linkDirections[(linkDirections.IndexOf(hitDir) + 1) % 2] * PanelManager.SpriteSize;
				// �d�Ԃ̖ړI�n�i�p�l�����ړI�n�ݒ�͈�a��������j
				other.GetComponent<ITrain>().Curve(transform.position, transform.position + new Vector3(targetPos.x, targetPos.y));
			}
		}
	}

	// �������������𒲂ׂ�
	Vector3 HitDirection(Vector3 position)
	{
		var dir = transform.position - position;
		var x = Mathf.Abs(dir.x);
		var y = Mathf.Abs(dir.y);

		if (x > y)
		{
			if (dir.x > 0)
			{
				return Vector2.left;
			}
			else
			{
				return Vector2.right;
			}
		}
		else
		{
			if (dir.y > 0)
			{
				return Vector2.down;
			}
			else
			{
				return Vector2.up;
			}
		}
	}

	// �������邩���ׂ�
	bool IsLoad(Vector2 dir)
	{
		return linkDirections.Contains(dir);
	}

	// �����ݒ�
	void IPanel.SetLinkDirection(List<Vector2> directions)
	{
		linkDirections = directions;
	}

	// �d�Ԃ�����Ă��邩����
	bool IPanel.IsOnTrain()
	{
		return isTrain;
	}
}