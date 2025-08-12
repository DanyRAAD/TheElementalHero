using UnityEngine;

public class GameLoader : MonoBehaviour
{
    public Transform playerTransform;
    public CheckpointManager checkpointManager;

    void Start()
    {
        if (GameState.cargarPartidaGuardada)
        {
            SaveData data = SaveLoadManager.instance.LoadGame(1);

            if (data != null)
            {
                playerTransform.position = data.playerPosition;
                playerTransform.rotation = data.playerRotation;

                checkpointManager.SetCheckpoint(data.checkpointID, data.playerPosition);

                PlayerHealth playerHealth = playerTransform.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.currentHealth = data.playerHealth;
                    playerHealth.currentShield = data.playerShield;
                    playerHealth.healthBar.SetHealth(data.playerHealth);
                    playerHealth.shieldBar.SetHealth(data.playerShield);
                }

                Debug.Log("Partida cargada desde checkpoint: " + data.checkpointID);
            }
            else
            {
                Debug.Log("No hay partida guardada. Iniciando juego nuevo.");
            }
        }
        else
        {
            Debug.Log("Nueva partida iniciada sin cargar datos previos.");
            
        }
    }
}
