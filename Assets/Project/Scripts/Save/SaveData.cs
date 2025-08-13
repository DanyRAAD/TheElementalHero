using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySaveData
{
    public string enemyID; 
    public float health;
    public bool isDead;
}
[Serializable]
public class ChestSaveData
{
    public string chestID;  
    public bool isOpen;
}
[Serializable]
public class SaveData
{
    public Vector3 playerPosition;   
    public Quaternion playerRotation;
    public int playerHealth;         
    public int playerShield;
    public int checkpointID;
    public bool baculoAdherido;
    public int monedas;
    public int pocionesVida;         
    public int pocionesEscudo;
    public List<EnemySaveData> enemies = new List<EnemySaveData>();
    public List<ChestSaveData> chests = new List<ChestSaveData>();
}
