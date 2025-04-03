using UnityEngine;
using System.Collections;

public class Ecolocalizacion : MonoBehaviour
{
    public float rangoDeteccion = 2000000f;
    public int cantidadRayos = 20;
    public float efectoDuracion = 30f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Ecolocalización activada");
            ActivarEcolocalizacion();
        }
    }

    void ActivarEcolocalizacion()
    {
        for (int i = 0; i < cantidadRayos; i++)
        {
            float angulo = (360f / cantidadRayos) * i;
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

    IEnumerator AplicarEfectoGradual(Material outlineMaterial)
    {
        float mitadTiempo = efectoDuracion / 2f;
        float tiempo = 0;

        while (tiempo < mitadTiempo)
        {
            float factor = tiempo / mitadTiempo;
            outlineMaterial.SetColor("_Color", Color.magenta * (1.0f + factor * 9.0f));
            outlineMaterial.SetFloat("_Scale", 1.0f + factor * 0.06f);
            tiempo += Time.deltaTime;
            yield return null;
        }

        while (tiempo > 0)
        {
            float factor = tiempo / mitadTiempo;
            outlineMaterial.SetColor("_Color", Color.magenta * (1.0f + factor * 9.0f));
            outlineMaterial.SetFloat("_Scale", 1.0f + factor * 0.06f);
            tiempo -= Time.deltaTime;
            yield return null;
        }

        outlineMaterial.SetColor("_Color", Color.magenta);
        outlineMaterial.SetFloat("_Scale", 1.0f);
    }
}
