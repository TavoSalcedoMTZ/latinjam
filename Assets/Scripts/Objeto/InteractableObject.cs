using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    public UnityEvent OnInteract;
   public bool activar=true;

    public void Interact()
    {
        Debug.Log("Interact called on " + gameObject.name);

        if (OnInteract != null && activar)
        {
            Debug.Log("OnInteract is set, invoking...");
            OnInteract.Invoke();
        }
        else
        {
            Debug.LogWarning("OnInteract event is null for " + gameObject.name);
        }
    }
}