using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// �t�F�[�h�Ǘ��N���X
public class FadeIO : MonoBehaviour
{
	// �t�F�[�h������摜
	[SerializeField]
	Image fadeImg;

	// �t�F�[�h���x
	[SerializeField]
	float fadeSpeed;

	// ���e�͈�
	[SerializeField]
	float toleranceValue;

	// �t�F�[�h��
	bool isFading = false;
	public bool IsFading { get => isFading; }

	// �t�F�[�h�C��
	public void FadeIn()
	{
		StartCoroutine(EFadeIn());
	}

	// �t�F�[�h�C���R���[�`��
	IEnumerator EFadeIn()
	{
		isFading = true;
		fadeImg.color = Color.black;
		while (fadeImg.color.a >= toleranceValue)
		{
			yield return null;
			// �A���t�@�𔲂�
			fadeImg.color -= new Color(0, 0, 0, fadeSpeed);
		}
		isFading = false;
	}

	// �t�F�[�h�A�E�g
	public void FadeOut()
	{
		StartCoroutine(EFadeOut());
	}

	// �t�F�[�h�A�E�g�R���[�`��
	IEnumerator EFadeOut()
	{
		isFading = true;
		fadeImg.color = new Color(0, 0, 0, 0);
		while (fadeImg.color.a <= 1 - toleranceValue)
		{
			yield return null;
			// �F��Z�����Ă���
			fadeImg.color += new Color(0, 0, 0, fadeSpeed);
		}
		isFading = false;
	}
}