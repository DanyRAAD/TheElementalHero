using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CinematicController : MonoBehaviour
{
    [Header("Componentes de la escena")]
    public VideoPlayer videoPlayer;
    public GameObject skipButton;
    public GameObject characterSelectionUI;

    [Header("Archivos de video")]
    public string mainCinematic_ES;
    public string mainCinematic_EN;
    public string cinematicKain_ES;
    public string cinematicKain_EN;
    public string cinematicJael_ES;
    public string cinematicJael_EN;

    private bool skipPressed = false;
    private bool isMainCinematic = true;
    private string selectedCharacter = "";

    void Start()
    {
        // Selección de idioma desde PlayerPrefs
        string lang = PlayerPrefs.GetInt("language", 0) == 0 ? "ES" : "EN";
        string videoFile = lang == "ES" ? mainCinematic_ES : mainCinematic_EN;

        // Inicia reproducción de cinemática principal
        PlayVideo(videoFile);

        skipButton.SetActive(true);
        characterSelectionUI.SetActive(false);

        videoPlayer.loopPointReached += OnVideoEnd;
    }

    /// <summary>
    /// Reproduce el video desde la carpeta StreamingAssets
    /// </summary>
    void PlayVideo(string fileName)
    {
        string path = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);
        path = path.Replace("\\", "/");

        Debug.Log("Reproduciendo: " + path);
        videoPlayer.url = path;
        videoPlayer.Play();
    }

    /// <summary>
    /// Se llama desde el botón "Skip"
    /// </summary>
    public void SkipCinematic()
    {
        skipPressed = true;
        videoPlayer.Stop();
        ShowCharacterSelection();
    }

    /// <summary>
    /// Evento al finalizar un video
    /// </summary>
    void OnVideoEnd(VideoPlayer vp)
    {
        if (isMainCinematic && !skipPressed)
        {
            ShowCharacterSelection();
        }
        else if (!isMainCinematic)
        {
            // Después de la cinemática de personaje, carga el primer nivel
            SceneManager.LoadScene("RuinasAntiguas"); // Cambia por tu escena real
        }
    }

    /// <summary>
    /// Muestra la UI para elegir personaje
    /// </summary>
    void ShowCharacterSelection()
    {
        skipButton.SetActive(false);
        isMainCinematic = false;
        characterSelectionUI.SetActive(true);
    }

    /// <summary>
    /// Llamado desde los botones de Kain y Jael
    /// </summary>
    public void SelectCharacter(string character)
    {
        selectedCharacter = character;
        characterSelectionUI.SetActive(false);

        string lang = PlayerPrefs.GetInt("language", 0) == 0 ? "ES" : "EN";
        string path = "";

        if (character == "Kain")
            path = lang == "ES" ? cinematicKain_ES : cinematicKain_EN;
        else
            path = lang == "ES" ? cinematicJael_ES : cinematicJael_EN;

        PlayVideo(path);
    }
}
