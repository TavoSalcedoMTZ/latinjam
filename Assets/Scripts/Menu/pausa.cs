using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pausa : MonoBehaviour
{
    public GameObject ObjetoManuPausa;
    public bool Pausa = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Pausa == false)
            {
                ObjetoManuPausa.SetActive(true);
                Pausa = true;

                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    public void Resumir()
    {
        ObjetoManuPausa.SetActive(false);
        Pausa = false;

        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
