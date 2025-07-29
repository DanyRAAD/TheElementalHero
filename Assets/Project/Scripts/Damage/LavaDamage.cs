using UnityEngine;

public class LavaDamage : MonoBehaviour
{
    private float damagePerSecond = 30f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }
    }
}
