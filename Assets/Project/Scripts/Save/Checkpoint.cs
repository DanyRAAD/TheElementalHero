using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int checkpointID; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckpointManager.instance.SetCheckpoint(checkpointID, transform.position);
            Debug.Log("Checkpoint alcanzado: " + checkpointID);
        }
    }
}
