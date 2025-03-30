using UnityEngine;

public class ParticulasSiguenJugador : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public ParticleSystem particleSystem;
    private ParticleSystem.Particle[] particles;
    public float followSpeed = 2.0f; // Velocidad de seguimiento de las partículas
    private bool isActive = false;
    private float timer = 0f;
    private float duration = 30f; // Duración en segundos

    private void Start()
    {
        particleSystem.Stop();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!isActive)
            {
                particleSystem.Play();
                isActive = true;
                timer = duration;
            }
        }

        if (isActive)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                particleSystem.Stop();
                isActive = false;
            }
        }
    }

    void LateUpdate()
    {
        if (player == null || particleSystem == null || !isActive)
            return;

        int particleCount = particleSystem.particleCount;
        if (particles == null || particles.Length < particleCount)
        {
            particles = new ParticleSystem.Particle[particleCount];
        }

        particleSystem.GetParticles(particles);

        for (int i = 0; i < particleCount; i++)
        {
            // Movimiento suavizado de cada partícula hacia el jugador
            particles[i].position = Vector3.Lerp(particles[i].position, player.position, Time.deltaTime * followSpeed);
        }

        particleSystem.SetParticles(particles, particleCount);
    }
}
