using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider musicSlider;

    private const float VOLUMEN_DEFECTO = 0.5f;
    private const string KEY_VOLUMEN_MUSICA = "volumen_musica";

    void Awake()
    {
        DontDestroyOnLoad(gameObject); // Opcional si usas música persistente
    }

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        float savedVol = PlayerPrefs.GetFloat(KEY_VOLUMEN_MUSICA, VOLUMEN_DEFECTO);
        audioSource.volume = savedVol;

        if (musicSlider != null)
            musicSlider.value = savedVol;
    }

    public void CambiarVolumenMusica(float valor)
    {
        audioSource.volume = valor;
    }

    public void GuardarVolumen()
    {
        PlayerPrefs.SetFloat(KEY_VOLUMEN_MUSICA, audioSource.volume);
        PlayerPrefs.Save();
    }

    public void RestablecerVolumen()
    {
        audioSource.volume = VOLUMEN_DEFECTO;

        if (musicSlider != null)
            musicSlider.value = VOLUMEN_DEFECTO;

        GuardarVolumen();
    }
}
