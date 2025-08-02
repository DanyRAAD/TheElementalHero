using UnityEngine;
using UnityEngine.SceneManagement;


public class UIAudioManager : MonoBehaviour
{
    public static UIAudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicAudioSource;
    public AudioSource sfxAudioSource;

    private const string MUSIC_VOLUME_KEY = "music_volume";
    private const string SFX_VOLUME_KEY = "sfx_volume";
    private const float DEFAULT_VOLUME = 0.3f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, DEFAULT_VOLUME);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, DEFAULT_VOLUME);

        musicAudioSource.volume = musicVolume;
        sfxAudioSource.volume = sfxVolume;
    }

    // Para efectos de UI
    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
            sfxAudioSource.PlayOneShot(clip);
    }

    public void SetMusicVolume(float value)
    {
        musicAudioSource.volume = value;
    }

    public void SetSFXVolume(float value)
    {
        sfxAudioSource.volume = value;
    }

    public void SaveVolumes()
    {
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, musicAudioSource.volume);
        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, sfxAudioSource.volume);
        PlayerPrefs.Save();
    }

    public void ResetVolumes()
    {
        musicAudioSource.volume = DEFAULT_VOLUME;
        sfxAudioSource.volume = DEFAULT_VOLUME;
        SaveVolumes();
    }

    public float GetMusicVolume() => musicAudioSource.volume;
    public float GetSFXVolume() => sfxAudioSource.volume;
}
