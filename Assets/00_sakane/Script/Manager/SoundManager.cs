using System.ComponentModel;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// 音量の設定
public class SoundManager : MonoBehaviour
{
	// AudioMixer
	[SerializeField]
	AudioMixer audioMixer;

	// bgmスライダー
	[SerializeField]
	Slider bgmSlider;
	// seスライダー
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
		// BGMの音量をスライダーに設定
		audioMixer.SetFloat("BGMVolume", bgmVolume);
		bgmSlider.value = bgmVolume;
		// SEの音量をスライダーに設定
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

	// BGMの音量設定
	void SetBGMVolume()
	{
		bgmVolume = bgmSlider.value;
		audioMixer.SetFloat("BGMVolume", bgmVolume);
	}

	// SEの音量設定
	void SetSEVolume()
	{
		seVolume = seSlider.value;
		audioMixer.SetFloat("SEVolume", seVolume);
	}
}
