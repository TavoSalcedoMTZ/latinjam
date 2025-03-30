using TMPro;
using UnityEngine;

public class ControllerUi : MonoBehaviour
{
    public TextMeshProUGUI textoDeInteraccion;
    public bool objetoInRaycast = false;
    void Start()
    {
        objetoInRaycast = false;
    }


    void Update()
    {

        if(objetoInRaycast)

        {
           textoDeInteraccion.text = "Presiona E ";
        }
        else
        {
           textoDeInteraccion.text = "";

        }

    }
}
