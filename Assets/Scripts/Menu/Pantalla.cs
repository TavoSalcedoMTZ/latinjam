using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pantalla : MonoBehaviour
{
    public Toggle toggle; // Cambiado de Teggle a Toggle y de toggled a toggle

    // Start is called before the first frame update
    void Start()
    {
        if (Screen.fullScreen)
        {
            toggle.isOn = true; // Cambiado toggled a toggle
        }
        else
        {
            toggle.isOn = false; // Cambiado toggled a toggle
        }
    }

    public void ActivarPantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }
}
