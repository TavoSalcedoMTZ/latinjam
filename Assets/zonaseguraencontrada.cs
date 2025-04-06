using System.Collections;
using UnityEditor.TerrainTools;
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
    public ZonaSegura zonaSegura; // Referencia a la clase ZonaSegura
    public GestorDeVariables gestorDeVariables; // Referencia a la clase GestorDeVariables

    void Start()
    {
        gestorDeVariables = FindObjectOfType<GestorDeVariables>();
        zonaSegura =GetComponent<ZonaSegura>();
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

   public void TiendaEncontrada()
    {
        if (!efectoActivo)
        {
            gestorDeVariables.TiendaEncontrada();
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
            float escala = Mathf.Lerp(1.0f, escalaMaxima, factor);
            instancia.SetFloat("_Scale", escala);
            tiempo += Time.deltaTime;
            yield return null;


        }
        zonaSegura.TiendaEncontrada = true; // Cambia el estado de la tienda a encontrada
        gestorDeVariables.numerodeTiendasEncontradas += 1; // Incrementa el contador de tiendas encontradas
    }




}
