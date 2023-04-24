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

	private void OnEnable()
	{
		bgmVolume = PlayerPrefs.GetFloat("BGM");
		seVolume = PlayerPrefs.GetFloat("SE");
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
