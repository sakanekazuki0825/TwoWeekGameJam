using UnityEngine;
using UnityEngine.Audio;

// âπó ÇÃê›íË
public class SoundManager : MonoBehaviour
{
	// AudioMixer
	[SerializeField]
	AudioMixer audioMixer;

	// BGMÇÃâπó ê›íË
	public void SetBGMVolume(float value)
	{
		audioMixer.SetFloat("BGMVolume", value);
	}

	// SEÇÃâπó ê›íË
	public void SetSEVolume(float value)
	{
		audioMixer.SetFloat("SE", value);
	}
}
