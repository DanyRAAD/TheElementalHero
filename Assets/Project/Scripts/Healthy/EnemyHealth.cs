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

    
    public void TakeDamage(float amount, bool isMeleeAttack = false)
    {
        if (isDead) return;

        if (enemyType == EnemyType.Golem && isMeleeAttack)
        {
            amount = 5f; 
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
                enemyController.Die();  
            }
            else
            {
                Animator anim = GetComponent<Animator>();
                if (anim != null)
                {
                    anim.SetTrigger("IsDying");
                }
                gameObject.SetActive(false);  
            }
        }
        else
        {
            
            health = 100f; 
            gameObject.SetActive(true);
            
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
