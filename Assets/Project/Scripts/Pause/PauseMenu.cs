using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject confirmationPanel;

    public static bool isPaused = false; // Para que otros scripts puedan consultarlo

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

        // Mostrar y desbloquear el cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        confirmationPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        // Ocultar y bloquear el cursor al centro
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
