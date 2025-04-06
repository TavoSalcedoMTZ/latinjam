using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GestorDeVariables : MonoBehaviour
{
    public bool stateZonaSegura;
    public GameObject playermodel;
    private RotationCamara rotationCamara;
    public int numerodeTiendasEncontradas;
    public bool iniciojuego;
    public bool yaempezo;
    public bool primerazonaSegura = false;
    private ControllerUi controllerUi;
    public bool llave1;
    public bool llave2;
    public int llaves = 0;
    public int llavesencontradas = 0;
    public Cambiarnivel cambiarnivel;

    public bool frutascompletas = false;
    public bool copascompletas = false;
    public bool crucifijoscompletos = false;
    public bool hiloscompletos = false;
    public bool velascompletas = false;
    public bool altaractivo = false;
    bool yapasowin=false;

    public bool PlayerMuerto = false;
    public bool eventopasado=false;

    public GameObject screemer;
    public GameObject ApagarCanvas;
    public GameObject[] enemigos;

    public bool ObjetosCompletos = false;
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

        if (!yapasowin)
        {
            WinCondition();
        }

        if (PlayerMuerto)
        {
            stateZonaSegura = true;
            screemer.SetActive(true);
            ApagarCanvas.SetActive(false);
            enemigos[0].SetActive(false);
            enemigos[1].SetActive(false);
            enemigos[2].SetActive(false);

            cambiarnivel.ActivarMuerte();


        }

        if (ObjetosCompletos&&!eventopasado)
        {
            controllerUi.AgregarEventoALaCola(controllerUi.GestorDialogos(11));
            eventopasado = true;
        }

       
    }
    public void WinCondition()
    {
        if (frutascompletas && copascompletas && crucifijoscompletos && hiloscompletos && velascompletas)
        {
            ObjetosCompletos = true;
            Debug.Log("Has completado el altar");

            altaractivo = true;
            yapasowin = true;


        }
    }


    public void LLaveIncorrecta(){
     controllerUi.AgregarEventoALaCola(controllerUi.GestorDialogos(7));
    }
    public void NoTengoLlave()
    {
        controllerUi.AgregarEventoALaCola(controllerUi.GestorDialogos(5));
    }
    public void NoTengoLlave2()
    {
        controllerUi.AgregarEventoALaCola(controllerUi.GestorDialogos(6));
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
        cambiarnivel = FindObjectOfType<Cambiarnivel>();
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
        if (numerodeTiendasEncontradas < 5 && numerodeTiendasEncontradas > 1)
        {
            controllerUi.AgregarEventoALaCola(controllerUi.GestorDialogos(3));
        }
        else if (numerodeTiendasEncontradas == 5)
        {
            controllerUi.AgregarEventoALaCola(controllerUi.GestorDialogos(4));
        }
        {
           
        }
    }

    public void IniciaJuego()
    {
        iniciojuego = false;
       


    }

    public void EncontrarLlave(int index)
    {
        switch (index)
        {
            case 1:
                llave1 = true;

                llavesencontradas++;
                controllerUi.AgregarEventoALaCola(controllerUi.GestorDialogos(8));
                break;

            case 2:
                llave2 = true;
                llavesencontradas++;
                controllerUi.AgregarEventoALaCola(controllerUi.GestorDialogos(8));
                break;
        }

    }
}
