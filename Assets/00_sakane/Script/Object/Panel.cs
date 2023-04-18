using System.Collections.Generic;
using UnityEngine;

// �p�l��
public class Panel : MonoBehaviour, IPanel
{
	[SerializeField]
	// �q�����Ă������
	List<Vector2> linkDirections;

	// true = �d�Ԃ�����Ă���
	bool isTrain = false;

	// �X�v���C�g��\������N���X
	SpriteRenderer spriteRenderer;

	// �ω������鑬�x
	float speedChangeValue = 0.4f;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

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
				// �Q�[���I�[�o�[
				GameInstance.gameManager.GameOver();
			}
			else
			{
				other.GetComponent<ITrain>().AddSpeed(speedChangeValue);

				var targetPos = linkDirections[(linkDirections.IndexOf(hitDir) + 1) % 2] * PanelManager.SpriteSize;
				// �d�Ԃ̖ړI�n�i�p�l�����ړI�n�ݒ�͈�a��������j
				other.GetComponent<ITrain>().Curve(transform.position, transform.position + new Vector3(targetPos.x, targetPos.y));
			}
		}
		else if (other.gameObject.CompareTag("CannotSelectPanel"))
		{
			GameInstance.player.SelectPanelRemove(gameObject);
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

	// �X�v���C�g�̐ݒ�
	void IPanel.SetSprite(Sprite sprite)
	{
		spriteRenderer.sprite = sprite;
	}

	// �ω������鑬�x�̐ݒ�
	void IPanel.SetChangeSpeedValue(float speed)
	{
		speedChangeValue = speed;
	}
}