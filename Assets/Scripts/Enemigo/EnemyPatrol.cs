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
    private bool wasChasingPlayer = false;

    // Collider para detectar al jugador en el área
    private SphereCollider detectionCollider;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        detectionCollider = gameObject.AddComponent<SphereCollider>();
        detectionCollider.radius = detectionRadius;  // Configura el radio del área de detección
        detectionCollider.isTrigger = true;  // Hace que el collider sea un trigger
        GoToNextPatrolPoint();
    }

    void Update()
    {
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

    // Detecta al jugador cuando entra en el área de detección
    void OnTriggerEnter(Collider other)
    {
        if ((playerLayer.value & (1 << other.gameObject.layer)) > 0) // Verifica si el objeto es el jugador
        {
            chasingPlayer = true;
            wasChasingPlayer = true;
        }
    }

    // Deja de perseguir al jugador cuando sale del área
    void OnTriggerExit(Collider other)
    {
        if ((playerLayer.value & (1 << other.gameObject.layer)) > 0) // Verifica si el objeto es el jugador
        {
            chasingPlayer = false;
            if (wasChasingPlayer)
            {
                GoToNextPatrolPoint();
                wasChasingPlayer = false;
            }
        }
    }

    // Dibuja el radio de detección en la escena
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
