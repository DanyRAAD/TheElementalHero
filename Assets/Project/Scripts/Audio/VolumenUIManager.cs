using UnityEngine;
using UnityEngine.UI;

public class VolumenUIManager : MonoBehaviour
{
    public Slider efectosSlider;

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("ui_volume", 0.3f);
        efectosSlider.value = savedVolume;
        UIAudioManager.Instance?.SetVolume(savedVolume);

        efectosSlider.onValueChanged.AddListener(delegate {
            UIAudioManager.Instance?.SetVolume(efectosSlider.value);
        });
    }

    public void GuardarVolumen()
    {
        UIAudioManager.Instance?.SaveVolume();
    }

    public void RestablecerVolumen()
    {
        efectosSlider.value = 0.3f;
        UIAudioManager.Instance?.ResetVolume();
        UIAudioManager.Instance?.SaveVolume();
    }
}
