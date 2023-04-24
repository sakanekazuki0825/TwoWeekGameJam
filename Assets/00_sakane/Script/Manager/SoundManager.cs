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

	public void Apply()
	{
		SetBGMVolume();
		SetSEVolume();
		gameObject.SetActive(false);
	}

	// BGMの音量設定
	void SetBGMVolume()
	{
		audioMixer.SetFloat("BGM", bgmSlider.value);
	}

	// SEの音量設定
	void SetSEVolume()
	{
		audioMixer.SetFloat("SE", seSlider.value);
	}
}
