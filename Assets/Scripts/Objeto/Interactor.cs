using UnityEngine;

public class Interactor : MonoBehaviour
{

    public void Interaction()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit _hit;


            if (Physics.Raycast(ray, out _hit, 1000))
            {
                // Comprobar si el objeto golpeado tiene el componente InteractableObject
                InteractableObject interactable = _hit.transform.GetComponent<InteractableObject>();
                if (interactable != null)
                {
                    Debug.Log("Objeto interactuable encontrado");
                    interactable.Interact();
                }
                else
                {
                    Debug.Log("No se encontró el componente InteractableObject en el objeto golpeado");
                }
            }
            else
            {
                Debug.Log("No se detectó ningún objeto con el Raycast");
            }
        }
    }

    void Update()
    {
        Interaction();
    }
}   


