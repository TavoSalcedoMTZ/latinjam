using UnityEngine;
using UnityEngine.UI;

public class Ecolocalizacion : MonoBehaviour
{
    public float rangoDeteccion = 2000000f;
    public int cantidadRayosActivo = 30;
    public int cantidadRayosPasivo = 18;
    public float intervaloEcolocalizacion = 0.5f;
    public float tiempoRecarga = 15f;

    private float tiempoUltimaEcolocalizacion = 0f;
    private float tiempoUltimaActiva = -Mathf.Infinity;

    public Image fillerRecarga; // UI Image con tipo Fill

    void Update()
    {
        // Manejamos la recarga del filler visual
        if (fillerRecarga != null)
        {
            float progreso = Mathf.Clamp01((Time.time - tiempoUltimaActiva) / tiempoRecarga);
            fillerRecarga.fillAmount = progreso;
        }

        // Activación manual con Q
        if (Input.GetKeyDown(KeyCode.Q) && Time.time >= tiempoUltimaActiva + tiempoRecarga)
        {
            Debug.Log("Ecolocalización activa");
            EmitirRayos(cantidadRayosActivo, rangoDeteccion);
            tiempoUltimaActiva = Time.time;
        }

        // Ecolocalización pasiva mientras se mueve
        if ((Input.GetButton("Horizontal") || Input.GetButton("Vertical")) &&
            Time.time >= tiempoUltimaEcolocalizacion + intervaloEcolocalizacion)
        {
            EmitirRayos(cantidadRayosPasivo, 10f);
            tiempoUltimaEcolocalizacion = Time.time;
        }
    }

    void EmitirRayos(int cantidad, float rango)
    {
        for (int i = 0; i < cantidad; i++)
        {
            float angulo = (360f / cantidad) * i;
            Vector3 direccion = Quaternion.Euler(0, angulo, 0) * Vector3.forward;

            if (Physics.Raycast(transform.position, direccion, out RaycastHit hit, rango))
            {
                Debug.DrawLine(transform.position, hit.point, Color.red, 1f);
                EfectoEcolocalizacion receptor = hit.collider.GetComponent<EfectoEcolocalizacion>();
                if (receptor != null)
                {
                    receptor.ActivarEfecto();
                }
            }
            else
            {
                Debug.DrawLine(transform.position, transform.position + direccion * rango, Color.blue, 1f);
            }
        }
    }
}
