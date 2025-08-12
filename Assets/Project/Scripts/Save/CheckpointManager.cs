using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance;

    private int lastCheckpointID = -1;
    private Vector3 lastCheckpointPosition;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void SetCheckpoint(int id, Vector3 position)
    {
        lastCheckpointID = id;
        lastCheckpointPosition = position;
    }

    public Vector3 GetLastCheckpointPosition()
    {
        return lastCheckpointPosition;
    }

    public int GetLastCheckpointID()
    {
        return lastCheckpointID;
    }
}
