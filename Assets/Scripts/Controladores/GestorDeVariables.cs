using Unity.VisualScripting;
using UnityEngine;

public class GestorDeVariables : MonoBehaviour
{
    public bool stateZonaSegura;
    public GameObject playermodel;
    private RotationCamara rotationCamara;
    public int numerodeTiendasEncontradas;
    public bool iniciojuego;
    public bool yaempezo;
    public bool primerazonaSegura=false;
    private ControllerUi controllerUi;


    private void Update()
    {

        if (!yaempezo)
        {
            yaempezojuego();
        }

        if (!primerazonaSegura)
        {
            PrimeraTiendaEncontrada();
        }

      

    }
    private void yaempezojuego()
    {
        if (iniciojuego)
        {
            stateZonaSegura = true;

        }
        else
        {
            stateZonaSegura = false;
            yaempezo = true;
        }
    }

    private void Start()
    {
        controllerUi = FindObjectOfType<ControllerUi>();
        yaempezo = false;

        iniciojuego = true;
        numerodeTiendasEncontradas = 0;
        rotationCamara = FindObjectOfType<RotationCamara>();
    }
    public void ActivarEsconderse()
    {
        playermodel.SetActive(false);
        stateZonaSegura = true;


    }

    public void DesactivarEsconderse()
    {
        playermodel.SetActive(true);
        stateZonaSegura = false;
        rotationCamara.DesactivarCursor();

    }

    public void PrimeraTiendaEncontrada()
    {
        if (numerodeTiendasEncontradas == 1)
        {
            
           controllerUi.AgregarEventoALaCola(controllerUi.GestorDialogos(2));
           primerazonaSegura = true;
        }

    }
    public void TiendaEncontrada()
    {
        if(numerodeTiendasEncontradas<5 && numerodeTiendasEncontradas>1)
        {
          controllerUi.AgregarEventoALaCola(controllerUi.GestorDialogos(3));
        }
        else
        {
           
        }
    }

    public void IniciaJuego()
    {
        iniciojuego = false;


    }

}
