using UnityEngine;

public class altar : MonoBehaviour
{
    private GestorDeVariables gestorDeVariables;
    public InteractableObject interactableObject;

    void Start()
    {
        gestorDeVariables = FindObjectOfType<GestorDeVariables>();
        interactableObject.enabled = false; // Desactiva el script al inicio
        interactableObject.activar = false; // Desactiva la variable activar al inicio
    }

    void Update()
    {
        if (gestorDeVariables.altaractivo)
        {
            interactableObject.enabled = true; // Activa el script si el altar está activo
            interactableObject.activar = true; // Activa la variable activar si el altar está activo
        }
        else
        {
            interactableObject.enabled = false; // Lo desactiva si no lo está
            interactableObject.activar = false; // Desactiva la variable activar si el altar no está activo 
        }
    }
}
