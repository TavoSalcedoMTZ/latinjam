using UnityEngine;

public class Interactor : MonoBehaviour
{
    public ControllerUi controllerUi;

    private InteractableObject objetoDetectado;

    void Update()
    {
        DetectarObjeto();
        ProcesarInput();
    }

    void DetectarObjeto()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;

        if (Physics.Raycast(ray, out _hit, 3))
        {
            objetoDetectado = _hit.transform.GetComponent<InteractableObject>();

            if (objetoDetectado != null && objetoDetectado.activar)
            {
                controllerUi.objetoInRaycast = true;
            }
            else
            {
                controllerUi.objetoInRaycast = false;
                objetoDetectado = null;
            }
        }
        else
        {
            controllerUi.objetoInRaycast = false;
            objetoDetectado = null;
        }
    }

    void ProcesarInput()
    {
        if (objetoDetectado != null && Input.GetKeyDown(KeyCode.E))
        {
            objetoDetectado.Interact();
        }
    }
}
