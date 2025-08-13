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
        
        string lang = PlayerPrefs.GetInt("language", 0) == 0 ? "ES" : "EN";
        string videoFile = lang == "ES" ? mainCinematic_ES : mainCinematic_EN;

        
        PlayVideo(videoFile);

        skipButton.SetActive(true);
        characterSelectionUI.SetActive(false);

        videoPlayer.loopPointReached += OnVideoEnd;
    }

    
    void PlayVideo(string fileName)
    {
        string path = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);
        path = path.Replace("\\", "/");

        Debug.Log("Reproduciendo: " + path);
        videoPlayer.url = path;
        videoPlayer.Play();
    }

   
    public void SkipCinematic()
    {
        skipPressed = true;
        videoPlayer.Stop();
        ShowCharacterSelection();
    }


    void OnVideoEnd(VideoPlayer vp)
    {
        if (isMainCinematic && !skipPressed)
        {
            ShowCharacterSelection();
        }
        else if (!isMainCinematic)
        {
            
            SceneManager.LoadScene("RuinasAntiguas");
        }
    }

   
    void ShowCharacterSelection()
    {
        skipButton.SetActive(false);
        isMainCinematic = false;
        characterSelectionUI.SetActive(true);
    }

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
