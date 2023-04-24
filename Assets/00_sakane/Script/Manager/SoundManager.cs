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

	private void OnEnable()
	{
		var volume = 0.0f;
		// BGMの音量をスライダーに設定
		audioMixer.GetFloat("BGMVolume",out volume);
		bgmSlider.value = volume;
		// SEの音量をスライダーに設定
		audioMixer.GetFloat("SEVolume", out volume);
		seSlider.value = volume;
	}

	public void Apply()
	{
		SetBGMVolume();
		SetSEVolume();
		gameObject.SetActive(false);
	}

	// BGMの音量設定
	void SetBGMVolume()
	{
		audioMixer.SetFloat("BGMVolume", bgmSlider.value);
	}

	// SEの音量設定
	void SetSEVolume()
	{
		audioMixer.SetFloat("SEVolume", seSlider.value);
	}
}
