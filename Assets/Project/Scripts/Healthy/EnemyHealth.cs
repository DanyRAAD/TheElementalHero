using UnityEngine;

public enum EnemyType { Normal, Golem }

public class EnemyHealth : MonoBehaviour
{
    public EnemyType enemyType;
    public float health = 100f;

    private bool isDead = false;
    private EnemyController enemyController;

    void Awake()
    {
        enemyController = GetComponent<EnemyController>();
    }

    // isMeleeAttack indica si el daño es de tipo melee para ajustar daño a golem
    public void TakeDamage(float amount, bool isMeleeAttack = false)
    {
        if (isDead) return;

        if (enemyType == EnemyType.Golem && isMeleeAttack)
        {
            amount = 5f; // Reduce daño melee al golem
        }

        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        if (enemyController != null)
        {
            enemyController.Die();
        }
        else
        {
            // Por si no hay EnemyController, solo dispara animación y desactiva
            Animator anim = GetComponent<Animator>();
            if (anim != null)
            {
                anim.SetTrigger("IsDying");
            }
            // Desactivar el gameobject después de un tiempo para que se vea la animación
            Destroy(gameObject, 3f);
        }
    }
}
