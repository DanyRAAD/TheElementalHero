using UnityEngine;
using UnityEngine.UI;

public class VolumenUIManager : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        
        musicSlider.value = PlayerPrefs.GetFloat("music_volume", 0.3f);
        sfxSlider.value = PlayerPrefs.GetFloat("sfx_volume", 0.3f);

        musicSlider.onValueChanged.AddListener((value) => {
            UIAudioManager.Instance?.SetMusicVolume(value);
        });

        sfxSlider.onValueChanged.AddListener((value) => {
            UIAudioManager.Instance?.SetSFXVolume(value);
        });

        
        UIAudioManager.Instance?.SetMusicVolume(musicSlider.value);
        UIAudioManager.Instance?.SetSFXVolume(sfxSlider.value);
    }

    public void GuardarVolumen()
    {
        UIAudioManager.Instance?.SaveVolumes();
    }

    public void RestablecerVolumen()
    {
        musicSlider.value = 0.3f;
        sfxSlider.value = 0.3f;
        UIAudioManager.Instance?.ResetVolumes();
    }
}
