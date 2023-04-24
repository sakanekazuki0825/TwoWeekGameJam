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

	private void OnEnable()
	{
		bgmVolume = PlayerPrefs.GetFloat("BGM");
		seVolume = PlayerPrefs.GetFloat("SE");
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
