using UnityEngine;

public class ZonaSegura : MonoBehaviour
{
    public Transform posicionEscondite;
    public float velocidadMovimiento = 2f;
    public Transform camaraJugador;

    private bool moverCamara = false;
    private bool escondido = false;
    public bool TiendaEncontrada = false; // Variable para verificar si la tienda ha sido encontrada

    private Vector3 posicionOriginal;
    private Quaternion rotacionOriginal;

    private GestorDeVariables gestorDeVariables;

    private void Start()
    {
        escondido = false;
        gestorDeVariables = FindObjectOfType<GestorDeVariables>();
    }

    public void GuardarPosicionOriginal()
    {
        posicionOriginal = camaraJugador.position;
        rotacionOriginal = camaraJugador.rotation;
    }

    public void EsconderseActivar()
    {
        if (TiendaEncontrada)
        {
            GuardarPosicionOriginal();
            gestorDeVariables.ActivarEsconderse();
            moverCamara = true;
            escondido = true;
        }
        else { return; }
    }

       

    private void Update()
    {
        if (TiendaEncontrada)
        {
            if (moverCamara && camaraJugador != null)
            {
                camaraJugador.position = Vector3.Lerp(camaraJugador.position, posicionEscondite.position, Time.deltaTime * velocidadMovimiento);
                camaraJugador.rotation = Quaternion.Lerp(camaraJugador.rotation, posicionEscondite.rotation, Time.deltaTime * velocidadMovimiento);

                if (Vector3.Distance(camaraJugador.position, posicionEscondite.position) < 0.05f)
                {
                    moverCamara = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.E) && escondido)
            {

                StartCoroutine(SalirEscondite());
            }

        }
        else {  }
    }

    private System.Collections.IEnumerator SalirEscondite()
    {
        float t = 0f;
        Vector3 posicionInicio = camaraJugador.position;
        Quaternion rotacionInicio = camaraJugador.rotation;

        while (t < 1f)
        {
            t += Time.deltaTime * velocidadMovimiento;
            camaraJugador.position = Vector3.Lerp(posicionInicio, posicionOriginal, t);
            camaraJugador.rotation = Quaternion.Lerp(rotacionInicio, rotacionOriginal, t);
            yield return null;
        }

        gestorDeVariables.DesactivarEsconderse();
        moverCamara = false;
        escondido = false;
    }
}
