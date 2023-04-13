using UnityEngine;
using UnityEngine.Audio;

// 音量の設定
public class SoundManager : MonoBehaviour
{
	// AudioMixer
	[SerializeField]
	AudioMixer audioMixer;

	// BGMの音量設定
	public void SetBGMVolume(float value)
	{
		audioMixer.SetFloat("BGMVolume", value);
	}

	// SEの音量設定
	public void SetSEVolume(float value)
	{
		audioMixer.SetFloat("SE", value);
	}
}
