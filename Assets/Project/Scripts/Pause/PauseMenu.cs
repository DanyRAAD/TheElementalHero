using System;
using UnityEditor.Overlays;
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
        BaculoAdherente baculo = FindObjectOfType<BaculoAdherente>();
        data.baculoAdherido = baculo != null && baculo.GetEstadoAdherido();
        PlayerEconomy economia = playerTransform.GetComponent<PlayerEconomy>();
        if (economia != null)
        {
            data.monedas = economia.GetMonedas();
        }
        PotionInventory potionInventory = playerTransform.GetComponent<PotionInventory>();

        if (potionInventory != null)
        {
            data.pocionesVida = potionInventory.GetPocionesVida();
            data.pocionesEscudo = potionInventory.GetPocionesEscudo();
        }

        EnemyHealth[] allEnemies = GameObject.FindObjectsOfType<EnemyHealth>();

        data.enemies.Clear();

        foreach (EnemyHealth enemy in allEnemies)
        {
            EnemySaveData enemyData = new EnemySaveData
            {
                enemyID = enemy.gameObject.name,  
                health = enemy.health,
                isDead = enemy.isDead
            };
            data.enemies.Add(enemyData);
        }

      
        // Guardar estado de cofres
        ChestInteraction[] cofres = GameObject.FindObjectsOfType<ChestInteraction>();
        data.chests.Clear();

        foreach (ChestInteraction cofre in cofres)
        {
            ChestSaveData chestData = new ChestSaveData
            {
                chestID = cofre.chestID,
                isOpen = cofre.cofreAbierto
            };
            data.chests.Add(chestData);
        }





        SaveLoadManager.instance.SaveGame(1, data);

        Debug.Log("Juego guardado correctamente.");
    }
}
