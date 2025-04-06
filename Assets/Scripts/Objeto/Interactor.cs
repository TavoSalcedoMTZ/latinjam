using UnityEngine;

public class Interactor : MonoBehaviour
{
    public ControllerUi controllerUi;

    public void Interaction()
    {


   

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit _hit;


            if (Physics.Raycast(ray, out _hit, 3))
            {

                InteractableObject interactable = _hit.transform.GetComponent<InteractableObject>();
                if (interactable != null)
                {
                    controllerUi.objetoInRaycast = true;

                if (Input.GetKeyDown(KeyCode.E)){

                     interactable.Interact();
                    }

                }
                 
                else
                {
                    controllerUi.objetoInRaycast = false;   
            }
            }   
             else
            { return; }

    }
        
    


    void Update()
    {
        Interaction();
    }
}   


