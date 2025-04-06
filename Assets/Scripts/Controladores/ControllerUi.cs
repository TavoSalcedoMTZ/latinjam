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

    private Queue<IEnumerator> colaDeEventos = new Queue<IEnumerator>(); // 🔁 COLA DE EVENTOS
    private bool procesandoCola = false;

   
    void Start()
    {
        inventarioabiertoporprimeraVez = false;
        canOpenInventory = false;
        objetoInRaycast = false;
        Panel.SetActive(false);

        AgregarEventoALaCola(GestorDialogos(0)); 
    }

    void Update()
    {
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

    public void ManagerObjetosInventory(string tag)
    {
        if (tag == "Copa")
        {
            contador += 1;
            textosObjetos[0].text = "Copa: " + contador + "/3";
            if (contador == 3)
            {
                gestorDeVariables.copascompletas = true;
            }
        }
        else if (tag == "Fruta")
        {
            contadorFruta += 1;
            textosObjetos[1].text = "Fruta: " + contadorFruta + "/8";

            if (contadorFruta == 8)
            {
                gestorDeVariables.frutascompletas = true;
            }
        }
        else if (tag == "Crucifico")
        {
            contadorcrucifijos += 1;
            textosObjetos[2].text = "Crucifico: " + contadorcrucifijos + "/3";
            if (contadorcrucifijos == 3)
            {
                gestorDeVariables.crucifijoscompletos = true;
            }
        }
        else if (tag == "hilo")
        {
            contadorhilo += 1;
            textosObjetos[3].text = "hilo: " + contadorhilo + "/3";

            if (contadorhilo == 3)
            {
                gestorDeVariables.hiloscompletos = true;
            }
        }
        else if (tag == "vela")
        {
            contadorvela += 1;
            textosObjetos[4].text = "vela: " + contadorvela + "/4";

            if (contadorvela == 4)
            {
                gestorDeVariables.velascompletas = true;
            }
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
    

    public void EventoMision1()
    {
        mensajeMision = "Encuentra una tienda bendita";
        AgregarEventoALaCola(EscribirTexto(mensajeMision, 0.05f, textoMision[0], 0));
    }

    public void EventoMision0()
    {
        mensajeMision = "Abre el inventario manteniendo TAB";
        canOpenInventory = true;
        AgregarEventoALaCola(EscribirTexto(mensajeMision, 0.05f, textoMision[0], 9090));
    }
    public void EventoMision2()
    {
        mensajeMision = "Encuentra la llave de la puerta";
        AgregarEventoALaCola(EscribirTexto(mensajeMision, 0.05f, textoMision[1], 1));
    }

    public  IEnumerator GestorDialogos(int dialogoIndex)
    {
        switch (dialogoIndex)
        {
            case 0:
                yield return EscribirTexto("Siento la prescencia de espiritus malignos", 0.05f, dialgosTexto, 9090);
                yield return BorrarTexto(dialgosTexto, null, 0.05f, 1f);
                yield return EscribirTexto("Debo Tener Cuidado", 0.05f, dialgosTexto, 9090);
                yield return BorrarTexto(dialgosTexto, null, 0.05f, 0.5f);
                yield return EscribirTexto("Necesito recolectar los materiales para purgar este lugar", 0.05f, dialgosTexto, 9090);
                EventoMision0();
                yield return BorrarTexto(dialgosTexto, null, 0.05f, 1f);
                break;

            case 1:
                yield return EscribirTexto("Los materiales deberia poder encontrarlos aqui", 0.05f, dialgosTexto, 9090);
                yield return BorrarTexto(dialgosTexto, null, 0.05f, 1f);
                yield return EscribirTexto("Muy bien empecemos", 0.05f, dialgosTexto, 9090);
                yield return BorrarTexto(dialgosTexto, null, 0.05f, 1f);
                yield return new WaitForSeconds(0.5f);
                gestorDeVariables.IniciaJuego();
                yield return new WaitForSeconds(1f);


                yield return EscribirTexto("Siento que la prescencia de los espiritus es hostil", 0.05f, dialgosTexto, 9090);
                yield return BorrarTexto(dialgosTexto, null, 0.01f, 1f);
                yield return EscribirTexto("Habia escuchado que en este mercado existian tiendas benditas", 0.05f, dialgosTexto, 9090);
                yield return BorrarTexto(dialgosTexto, null, 0.01f, 1f);
                yield return EscribirTexto("Ellos no me podran atacar ahi, sera mejor encontrarlas", 0.05f, dialgosTexto, 9090);
                yield return BorrarTexto(dialgosTexto, null, 0.01f, 1f);
                yield return EscribirTexto("Se que podre sentirla cuando este cerca", 0.05f, dialgosTexto, 9090);
                yield return BorrarTexto(dialgosTexto, null, 0.01f, 1f);
                yield return EscribirTexto("No tengo tiempo que perder", 0.05f, dialgosTexto, 9090);
                yield return BorrarTexto(dialgosTexto, null, 0.01f, 1f);
                EventoMision1();
                break;
            case 2:
                AgregarEventoALaCola( EscribirTexto("Encontre una, ahora la podre usar para protegerme", 0.05f, dialgosTexto, 9090));
                AgregarEventoALaCola( BorrarTexto(dialgosTexto, null, 0.01f, 1f));
                AgregarEventoALaCola(EscribirTexto("Puedo sentir que hay mas como esta, debo encontrarlas", 0.05f, dialgosTexto, 9090));
                AgregarEventoALaCola(BorrarTexto(dialgosTexto, null, 0.01f, 1f));
                break;


                //cuando encuentras una tienda cualquiera que no es la primera ni ultima
            case 3:
                AgregarEventoALaCola(EscribirTexto("Ahi va otra mas", 0.05f, dialgosTexto, 9090));
                AgregarEventoALaCola(BorrarTexto(dialgosTexto, null, 0.01f, 1f));
                break;


            //cuando encuentras la ultima tienda
            case 4:
                AgregarEventoALaCola(EscribirTexto("Me parece que es la ultima", 0.05f, dialgosTexto, 9090));
                AgregarEventoALaCola(BorrarTexto(dialgosTexto, null, 0.01f, 1f));
                break;

            //cuando encuentras la puerta pero no tienes la llave
            case 5:
                AgregarEventoALaCola(EscribirTexto("La puerta esta cerrada", 0.05f, dialgosTexto, 9090));
                AgregarEventoALaCola(BorrarTexto(dialgosTexto, null, 0.01f, 1f));
                AgregarEventoALaCola(EscribirTexto("Debo encontrar la llave seguro estara por aqui", 0.05f, dialgosTexto, 9090));
                AgregarEventoALaCola(BorrarTexto(dialgosTexto, null, 0.01f, 1f));
                EventoMision2();
                break;
            case 6:
                AgregarEventoALaCola(EscribirTexto("Sigue Cerrado", 0.05f, dialgosTexto, 9090));
                AgregarEventoALaCola(BorrarTexto(dialgosTexto, null, 0.01f, 1f));
                break;
        }
    }

    IEnumerator EscribirTexto(string mensaje, float velocidad, TextMeshProUGUI textmesh, int index)
    {
        textmesh.text = "";
        foreach (char letra in mensaje)
        {
            textmesh.text += letra;
            yield return new WaitForSeconds(velocidad);
        }

        yield return new WaitForSeconds(1f);

        if (index != 9090)
        {
            contadoresMisiones[index].SetActive(true);
        }
    }

    IEnumerator BorrarTexto(TextMeshProUGUI texto1, TextMeshProUGUI texto2, float velocidad, float tiempoantesdeborrar)
    {
        if (!BorrarTextoUso)
        {
            BorrarTextoUso = true;

            yield return new WaitForSeconds(tiempoantesdeborrar);

            while (texto1.text.Length > 0)
            {
                texto1.text = texto1.text.Substring(0, texto1.text.Length - 1);
                yield return new WaitForSeconds(velocidad);
            }

            if (texto2 != null)
            {
                while (texto2.text.Length > 0)
                {
                    texto2.text = texto2.text.Substring(0, texto2.text.Length - 1);
                    yield return new WaitForSeconds(velocidad);
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
}
