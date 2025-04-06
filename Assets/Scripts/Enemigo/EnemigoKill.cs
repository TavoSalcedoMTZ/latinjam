using UnityEngine;

public class EnemigoKill : MonoBehaviour
{
    private GestorDeVariables gestorDeVariables;
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            gestorDeVariables.PlayerMuerto = true;
        }
     
    }

    public void Start()
    {
        gestorDeVariables = FindObjectOfType<GestorDeVariables>();
    }
}
