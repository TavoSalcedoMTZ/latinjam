using UnityEngine;

public class ZonaSegura : MonoBehaviour
{
    public Transform posicionEscondite; // Posici�n a la que se mover� la c�mara
    public float velocidadMovimiento = 2f;

    private bool moverCamara = false;
    public Transform camaraJugador; // Asigna aqu� la c�mara del jugador

    public void EsconderseActivar()
    {
        moverCamara = true;
    }

    private void Update()
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
    }
}
