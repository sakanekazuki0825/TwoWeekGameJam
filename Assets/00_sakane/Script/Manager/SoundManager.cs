using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// ���ʂ̐ݒ�
public class SoundManager : MonoBehaviour
{
	// AudioMixer
	[SerializeField]
	AudioMixer audioMixer;

	// bgm�X���C�_�[
	[SerializeField]
	Slider bgmSlider;
	// se�X���C�_�[
	[SerializeField]
	Slider seSlider;

	public void Apply()
	{
		SetBGMVolume();
		SetSEVolume();
		gameObject.SetActive(false);
	}

	// BGM�̉��ʐݒ�
	void SetBGMVolume()
	{
		audioMixer.SetFloat("BGM", bgmSlider.value);
	}

	// SE�̉��ʐݒ�
	void SetSEVolume()
	{
		audioMixer.SetFloat("SE", seSlider.value);
	}
}
