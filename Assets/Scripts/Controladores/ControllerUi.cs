using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControllerUi : MonoBehaviour
{
    public TextMeshProUGUI textoDeInteraccion;
    public bool objetoInRaycast = false;
    public GameObject Panel;
    public TextMeshProUGUI[] textosObjetos;
    public GestorDeVariables gestorDeVariables;
    int contador = 0;
    int contadorFruta = 0;
    int contadorcrucifijos = 0;
    int contadorhilo = 0;
    int contadorvela = 0;
    public TextMeshProUGUI[] textoMision;
    public GameObject[] contadoresMisiones;
    public string mensajeMision;
    public TextMeshProUGUI dialgosTexto;
    bool inventarioabiertoporprimeraVez = false;
    bool BorrarTextoUso = false;
    bool textoMostrandose = false;
    bool canOpenInventory = false;

    private Queue<IEnumerator> colaDeEventos = new Queue<IEnumerator>();
    private bool procesandoCola = false;

    public bool dialogoOlfatear = false;
    public bool dialogoOlfatearMostrado = false;

    private bool saltarDialogo = false;
    private bool textoCompleto = false;

    void Start()
    {
        inventarioabiertoporprimeraVez = false;
        canOpenInventory = false;
        objetoInRaycast = false;
        Panel.SetActive(false);

        if (!GestorDeVariables.PrimeraVezYaPasada)
        {
            AgregarEventoALaCola(GestorDialogos(0));
            canOpenInventory=true;
        }
        else
        {
            inventarioabiertoporprimeraVez = true;
           AgregarEventoALaCola( GestorDialogos(15));  
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!saltarDialogo)
            {
                saltarDialogo = true;
            }
        }

        if (objetoInRaycast)
        {
            textoDeInteraccion.text = "Presiona E ";
        }
        else
        {
            textoDeInteraccion.text = "";
        }

        ShowPanelInventory();
        ManejoContadoresObjetos();

        if (Input.GetKey(KeyCode.F) && !dialogoOlfatear && dialogoOlfatearMostrado)
        {
            AgregarEventoALaCola(BorrarTexto(textoMision[3], null, 0.05f, 1f));
            dialogoOlfatear = true;
        }
    }

    public void ShowPanelInventory()
    {
        if (Input.GetKey(KeyCode.Tab) && canOpenInventory)
        {
            if (!inventarioabiertoporprimeraVez)
            {
                inventarioabiertoporprimeraVez = true;
                AgregarEventoALaCola(BorrarTexto(textoMision[0], null, 0.05f, 1f));
                AgregarEventoALaCola(GestorDialogos(1));
            }

            Panel.SetActive(true);
        }
        else
        {
            Panel.SetActive(false);
        }
    }

    public void ShowTextoFrutas()
    {
        AgregarEventoALaCola(GestorDialogos(12));
    }

    public void ManagerObjetosInventory(string tag)
    {
        if (tag == "Copa")
        {
            contador += 1;
            textosObjetos[0].text = contador + "/3";
            if (contador == 3) gestorDeVariables.copascompletas = true;
        }
        else if (tag == "Fruta")
        {
            contadorFruta += 1;
            textosObjetos[1].text = contadorFruta + "/8";
            if (contadorFruta == 8) gestorDeVariables.frutascompletas = true;
        }
        else if (tag == "Crucifico")
        {
            contadorcrucifijos += 1;
            textosObjetos[2].text = contadorcrucifijos + "/4";
            if (contadorcrucifijos == 4) gestorDeVariables.crucifijoscompletos = true;
        }
        else if (tag == "hilo")
        {
            contadorhilo += 1;
            textosObjetos[3].text = contadorhilo + "/3";
            if (contadorhilo == 3) gestorDeVariables.hiloscompletos = true;
        }
        else if (tag == "vela")
        {
            contadorvela += 1;
            textosObjetos[4].text = contadorvela + "/4";
            if (contadorvela == 4) gestorDeVariables.velascompletas = true;
        }
    }

    void ManejoContadoresObjetos()
    {
        if (gestorDeVariables.numerodeTiendasEncontradas != 5)
        {
            contadoresMisiones[0].GetComponent<TextMeshProUGUI>().text = "[" + gestorDeVariables.numerodeTiendasEncontradas + "/5]";
        }
        else
        {
            AgregarEventoALaCola(GestorDialogos(4));
            AgregarEventoALaCola(BorrarTexto(contadoresMisiones[0].GetComponent<TextMeshProUGUI>(), textoMision[0], 0.05f, 0));
        }

        if (gestorDeVariables.llavesencontradas != 2)
        {
            contadoresMisiones[1].GetComponent<TextMeshProUGUI>().text = "[" + gestorDeVariables.llavesencontradas + "/" + gestorDeVariables.llaves + "]";
        }
        else
        {
            AgregarEventoALaCola(BorrarTexto(contadoresMisiones[1].GetComponent<TextMeshProUGUI>(), textoMision[1], 0.05f, 0));
        }
    }

    public void EventoMision0()
    {
        mensajeMision = "Abre el inventario manteniendo TAB";
        canOpenInventory = true;
        AgregarEventoALaCola(EscribirTexto(mensajeMision, 0.05f, textoMision[0], 9090, true));
    }

    public void EventoMision1()
    {
        mensajeMision = "Encuentra una tienda bendita";
        AgregarEventoALaCola(EscribirTexto(mensajeMision, 0.05f, textoMision[0], 0, true));
    }

    public void EventoMision2()
    {
        mensajeMision = "Encuentra la llave de la puerta";
        AgregarEventoALaCola(EscribirTexto(mensajeMision, 0.05f, textoMision[1], 1, true));
    }

    public void EventoMision3()
    {
        mensajeMision = "Ve al altar";
        AgregarEventoALaCola(EscribirTexto(mensajeMision, 0.05f, textoMision[2], 9090, true));
    }

    public void EventoMision4()
    {
        mensajeMision = "Presiona F para olfatear la fruta podrida";
        dialogoOlfatearMostrado = true;
        AgregarEventoALaCola(EscribirTexto(mensajeMision, 0.05f, textoMision[3], 9090, true));
    }

    public IEnumerator GestorDialogos(int dialogoIndex)
    {
        switch (dialogoIndex)
        {
            case 0:
                yield return EscribirTexto("Siento la prescencia de espiritus malignos", 0.05f, dialgosTexto, 9090, false);
                yield return BorrarTexto(dialgosTexto, null, 0.05f, 1f);
                yield return EscribirTexto("Debo Tener Cuidado", 0.05f, dialgosTexto, 9090, false);
                yield return BorrarTexto(dialgosTexto, null, 0.05f, 0.5f);
                yield return EscribirTexto("Necesito recolectar los materiales para purgar este lugar", 0.05f, dialgosTexto, 9090, false);
                EventoMision0();
                yield return BorrarTexto(dialgosTexto, null, 0.05f, 1f);
                break;

            case 1:
                yield return EscribirTexto("Los materiales deberia poder encontrarlos aqui", 0.05f, dialgosTexto, 9090, false);
                yield return BorrarTexto(dialgosTexto, null, 0.05f, 1f);
                yield return EscribirTexto("Muy bien empecemos", 0.05f, dialgosTexto, 9090, false);
                yield return BorrarTexto(dialgosTexto, null, 0.05f, 1f);
                yield return new WaitForSeconds(0.5f);
                gestorDeVariables.IniciaJuego();
                yield return new WaitForSeconds(1f);
                AgregarEventoALaCola(GestorDialogos(15));
                break;


            case 15:
                yield return EscribirTexto("Siento que la prescencia de los espiritus es hostil", 0.05f, dialgosTexto, 9090, false);
                yield return BorrarTexto(dialgosTexto, null, 0.01f, 1f);
                yield return EscribirTexto("Habia escuchado que en este mercado existian tiendas benditas", 0.05f, dialgosTexto, 9090, false);
                yield return BorrarTexto(dialgosTexto, null, 0.01f, 1f);
                yield return EscribirTexto("Ellos no me podran atacar ahi, sera mejor encontrarlas", 0.05f, dialgosTexto, 9090, false);
                yield return BorrarTexto(dialgosTexto, null, 0.01f, 1f);
                yield return EscribirTexto("Se que podre sentirla cuando este cerca", 0.05f, dialgosTexto, 9090, false);
                yield return BorrarTexto(dialgosTexto, null, 0.01f, 1f);
                yield return EscribirTexto("No tengo tiempo que perder", 0.05f, dialgosTexto, 9090, false);
                yield return BorrarTexto(dialgosTexto, null, 0.01f, 1f);
                EventoMision1();
                break;

            case 2:
                AgregarEventoALaCola(EscribirTexto("Encontre una, ahora la podre usar para protegerme", 0.05f, dialgosTexto, 9090, false));
                AgregarEventoALaCola(BorrarTexto(dialgosTexto, null, 0.01f, 1f));
                AgregarEventoALaCola(EscribirTexto("Puedo sentir que hay mas como esta, debo encontrarlas", 0.05f, dialgosTexto, 9090, false));
                AgregarEventoALaCola(BorrarTexto(dialgosTexto, null, 0.01f, 1f));
                break;

            case 3:
                AgregarEventoALaCola(EscribirTexto("Ahi va otra mas", 0.05f, dialgosTexto, 9090, false));
                AgregarEventoALaCola(BorrarTexto(dialgosTexto, null, 0.01f, 1f));
                break;

            case 4:
                AgregarEventoALaCola(EscribirTexto("Me parece que es la ultima", 0.05f, dialgosTexto, 9090, false));
                AgregarEventoALaCola(BorrarTexto(dialgosTexto, null, 0.01f, 1f));
                break;

            case 5:
                AgregarEventoALaCola(EscribirTexto("La puerta esta cerrada", 0.05f, dialgosTexto, 9090, false));
                AgregarEventoALaCola(BorrarTexto(dialgosTexto, null, 0.01f, 1f));
                AgregarEventoALaCola(EscribirTexto("Debo encontrar la llave seguro estara por aqui", 0.05f, dialgosTexto, 9090, false));
                AgregarEventoALaCola(BorrarTexto(dialgosTexto, null, 0.01f, 1f));
                EventoMision2();
                break;

            case 6:
                AgregarEventoALaCola(EscribirTexto("Sigue Cerrado", 0.05f, dialgosTexto, 9090, false));
                AgregarEventoALaCola(BorrarTexto(dialgosTexto, null, 0.01f, 1f));
                break;

            case 7:
                AgregarEventoALaCola(EscribirTexto("La llave no encaja", 0.05f, dialgosTexto, 9090, false));
                AgregarEventoALaCola(BorrarTexto(dialgosTexto, null, 0.01f, 1f));
                break;

            case 8:
                AgregarEventoALaCola(EscribirTexto("Encontre una llave", 0.05f, dialgosTexto, 9090, false));
                AgregarEventoALaCola(BorrarTexto(dialgosTexto, null, 0.01f, 1f));
                break;

            case 9:


            case 11:
                AgregarEventoALaCola(EscribirTexto("Ya tengo todo, ahora tengo que ir al altar", 0.05f, dialgosTexto, 9090, false));
                AgregarEventoALaCola(BorrarTexto(dialgosTexto, null, 0.01f, 1f));
                EventoMision3();
                break;

            case 12:
                AgregarEventoALaCola(EscribirTexto("Creo que puedo oler la fruta podrida", 0.05f, dialgosTexto, 9090,false));
                AgregarEventoALaCola(BorrarTexto(dialgosTexto, null, 0.01f, 1f));
                EventoMision4();
                break;
        }
    }

    IEnumerator EscribirTexto(string mensaje, float velocidad, TextMeshProUGUI textmesh, int index, bool esMision)
    {
        textmesh.text = "";
        saltarDialogo = false;
        textoCompleto = false;

        for (int i = 0; i < mensaje.Length; i++)
        {
            if (!esMision && saltarDialogo)
            {
                textmesh.text = mensaje;
                break;
            }
            textmesh.text += mensaje[i];
            yield return new WaitForSeconds(velocidad);
        }

        textoCompleto = true;

        float tiempoAntesDeAutoBorrar = 3f;
        float timer = 0f;

        while (!esMision && !saltarDialogo && timer < tiempoAntesDeAutoBorrar)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        if (index != 9090)
        {
            contadoresMisiones[index].SetActive(true);
        }

        saltarDialogo = false;
        textoCompleto = false;
    }


    IEnumerator BorrarTexto(TextMeshProUGUI texto1, TextMeshProUGUI texto2, float velocidad, float tiempoantesdeborrar)
    {
        if (!BorrarTextoUso)
        {
            BorrarTextoUso = true;

            yield return new WaitForSeconds(tiempoantesdeborrar);

            if (saltarDialogo)
            {
                texto1.text = "";
                if (texto2 != null) texto2.text = "";
            }
            else
            {
                while (texto1.text.Length > 0)
                {
                    if (saltarDialogo)
                    {
                        texto1.text = "";
                        break;
                    }
                    texto1.text = texto1.text.Substring(0, texto1.text.Length - 1);
                    yield return new WaitForSeconds(velocidad);
                }

                if (texto2 != null)
                {
                    while (texto2.text.Length > 0)
                    {
                        if (saltarDialogo)
                        {
                            texto2.text = "";
                            break;
                        }
                        texto2.text = texto2.text.Substring(0, texto2.text.Length - 1);
                        yield return new WaitForSeconds(velocidad);
                    }
                }
            }

            yield return new WaitForSeconds(1f);
            BorrarTextoUso = false;
        }
    }

    public void AgregarEventoALaCola(IEnumerator evento)
    {
        colaDeEventos.Enqueue(evento);
        if (!procesandoCola)
        {
            StartCoroutine(ProcesarCola());
        }
    }

    IEnumerator ProcesarCola()
    {
        procesandoCola = true;

        while (colaDeEventos.Count > 0)
        {
            yield return StartCoroutine(colaDeEventos.Dequeue());
        }

        procesandoCola = false;
    }

    public void EjecutarDialogoConPrioridad(int id)
    {
        Debug.Log("Ejecutando diálogo con prioridad. ID: " + id);


        colaDeEventos.Clear();


        StopAllCoroutines();
        procesandoCola = false;

  
        StartCoroutine(GestorDialogos(id));
    }

}
