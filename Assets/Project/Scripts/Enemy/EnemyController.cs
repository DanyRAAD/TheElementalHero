using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("Patrol Points")]
    public Transform patrolPointA;
    public Transform patrolPointB;

    [Header("Combat")]
    public float meleeAttackRange = 2f;
    public float rockThrowRange = 20f;
    public float meleeAttackDamage = 10f;
    public float rockThrowDamage = 15f;

    [Header("References")]
    public GameObject rockPrefab;
    public Transform rockSpawnPoint;

    private NavMeshAgent agent;
    private Animator animator;
    private GameObject player;

    private Transform currentPatrolTarget;
    private bool isWaiting = false;
    private bool isDead = false;

    public GameObject bloqueoZona;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");

        currentPatrolTarget = patrolPointA;
        agent.SetDestination(currentPatrolTarget.position);
    }

    void Update()
    {
        if (isDead) return;

        animator.SetFloat("Speed", agent.velocity.magnitude);

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        
        if (distanceToPlayer <= meleeAttackRange)
        {
            agent.isStopped = true;
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            MeleeAttack();
        }
        else if (distanceToPlayer <= rockThrowRange)
        {
            agent.isStopped = true;
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            RockThrowAttack();
        }
        else
        {
            
            agent.isStopped = false;
            Patrol();
        }
    }

    void Patrol()
    {
        if (isWaiting) return;

        agent.SetDestination(currentPatrolTarget.position);

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            StartCoroutine(WaitAndSwitchPatrolPoint());
        }
    }

    IEnumerator WaitAndSwitchPatrolPoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(1f);
        currentPatrolTarget = currentPatrolTarget == patrolPointA ? patrolPointB : patrolPointA;
        isWaiting = false;
    }

    private float lastMeleeAttackTime = 0f;
    private float meleeAttackCooldown = 1.5f;

    void MeleeAttack()
    {
        if (Time.time - lastMeleeAttackTime < meleeAttackCooldown)
            return;

        lastMeleeAttackTime = Time.time;

        animator.SetTrigger("IsMeleeAttack");

        
        Collider[] hits = Physics.OverlapSphere(transform.position + transform.forward * meleeAttackRange, 1.5f);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                var playerHealth = hit.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(meleeAttackDamage);
                }
            }
        }
    }

    private float lastRockThrowTime = 0f;
    private float rockThrowCooldown = 3f;

    void RockThrowAttack()
    {
        if (Time.time - lastRockThrowTime < rockThrowCooldown)
            return;

        lastRockThrowTime = Time.time;

        animator.SetTrigger("IsGolemMagicAttack");

        
        StartCoroutine(SpawnAndThrowRock());
    }

    IEnumerator SpawnAndThrowRock()
    {
        yield return new WaitForSeconds(0.5f);

        if (rockPrefab != null && rockSpawnPoint != null)
        {
            GameObject rock = Instantiate(rockPrefab, rockSpawnPoint.position, rockSpawnPoint.rotation);

            RockProjectile projectile = rock.GetComponent<RockProjectile>();
            if (projectile != null)
            {
                projectile.SetTarget(player.transform);
            }

            
            Collider rockCollider = rock.GetComponent<Collider>();
            Collider golemCollider = GetComponent<Collider>();
            if (rockCollider != null && golemCollider != null)
            {
                Physics.IgnoreCollision(rockCollider, golemCollider);
            }
        }
    }





    public void Die()
    {
        isDead = true;
        agent.isStopped = true;
        animator.SetTrigger("IsDead");

       
        if (bloqueoZona != null)
        {
            bloqueoZona.SetActive(false); 
        }

        StartCoroutine(DeactivateAfterDeath(3.724f));
    }

    IEnumerator DeactivateAfterDeath(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }

    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * meleeAttackRange, 1.5f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rockThrowRange);
    }
}
