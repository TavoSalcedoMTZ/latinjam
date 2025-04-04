using UnityEngine;
using System.Collections;

public class Ecolocalizacion : MonoBehaviour
{
    public float rangoDeteccion = 2000000f;
    public int cantidadRayosActivo = 20;
    public int cantidadRayosPasivo = 10;
    public float efectoDuracion = 5f;
    public float intervaloEcolocalizacion = 0.0000001f;
    private float tiempoUltimaEcolocalizacion = 0f;

    private bool efectoEnCurso = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Ecolocalización activada");
            ActivarEcolocalizacion();
        }

        if ((Input.GetButton("Horizontal") || Input.GetButton("Vertical")) &&
            Time.time >= tiempoUltimaEcolocalizacion + intervaloEcolocalizacion)
        {
            EcolocalizacionPasiva();
            tiempoUltimaEcolocalizacion = Time.time;
        }
    }

    void ActivarEcolocalizacion()
    {
        for (int i = 0; i < cantidadRayosActivo; i++)
        {
            float angulo = (360f / cantidadRayosActivo) * i;
            Vector3 direccion = Quaternion.Euler(0, angulo, 0) * Vector3.forward;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, direccion, out hit, rangoDeteccion))
            {
                ProcesarImpacto(hit);
            }
            else
            {
                Debug.DrawLine(transform.position, transform.position + direccion * rangoDeteccion, Color.blue, 1f);
            }
        }
    }

    void EcolocalizacionPasiva()
    {
        Debug.Log("Ecolocalización pasiva activada");
        for (int i = 0; i < cantidadRayosPasivo; i++)
        {
            float angulo = (360f / cantidadRayosPasivo) * i;
            Vector3 direccion = Quaternion.Euler(0, angulo, 0) * Vector3.forward;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, direccion, out hit, 10f))
            {
                ProcesarImpacto(hit);
            }
            else
            {
                Debug.DrawLine(transform.position, transform.position + direccion * 10f, Color.blue, 1f);
            }
        }
    }

    void ProcesarImpacto(RaycastHit hit)
    {
        if (efectoEnCurso) return; // Ignora si ya hay un efecto activo

        Debug.DrawLine(transform.position, hit.point, Color.red, 1f);
        Debug.Log($"Impacto en: {hit.collider.name} a {hit.point}");

        Renderer renderer = hit.collider.GetComponent<MeshRenderer>();
        if (renderer != null && renderer.materials.Length > 1)
        {
            Material[] materiales = renderer.materials;
            Material originalMaterial = materiales[1]; // Guardar material original
            Material instancia = new Material(originalMaterial);
            instancia.name = originalMaterial.name + "_Instance";

            materiales[1] = instancia;
            renderer.materials = materiales;

            StartCoroutine(AplicarEfectoGradual(instancia, renderer, originalMaterial, 1));
        }
        else
        {
            Debug.LogWarning($"El objeto {hit.collider.name} no tiene suficientes materiales.");
        }
    }

    IEnumerator AplicarEfectoGradual(Material outlineMaterial, Renderer renderer, Material originalMaterial, int materialIndex)
    {
        efectoEnCurso = true; // Bloquear nuevas ejecuciones

        float mitadTiempo = efectoDuracion / 2f;
        float tiempo = 0;

        while (tiempo < mitadTiempo)
        {
            float factor = tiempo / mitadTiempo;
            float escala = Mathf.Lerp(1.0f, 1.0006f, factor);
            outlineMaterial.SetColor("_Color", Color.white * (1.0f + factor * 9.0f));
            outlineMaterial.SetFloat("_Scale", escala);
            tiempo += Time.deltaTime;
            yield return null;
        }

        while (tiempo > 0)
        {
            float factor = tiempo / mitadTiempo;
            float escala = Mathf.Lerp(1.0f, 1.0006f, factor);
            outlineMaterial.SetColor("_Color", Color.white * (1.0f + factor * 9.0f));
            outlineMaterial.SetFloat("_Scale", escala);
            tiempo -= Time.deltaTime;
            yield return null;
        }

        // Restaurar material original
        Material[] materiales = renderer.materials;
        materiales[materialIndex] = originalMaterial;
        renderer.materials = materiales;

        efectoEnCurso = false; // Permitir nuevos efectos
    }
}
