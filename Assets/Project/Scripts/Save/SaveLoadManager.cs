using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Que no se destruya al cambiar escena
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private string GetSavePath(int slot)
    {
        return Application.persistentDataPath + "/save_slot_" + slot + ".json";
    }

    public void SaveGame(int slot, SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetSavePath(slot), json);
    }

    public SaveData LoadGame(int slot)
    {
        string path = GetSavePath(slot);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<SaveData>(json);
        }
        return null;
    }

    public bool SaveExists(int slot)
    {
        return File.Exists(GetSavePath(slot));
    }
}
