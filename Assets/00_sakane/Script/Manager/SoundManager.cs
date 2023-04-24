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

	private void OnEnable()
	{
		var volume = 0.0f;
		// BGM�̉��ʂ��X���C�_�[�ɐݒ�
		audioMixer.GetFloat("BGMVolume",out volume);
		bgmSlider.value = volume;
		// SE�̉��ʂ��X���C�_�[�ɐݒ�
		audioMixer.GetFloat("SEVolume", out volume);
		seSlider.value = volume;
	}

	public void Apply()
	{
		SetBGMVolume();
		SetSEVolume();
		gameObject.SetActive(false);
	}

	// BGM�̉��ʐݒ�
	void SetBGMVolume()
	{
		audioMixer.SetFloat("BGMVolume", bgmSlider.value);
	}

	// SE�̉��ʐݒ�
	void SetSEVolume()
	{
		audioMixer.SetFloat("SEVolume", seSlider.value);
	}
}
