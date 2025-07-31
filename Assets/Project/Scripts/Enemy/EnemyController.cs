using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform patrolPointA; 
    public Transform patrolPointB; 
    private float waitTimeAtPoint = 0.1f;

    private Transform currentTarget;
    private NavMeshAgent agent;
    private GameObject player;
    private Animator animator;
    private EnemyState currentState;
    private bool isWaiting = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        currentTarget = patrolPointA;
        ChangeState(EnemyState.PATROL);
    }

    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);

        if (currentState == EnemyState.PATROL)
        {
            Patrol();
        }

        // Detectar jugador con SphereCast
        Ray ray = new Ray(transform.position + Vector3.up, player.transform.position - transform.position);
        if (Physics.SphereCast(ray, 2f, out RaycastHit hit, 10f))
        {
            if (hit.transform.CompareTag("Player"))
            {
                ChangeState(EnemyState.CHASE);
            }
        }

        if (currentState == EnemyState.CHASE)
        {
            ChasePlayer();
        }
    }

    void Patrol()
    {
        if (!isWaiting)
        {
            agent.SetDestination(currentTarget.position);

            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                StartCoroutine(WaitAndSwitchPoint());
            }
        }
    }

    IEnumerator WaitAndSwitchPoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTimeAtPoint);

        currentTarget = (currentTarget == patrolPointA) ? patrolPointB : patrolPointA;
        isWaiting = false;
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);
    }

    public void ChangeState(EnemyState newState)
    {
        currentState = newState;
        if (newState == EnemyState.CHASE)
        {
            StopAllCoroutines();
            isWaiting = false;
        }
    }
}

public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK
}
