using System.ComponentModel;
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

	float bgmVolume = 0;
	float seVolume = 0;

	[SerializeField]
	float initialBGMVolume = -20.0f;
	[SerializeField]
	float initialSEVolume = -20.0f;

	private void OnDisable()
	{
		if (PlayerPrefs.HasKey("BGM"))
		{
			bgmVolume = PlayerPrefs.GetFloat("BGM");
			audioMixer.SetFloat("BGMVolume", bgmVolume);
		}
		else
		{
			PlayerPrefs.SetFloat("BGM", initialBGMVolume);
			bgmVolume = initialBGMVolume;
			audioMixer.SetFloat("BGMVolume", bgmVolume);
		}
		if (PlayerPrefs.HasKey("SE"))
		{
			seVolume = PlayerPrefs.GetFloat("SE");
			audioMixer.SetFloat("SEVolume", seVolume);
		}
		else
		{
			PlayerPrefs.SetFloat("SE", initialSEVolume);
			seVolume = initialSEVolume;
			audioMixer.SetFloat("SEVolume", seVolume);
		}
	}

	private void OnEnable()
	{
		// BGM�̉��ʂ��X���C�_�[�ɐݒ�
		audioMixer.SetFloat("BGMVolume", bgmVolume);
		bgmSlider.value = bgmVolume;
		// SE�̉��ʂ��X���C�_�[�ɐݒ�
		audioMixer.SetFloat("SEVolume", seVolume);
		seSlider.value = seVolume;
	}

	public void Apply()
	{
		SetBGMVolume();
		SetSEVolume();
		PlayerPrefs.SetFloat("BGM", bgmSlider.value);
		PlayerPrefs.SetFloat("SE",seSlider.value);
	}

	public void ReturnClick()
	{
		gameObject.SetActive(false);
	}

	// BGM�̉��ʐݒ�
	void SetBGMVolume()
	{
		bgmVolume = bgmSlider.value;
		audioMixer.SetFloat("BGMVolume", bgmVolume);
	}

	// SE�̉��ʐݒ�
	void SetSEVolume()
	{
		seVolume = seSlider.value;
		audioMixer.SetFloat("SEVolume", seVolume);
	}
}
