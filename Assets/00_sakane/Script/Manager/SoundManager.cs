using UnityEngine;
using UnityEngine.Audio;

// ���ʂ̐ݒ�
public class SoundManager : MonoBehaviour
{
	// AudioMixer
	[SerializeField]
	AudioMixer audioMixer;

	// BGM�̉��ʐݒ�
	public void SetBGMVolume(float value)
	{
		audioMixer.SetFloat("BGMVolume", value);
	}

	// SE�̉��ʐݒ�
	public void SetSEVolume(float value)
	{
		audioMixer.SetFloat("SE", value);
	}
}
