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

	float beforeBGMVolume;
	float beforeSEVolume;

	private void OnDisable()
	{
		bgmVolume = PlayerPrefs.GetFloat("BGM", -20);
		audioMixer.SetFloat("BGMVolume", bgmVolume);
		seVolume = PlayerPrefs.GetFloat("SE", -20);
		audioMixer.SetFloat("SEVolume", seVolume);
	}

	private void OnEnable()
	{
		// BGMの音量をスライダーに設定
		audioMixer.SetFloat("BGMVolume", bgmVolume);
		bgmSlider.value = bgmVolume;
		// SEの音量をスライダーに設定
		audioMixer.SetFloat("SEVolume", seVolume);
		seSlider.value = seVolume;

		beforeBGMVolume = bgmVolume;
		beforeSEVolume = seVolume;
	}

	public void Apply()
	{
		SetBGMVolume();
		SetSEVolume();
		PlayerPrefs.SetFloat("BGM", bgmSlider.value);
		PlayerPrefs.SetFloat("SE", seSlider.value);
	}

	public void ReturnClick()
	{
		audioMixer.SetFloat("BGM", beforeBGMVolume);
		audioMixer.SetFloat("SE", beforeSEVolume);
		gameObject.SetActive(false);
	}

	// BGMの音量設定
	void SetBGMVolume()
	{
		bgmVolume = bgmSlider.value;
		beforeBGMVolume = bgmVolume;
	}

	// SEの音量設定
	void SetSEVolume()
	{
		seVolume = seSlider.value;
		beforeSEVolume = seVolume;
	}

	public void ApplyBGMVolume()
	{
		audioMixer.SetFloat("BGM", bgmSlider.value);
	}
	
	public void ApplySEVolume()
	{
		audioMixer.SetFloat("SE", seSlider.value);
	}
}
