using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject confirmationPanel;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused) PauseGame();
            else ResumeGame();
        }
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        confirmationPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void AskExitToMenu()
    {
        
        confirmationPanel.SetActive(true);
        confirmationPanel.GetComponent<Confirmation>().action = Confirmation.Action.ExitToMenu;
    }

    public void AskQuitGame()
    {
        
        confirmationPanel.SetActive(true);
        confirmationPanel.GetComponent<Confirmation>().action = Confirmation.Action.QuitGame;
    }

    public void SaveGame()
    {
        Debug.Log("Guardar juego (función pendiente)");
    }
}
