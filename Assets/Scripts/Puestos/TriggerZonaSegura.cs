using UnityEngine;
using UnityEngine.Events;
public class TriggerZonaSegura : MonoBehaviour
{

    public UnityEvent OnEnterZonaSegura;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
          OnEnterZonaSegura.Invoke();
        }
    }
}
