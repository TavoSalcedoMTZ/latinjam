using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float detectionRadius = 10f;
    public LayerMask playerLayer;
    public Transform player;

    private NavMeshAgent agent;
    private int currentPatrolIndex;
    private bool chasingPlayer = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GoToNextPatrolPoint();
    }

    bool wasChasingPlayer = false;

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRadius && CanSeePlayer())
        {
            if (!chasingPlayer)
            {
                chasingPlayer = true;
                wasChasingPlayer = true;
            }
        }
        else if (distanceToPlayer > detectionRadius * 1.5f && chasingPlayer)
        {
            chasingPlayer = false;

            // Solo llamar una vez cuando deja de perseguir
            if (wasChasingPlayer)
            {
                GoToNextPatrolPoint();
                wasChasingPlayer = false;
            }
        }

        if (chasingPlayer)
        {
            agent.SetDestination(player.position);
        }
        else if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPatrolPoint();
        }
    }

    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;

        agent.destination = patrolPoints[currentPatrolIndex].position;
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    bool CanSeePlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        if (Physics.Raycast(transform.position + Vector3.up, directionToPlayer, out RaycastHit hit, detectionRadius, ~0))
        {
            return hit.transform.CompareTag("Player");
        }
        return false;
    }
}
