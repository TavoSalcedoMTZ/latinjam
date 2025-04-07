using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float detectionRadius = 10f;
    public LayerMask playerLayer;
    public Transform player;

    public bool allowChase = true; // Puedes desactivar esto desde el Inspector para que no persigan
    public float chaseTime = 5f; // Tiempo máximo de persecución
    public float maxChaseDistanceMultiplier = 1.5f; // Si el jugador se aleja más del radio * esto, dejan de perseguir

    private NavMeshAgent agent;
    private int currentPatrolIndex;
    private bool chasingPlayer = false;
    private float chaseTimer = 0f;
    public GestorDeVariables gestorDeVariables; // Referencia al gestor de variables

    private SphereCollider detectionCollider;

    void Start()
    {
        gestorDeVariables = FindObjectOfType<GestorDeVariables>();
        agent = GetComponent<NavMeshAgent>();
        detectionCollider = gameObject.AddComponent<SphereCollider>();
        detectionCollider.radius = detectionRadius;
        detectionCollider.isTrigger = true;
        GoToNextPatrolPoint();
    }

    void Update()
    {
        if (chasingPlayer && allowChase)
        {
            agent.SetDestination(player.position);
            chaseTimer += Time.deltaTime;

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (chaseTimer >= chaseTime || distanceToPlayer > detectionRadius * maxChaseDistanceMultiplier)
            {
                // Se detiene la persecución y se reanuda la patrulla
                chasingPlayer = false;
                chaseTimer = 0f;
                GoToNextPatrolPoint();
            }
        }
        else if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPatrolPoint();
        }
        IniciarJuego();

        if (!gestorDeVariables.iniciojuego){
            ZonaSegura();
        }
    }

    void IniciarJuego()
    {
        if (gestorDeVariables.iniciojuego)
        {
            agent.isStopped = true;
            chasingPlayer = false;
            chaseTimer = 0f;
        }
        else
        {
            agent.isStopped = false;
        }
    }

    void ZonaSegura()
    {
        if (gestorDeVariables.stateZonaSegura)
        {
            if (chasingPlayer) 
            {
                chasingPlayer = false;
                chaseTimer = 0f;
                GoToNextPatrolPoint();
            }

            agent.isStopped = false;
        }
    }
    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;

        agent.ResetPath();
        agent.destination = patrolPoints[currentPatrolIndex].position;
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!allowChase) return;

        if ((playerLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            chasingPlayer = true;
            chaseTimer = 0f;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if ((playerLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            // No necesariamente deja de perseguir aquí: puede seguir hasta que expire el tiempo o se aleje
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
