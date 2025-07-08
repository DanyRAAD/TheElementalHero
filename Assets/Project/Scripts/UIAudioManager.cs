using UnityEngine;

public class UIAudioManager : MonoBehaviour
{
    public static UIAudioManager Instance;

    private AudioSource audioSource;

    private const string VOLUME_KEY = "ui_volume";
    private const float DEFAULT_VOLUME = 0.3f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // si quieres mantener entre escenas
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat(VOLUME_KEY, DEFAULT_VOLUME);
        audioSource.volume = savedVolume;
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
            audioSource.PlayOneShot(clip);
    }

    public void SetVolume(float value)
    {
        audioSource.volume = value;
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat(VOLUME_KEY, audioSource.volume);
        PlayerPrefs.Save();
    }

    public void ResetVolume()
    {
        audioSource.volume = DEFAULT_VOLUME;
    }

    public float GetCurrentVolume()
    {
        return audioSource.volume;
    }
}
