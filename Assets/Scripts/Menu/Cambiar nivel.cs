using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Cambiarnivel : MonoBehaviour
{
    public void CambiarEscena(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }
 
}
