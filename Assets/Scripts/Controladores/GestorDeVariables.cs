        using UnityEngine;

        public class GestorDeVariables : MonoBehaviour
        {
            [Header("Player y Controladores")]
            public GameObject playermodel;
            private RotationCamara rotationCamara;
            private ControllerUi controllerUi;
            public Cambiarnivel cambiarnivel;

            [Header("Zonas y Juego")]
            public bool stateZonaSegura;
            public bool iniciojuego;
            public bool yaempezo;
            public bool primerazonaSegura = false;
            public GameObject textoSaltarDialogo;

  
            public static bool PrimeraVezYaPasada = false;

            [Header("Condiciones del Juego")]
            public bool frutascompletas = false;
            public bool copascompletas = false;
            public bool crucifijoscompletos = false;
            public bool hiloscompletos = false;
            public bool velascompletas = false;
            public bool altaractivo = false;
            public bool ObjetosCompletos = false;
            private bool yapasowin = false;

            [Header("Llaves")]
            public bool llave1;
            public bool llave2;
            public int llaves = 0;
            public int llavesencontradas = 0;

            [Header("Enemigos y Eventos")]
            public bool PlayerMuerto = false;
            public bool eventopasado = false;
            public GameObject screemer;
            public GameObject ApagarCanvas;
            public GameObject[] enemigos;

            [Header("Tiendas")]
            public int numerodeTiendasEncontradas;

            private void Start()
            {
                rotationCamara = FindObjectOfType<RotationCamara>();
                controllerUi = FindObjectOfType<ControllerUi>();
                cambiarnivel = FindObjectOfType<Cambiarnivel>();

      
                yaempezo = false;
                iniciojuego = true;
                numerodeTiendasEncontradas = 0;
                eventopasado = false;
            }

            private void Update()
            {
                ManejarInicio();
                VerificarPrimeraZona();
                VerificarWinCondition();
                VerificarMuerte();

                if (ObjetosCompletos && !eventopasado)
                {
            Debug.Log("Intentando ejecutar el diálogo 11...");
            controllerUi.EjecutarDialogoConPrioridad(11);
            eventopasado = true;
                }
            }

            private void ManejarInicio()
            {
                if (!PrimeraVezYaPasada)
                {
                    if (!yaempezo)
                    {
                        yaempezojuego();      
                    }
                }
                else
                {
                    iniciojuego = false;
                    yaempezo = true;
                    textoSaltarDialogo.SetActive(false);
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
                    PrimeraVezYaPasada = true;
                }
            }

            private void VerificarPrimeraZona()
            {
                if (!primerazonaSegura && numerodeTiendasEncontradas == 1)
                {
                    controllerUi.AgregarEventoALaCola(controllerUi.GestorDialogos(2));
                    primerazonaSegura = true;
                }
            }

            private void VerificarWinCondition()
            {
                if (!yapasowin && frutascompletas && copascompletas && crucifijoscompletos && hiloscompletos && velascompletas)
                {
                    ObjetosCompletos = true;
                    Debug.Log("Has completado el altar");
                    altaractivo = true;
                    yapasowin = true;
                }
            }

            private void VerificarMuerte()
            {
                if (PlayerMuerto)
                {
                    stateZonaSegura = true;
                    screemer.SetActive(true);
                    ApagarCanvas.SetActive(false);
                    foreach (var enemigo in enemigos)
                    {
                        enemigo.SetActive(false);
                    }

                    cambiarnivel.ActivarMuerte();
                }
            }



 

            public void LLaveIncorrecta()
            {
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
                        break;
                    case 2:
                        llave2 = true;
                        break;
                }

                llavesencontradas++;
                controllerUi.AgregarEventoALaCola(controllerUi.GestorDialogos(8));
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

            public void TiendaEncontrada()
            {
                if (numerodeTiendasEncontradas > 1 && numerodeTiendasEncontradas < 5)
                {
                    controllerUi.AgregarEventoALaCola(controllerUi.GestorDialogos(3));
                }
                else if (numerodeTiendasEncontradas == 5)
                {
                    controllerUi.AgregarEventoALaCola(controllerUi.GestorDialogos(4));
                }
            }
        }
