using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject confirmationPanel;

    public PlayerHealth playerHealth;             
    public CheckpointManager checkpointManager;  
    public Transform playerTransform;              
    public static bool isPaused = false;

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

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        confirmationPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

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
        SaveData data = new SaveData();

        data.playerPosition = playerTransform.position;
        data.playerRotation = playerTransform.rotation;
        data.playerHealth = Mathf.RoundToInt(playerHealth.currentHealth);
        data.playerShield = Mathf.RoundToInt(playerHealth.currentShield);
        data.checkpointID = checkpointManager.GetLastCheckpointID();
        

        SaveLoadManager.instance.SaveGame(1, data);

        Debug.Log("Juego guardado correctamente.");
    }
}
