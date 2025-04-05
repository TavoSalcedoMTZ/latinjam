using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pausa1 : MonoBehaviour
{
    public GameObject ObjetoManuPausa;
    public bool Pausa = false;

    public void Resumir()
    {
        ObjetoManuPausa.SetActive(false);
        Pausa = false;

        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Puedes agregar una función pública para pausar desde un botón:
    public void Pausar()
    {
        if (!Pausa)
        {
            ObjetoManuPausa.SetActive(true);
            Pausa = true;

            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}

