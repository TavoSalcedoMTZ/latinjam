using UnityEngine;

public class ParticulasSiguenJugador : MonoBehaviour
{
    public ParticleSystem sistemaParticulas; // Asigna el sistema de partículas en el Inspector
    public Transform jugador; // Asigna el jugador al que las partículas deben seguir
    public float fuerzaCurva = 3f; // Cuánta curva tendrán las partículas
    public float velocidadParticulas = 5f; // Velocidad a la que se moverán las partículas

    private ParticleSystem.Particle[] particulas;

    void LateUpdate()
    {
        if (sistemaParticulas == null || jugador == null)
            return;

        int numParticulas = sistemaParticulas.particleCount;
        if (particulas == null || particulas.Length < numParticulas)
            particulas = new ParticleSystem.Particle[numParticulas];

        sistemaParticulas.GetParticles(particulas);

        for (int i = 0; i < numParticulas; i++)
        {
            // Dirección recta hacia el jugador
            Vector3 direccion = (jugador.position - particulas[i].position).normalized;

            // Crear una desviación en la trayectoria con seno
            Vector3 curva = Vector3.Cross(direccion, Vector3.up) * Mathf.Sin(Time.time * fuerzaCurva);

            // Aplicar movimiento con curvatura
            particulas[i].velocity = (direccion + curva) * velocidadParticulas;
        }

        sistemaParticulas.SetParticles(particulas, numParticulas);
    }
}
