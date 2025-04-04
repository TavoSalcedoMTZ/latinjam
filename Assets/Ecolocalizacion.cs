using UnityEngine;
using System.Collections;

public class Ecolocalizacion : MonoBehaviour
{
    public float rangoDeteccion = 2000000f;
    public int cantidadRayosActivo = 20;
    public int cantidadRayosPasivo = 10;
    public float efectoDuracion = 30f;
    public float intervaloEcolocalizacion = 0.0000001f; 
    private float tiempoUltimaEcolocalizacion = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Ecolocalización activada");
            ActivarEcolocalizacion();
        }

        if ((Input.GetButton("Horizontal") || Input.GetButton("Vertical")) && Time.time >= tiempoUltimaEcolocalizacion + intervaloEcolocalizacion)
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
                Debug.DrawLine(transform.position, hit.point, Color.red, 1f);
                Debug.Log($"Impacto en: {hit.collider.name} a {hit.point}");

                Renderer renderer = hit.collider.GetComponent<MeshRenderer>();
                if (renderer != null && renderer.materials.Length > 1)
                {
                    StartCoroutine(AplicarEfectoGradual(renderer.materials[1]));
                }
                else
                {
                    Debug.LogWarning($"El objeto {hit.collider.name} no tiene suficientes materiales.");
                }
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
                Debug.DrawLine(transform.position, hit.point, Color.red, 1f);
                Debug.Log($"Impacto en: {hit.collider.name} a {hit.point}");

                Renderer renderer = hit.collider.GetComponent<MeshRenderer>();
                if (renderer != null && renderer.materials.Length > 1)
                {
                    StartCoroutine(AplicarEfectoGradual(renderer.materials[1]));
                }
                else
                {
                    Debug.LogWarning($"El objeto {hit.collider.name} no tiene suficientes materiales.");
                }
            }
            else
            {
                Debug.DrawLine(transform.position, transform.position + direccion * 10f, Color.blue, 1f);
            }
        }
    }

    IEnumerator AplicarEfectoGradual(Material outlineMaterial)
    {
        float mitadTiempo = efectoDuracion / 2f;
        float tiempo = 0;

        while (tiempo < mitadTiempo)
        {
            float factor = tiempo / mitadTiempo;
            outlineMaterial.SetColor("_Color", Color.white * (1.0f + factor * 9.0f));
            outlineMaterial.SetFloat("_Scale", 1.0f + factor * 0.02f);
            tiempo += Time.deltaTime;
            yield return null;
        }

        while (tiempo > 0)
        {
            float factor = tiempo / mitadTiempo;
            outlineMaterial.SetColor("_Color", Color.white * (1.0f + factor * 9.0f));
            outlineMaterial.SetFloat("_Scale", 1.0f + factor * 0.02f);
            tiempo -= Time.deltaTime;
            yield return null;
        }

        outlineMaterial.SetColor("_Color", Color.white);
        outlineMaterial.SetFloat("_Scale", 1.0f);
    }
}
