using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public PlayerEnergy energy;
    public float meleeDamage = 10f;
    public float magicDamage = 25f;
    public float magicCost = 20f;
    public float attackRange = 2f;
    public LayerMask enemyLayer;

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Click derecho
        {
            MeleeAttack();
        }
        else if (Input.GetMouseButtonDown(0)) // Click izquierdo
        {
            MagicAttack();
        }
    }

    void MeleeAttack()
    {
        animator.SetTrigger("IsMeleeAttack");
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position + transform.forward * attackRange, 1.5f, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>()?.TakeDamage(meleeDamage);
        }
    }

    void MagicAttack()
    {
        if (energy.currentEnergy >= magicCost)
        {
            animator.SetTrigger("IsKainMagicAttack");
            energy.UseEnergy(magicCost);

            Collider[] hitEnemies = Physics.OverlapSphere(transform.position + transform.forward * attackRange, 2f, enemyLayer);

            foreach (Collider enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyHealth>()?.TakeDamage(magicDamage);
            }

            // Aquí más adelante se añadirá el efecto visual del hechizo
        }
        else
        {
            Debug.Log("No hay suficiente energía para lanzar magia.");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * attackRange, 1.5f);
    }
}
