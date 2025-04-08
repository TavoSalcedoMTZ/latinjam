using System.Collections;
using UnityEngine;
using static UnityEngine.UI.Image;

public class zonaseguraencontrada : MonoBehaviour
{
    public float duracion = 5f;
    private bool efectoActivo = false;

    private Renderer rend;
    private Material original;
    private Material instancia;
    public int materialIndex = 1;
    public float escalaMaxima = 1.014f;
    public float escalaParpadeo = 1.003f;
    public ZonaSegura zonaSegura; // Referencia a la clase ZonaSegura
    public GestorDeVariables gestorDeVariables; // Referencia a la clase GestorDeVariables

    private Coroutine parpadeoCoroutine;

    void Start()
    {
        gestorDeVariables = FindObjectOfType<GestorDeVariables>();
        zonaSegura = GetComponent<ZonaSegura>();
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

        // Iniciar el efecto pasivo si aún no ha sido encontrada
        if (!zonaSegura.TiendaEncontrada)
        {
            parpadeoCoroutine = StartCoroutine(EfectoPasivo());
        }
    }

    public void TiendaEncontrada()
    {
        if (!efectoActivo)
        {
            // Detener el parpadeo pasivo si está activo
            if (parpadeoCoroutine != null)
            {
                StopCoroutine(parpadeoCoroutine);
                parpadeoCoroutine = null;
            }

            gestorDeVariables.TiendaEncontrada();
            StartCoroutine(EfectoVisual());
        }
    }

    IEnumerator EfectoVisual()
    {
        efectoActivo = true;
        float mitadTiempo = duracion / 2f;
        float tiempo = 0f;

        while (tiempo < mitadTiempo)
        {
            float factor = tiempo / mitadTiempo;
            float escala = Mathf.Lerp(1.0f, escalaMaxima, factor);
            instancia.SetFloat("_Scale", escala);
            tiempo += Time.deltaTime;
            yield return null;
        }

        zonaSegura.TiendaEncontrada = true;
        gestorDeVariables.numerodeTiendasEncontradas += 1;
    }

    IEnumerator EfectoPasivo()
    {
        while (!zonaSegura.TiendaEncontrada)
        {
            float duracionParpadeo = 1.0f;
            float tiempo = 0f;

            while (tiempo < duracionParpadeo)
            {
                float factor = Mathf.Sin(tiempo * Mathf.PI / duracionParpadeo); 
                float escala = Mathf.Lerp(1.0f, 1.003f, factor);
                instancia.SetFloat("_Scale", escala);
                tiempo += Time.deltaTime;
                yield return null;
            }

        
            instancia.SetFloat("_Scale", 1.0f);

         
            yield return new WaitForSeconds(5f);
        }
    }
}
