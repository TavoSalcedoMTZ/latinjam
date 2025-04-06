using UnityEngine;

public class Puerta : MonoBehaviour
{
    public Animator puerta1;
    public Animator puerta2;
    private GestorDeVariables gestorDeVariables;
    public bool intentoOpen1 = false;
    public bool intentoOpen2 = false;
    public bool yaMostroMensajeSinLlave = false;

    private void Start()
    {
        gestorDeVariables = FindObjectOfType<GestorDeVariables>();
    }

    public void AbrirPuerta1()
    {
        if (gestorDeVariables.llave1)
        {
            puerta1.SetBool("isOpen", true);
        }
        else
        {
            if (gestorDeVariables.llave2)
            {
                gestorDeVariables.LLaveIncorrecta();
            }

            if (!intentoOpen1)
            {
                intentoOpen1 = true;
                if (!yaMostroMensajeSinLlave)
                {
                    yaMostroMensajeSinLlave = true;
                    Debug.Log("No tengo llave puerta1");
                    gestorDeVariables.NoTengoLlave();
                }
                else
                {
                    gestorDeVariables.NoTengoLlave2();
                }
                gestorDeVariables.llaves++;
            }
            else
            {
                gestorDeVariables.NoTengoLlave2();
            }
        }
    }

    public void AbrirPuerta2()
    {
        if (gestorDeVariables.llave2)
        {
            puerta2.SetBool("isOpen", true);
        }
        else
        {
            if (gestorDeVariables.llave1)
            {
                gestorDeVariables.LLaveIncorrecta();
            }

            if (!intentoOpen2)
            {
                intentoOpen2 = true;
                if (!yaMostroMensajeSinLlave)
                {
                    yaMostroMensajeSinLlave = true;
                    Debug.Log("No tengo llave puerta2");
                    gestorDeVariables.NoTengoLlave();
                }
                else
                {
                    gestorDeVariables.NoTengoLlave2();
                }
                gestorDeVariables.llaves++;
            }
            else
            {
                gestorDeVariables.NoTengoLlave2();
            }
        }
    }
}
