using UnityEngine;

public enum EnemyType { Normal, Golem }

public class EnemyHealth : MonoBehaviour
{
    public EnemyType enemyType;
    public float health = 100f;

    public void TakeDamage(float amount)
    {
        if (enemyType == EnemyType.Golem && amount == 10f) // Si fue melee
        {
            amount = 5f; // Reduce daño
        }

        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GetComponent<Animator>()?.SetTrigger("IsDying");
        // Aquí puedes añadir un respawn si es necesario
    }
}
