//using UnityEditor.Overlays;
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

                // Restaurar estado báculo
                BaculoAdherente baculo = FindObjectOfType<BaculoAdherente>();
                if (baculo != null)
                {
                    baculo.SetEstadoAdherido(data.baculoAdherido);
                }

                PlayerEconomy economia = playerTransform.GetComponent<PlayerEconomy>();
                if (economia != null)
                {
                    economia.SetMonedas(data.monedas);
                }
                PotionInventory potionInventory = playerTransform.GetComponent<PotionInventory>();

                if (potionInventory != null)
                {
                    potionInventory.SetPocionesVida(data.pocionesVida);
                    potionInventory.SetPocionesEscudo(data.pocionesEscudo);
                }

               

              

                if (data != null)
                {
                    

                    
                    foreach (EnemySaveData enemyData in data.enemies)
                    {
                        GameObject enemyGO = GameObject.Find(enemyData.enemyID);
                        if (enemyGO != null)
                        {
                            EnemyHealth enemy = enemyGO.GetComponent<EnemyHealth>();
                            if (enemy != null)
                            {
                                enemy.health = enemyData.health;
                                enemy.SetDeadState(enemyData.isDead);  
                            }
                        }
                    }
                }

                
               

                if (data != null)
                {
                    
                    foreach (ChestSaveData chestData in data.chests)
                    {
                        GameObject chestGO = GameObject.Find(chestData.chestID);
                        if (chestGO != null)
                        {
                            ChestInteraction cofre = chestGO.GetComponent<ChestInteraction>();
                            if (cofre != null)
                            {
                                cofre.cofreAbierto = chestData.isOpen;
                                if (chestData.isOpen)
                                {
                                    
                                    cofre.animatorCofre.SetBool("isOpen", true);
                                    cofre.mensajeUI.SetActive(false);
                                }
                            }
                        }
                    }
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
