using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public PlayerEnergy energy;
    public float meleeDamage = 10f;
    public float magicDamage = 25f;
    public float magicCost = 20f;
    public float attackRange = 2f;
    public LayerMask enemyLayer;

    private float attackCooldown = 0.5f;
    private float lastAttackTime = -1f;

    
    public GameObject magicProjectilePrefab;       
    public Transform magicSpawnPoint;              
    public float projectileSpeed = 20f;            


    void Update()
    {
        if (Time.timeScale == 0f) return;

        if (Time.time - lastAttackTime >= attackCooldown)
        {
            if (Input.GetMouseButtonDown(1))
            {
                MeleeAttack();
            }
            else if (Input.GetMouseButtonDown(0))
            {
                MagicAttack();
            }
        }
    }

    void MeleeAttack()
    {
        lastAttackTime = Time.time;

        
        animator.ResetTrigger("IsMeleeAttack");
        animator.SetTrigger("IsMeleeAttack");

        
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position + transform.forward * attackRange, 1.5f, enemyLayer);
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>()?.TakeDamage(meleeDamage, true);
        }
    }

    void MagicAttack()
    {
        if (energy.currentEnergy >= magicCost)
        {
            lastAttackTime = Time.time;
            animator.SetTrigger("IsKainMagicAttack");
            energy.UseEnergy(magicCost);

            StartCoroutine(LaunchMagicAfterDelay(0.6f)); // Espera 0.5 segundos
        }
        else
        {
            Debug.Log("No hay suficiente energía para lanzar magia.");
        }
    }
    IEnumerator LaunchMagicAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        GameObject projectile = Instantiate(magicProjectilePrefab, magicSpawnPoint.position, magicSpawnPoint.rotation);

        // Apunta horizontalmente hacia el frente
        Vector3 dir = transform.forward;
        dir.y = 0;
        projectile.transform.rotation = Quaternion.LookRotation(dir);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * attackRange, 1.5f);
    }
}
