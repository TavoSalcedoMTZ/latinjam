using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    public UnityEvent OnInteract;

    public void Interact()
    {
        Debug.Log("Interact called on " + gameObject.name);

        if (OnInteract != null)
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