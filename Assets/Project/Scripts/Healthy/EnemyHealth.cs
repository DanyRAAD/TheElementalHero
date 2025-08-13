using UnityEngine;

public enum EnemyType { Normal, Golem }

public class EnemyHealth : MonoBehaviour
{
    public EnemyType enemyType;
    public float health = 100f;

    public bool isDead = false;
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
    public void SetDeadState(bool dead)
    {
        isDead = dead;

        if (isDead)
        {
            if (enemyController != null)
            {
                enemyController.Die();  // Esto debería desactivar o animar la muerte
            }
            else
            {
                Animator anim = GetComponent<Animator>();
                if (anim != null)
                {
                    anim.SetTrigger("IsDying");
                }
                gameObject.SetActive(false);  // En lugar de destruir, para que quede oculto
            }
        }
        else
        {
            // Reiniciar vida, animaciones o estados si es necesario para enemigo vivo
            health = 100f; // O el valor que corresponda
            gameObject.SetActive(true);
            // Reiniciar animaciones o lógica que tengas
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
            Animator anim = GetComponent<Animator>();
            if (anim != null)
            {
                anim.SetTrigger("IsDying");
            }
            // Desactiva en lugar de destruir para mantener referencia en guardado
            gameObject.SetActive(false);
        }
    }

}
