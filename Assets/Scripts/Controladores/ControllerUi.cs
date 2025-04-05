using TMPro;
using UnityEngine;

public class ControllerUi : MonoBehaviour
{
    public TextMeshProUGUI textoDeInteraccion;
    public bool objetoInRaycast = false;
    public GameObject Panel;
    public TextMeshProUGUI[] textosObjetos;
    public GestorDeVariables gestorDeVariables;
    int contador = 0;
    int contadorFruta = 0;
    void Start()
    {
        objetoInRaycast = false;
        Panel.SetActive(false);
     
    }


    void Update()
    {

        if(objetoInRaycast)

        {
           textoDeInteraccion.text = "Presiona E ";
        }
        else if (gestorDeVariables.stateZonaSegura)
        {
           textoDeInteraccion.text = "";

        }
        else
        {
            textoDeInteraccion.text = "";
        }


            ShowPanelInventory();
    }

    public void ShowPanelInventory()
    {
        if (Input.GetKey(KeyCode.Tab))
        {


            Panel.SetActive(true);
        }
        else
        {
            Panel.SetActive(false);
        }
    }

    public void ManagerObjetosInventory(string tag)
    {
        if (tag == "Cubo")
        {
            Debug.Log("Marcador");
           contador += 1;
            textosObjetos[0].text = "Cubo: " + contador;    

        }
        else if (tag=="Fruta")
        {
            contadorFruta += 1;
            textosObjetos[1].text = "Fruta: " + contadorFruta;
        }

    }

}
