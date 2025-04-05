
using UnityEngine;
using System.Collections;

public class EfectoEcolocalizacion : MonoBehaviour
{
    public float duracion = 5f;
    private bool efectoActivo = false;

    private Renderer rend;
    private Material original;
    private Material instancia;
    public int materialIndex = 1;

    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        if (rend != null && rend.materials.Length > materialIndex)
        {
            original = rend.materials[materialIndex];
            instancia = new Material(original);
            instancia.name = original.name + "_Instance";

            Material[] mats = rend.materials;
            mats[materialIndex] = instancia;
            rend.materials = mats;
        }
    }

    public void ActivarEfecto()
    {
        if (!efectoActivo)
        {
            StartCoroutine(EfectoVisual());
        }
      
    }

    IEnumerator EfectoVisual()
    {
        efectoActivo = true;

        float mitadTiempo = duracion / 2f;
        float tiempo = 0f;

        // Fase de encendido
        while (tiempo < mitadTiempo)
        {
            float factor = tiempo / mitadTiempo;
            float escala = Mathf.Lerp(1.0f, 1.06f, factor);
            instancia.SetColor("_Color", Color.white * (1.0f + factor * 9.0f));
            instancia.SetFloat("_Scale", escala);

            tiempo += Time.deltaTime;
            yield return null;
        }

        // Fase de apagado
        while (tiempo > 0)
        {
            float factor = tiempo / mitadTiempo;
            float escala = Mathf.Lerp(1.0f, 1.06f, factor);
            instancia.SetColor("_Color", Color.white * (1.0f + factor * 9.0f));
            instancia.SetFloat("_Scale", escala);

            tiempo -= Time.deltaTime;
            yield return null;
        }

        // Restaurar estado visual
        instancia.SetColor("_Color", Color.white);
        instancia.SetFloat("_Scale", 1.0f);

        efectoActivo = false;
    }
}



/*
 * Aqui hay una variante de la funcion que se va reiniciando la corrutina conforme va caminando, por si es necesario la guardare aqui
 * using UnityEngine;
using System.Collections;

public class EfectoEcolocalizacion : MonoBehaviour
{
    public float duracion = 5f;
    private Coroutine efectoCoroutine;

    private Renderer rend;
    private Material original;
    private Material instancia;
    public int materialIndex = 1;

    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        if (rend != null && rend.materials.Length > materialIndex)
        {
            original = rend.materials[materialIndex];
            instancia = new Material(original);
            instancia.name = original.name + "_Instance";

            Material[] mats = rend.materials;
            mats[materialIndex] = instancia;
            rend.materials = mats;
        }
    }

    public void ActivarEfecto()
    {
        // Si ya hay una corrutina en ejecución, la detenemos
        if (efectoCoroutine != null)
        {
            StopCoroutine(efectoCoroutine);
        }

        // Reiniciamos el efecto
        efectoCoroutine = StartCoroutine(EfectoVisual());
    }

    IEnumerator EfectoVisual()
    {
        float mitadTiempo = duracion / 2f;
        float tiempo = 0f;

        // Fase de encendido (brillo y expansión)
        while (tiempo < mitadTiempo)
        {
            float factor = tiempo / mitadTiempo;
            float escala = Mathf.Lerp(1.0f, 1.06f, factor);
            instancia.SetColor("_Color", Color.white * (1.0f + factor * 9.0f));
            instancia.SetFloat("_Scale", escala);

            tiempo += Time.deltaTime;
            yield return null;
        }

        // Fase de apagado (volver a la normalidad)
        while (tiempo > 0)
        {
            float factor = tiempo / mitadTiempo;
            float escala = Mathf.Lerp(1.0f, 1.06f, factor);
            instancia.SetColor("_Color", Color.white * (1.0f + factor * 9.0f));
            instancia.SetFloat("_Scale", escala);

            tiempo -= Time.deltaTime;
            yield return null;
        }

        // Restaurar los valores originales del material instancia
        instancia.SetColor("_Color", Color.white);
        instancia.SetFloat("_Scale", 1.0f);

        efectoCoroutine = null;
    }
}
*/