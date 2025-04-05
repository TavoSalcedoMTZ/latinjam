using UnityEngine;

public class Inventario : MonoBehaviour
{
    public ControllerUi controllerUi;

    public void AddItem(GameObject gameob)
    {
        string etiqueta = gameob.tag;
        Debug.Log("Etiqueta del objeto: " + etiqueta);
        Debug.Log("Objeto: " + gameob.name + " - Tag: " + gameob.tag);


        controllerUi.ManagerObjetosInventory(etiqueta);

        Destroy(gameob);
    }
}
