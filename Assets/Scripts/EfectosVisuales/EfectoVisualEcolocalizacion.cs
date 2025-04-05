using UnityEngine;

public class EfectoVisualEcolocalizacion : MonoBehaviour
{
    public GameObject ondaPrefab; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject onda = Instantiate(ondaPrefab, transform.position, Quaternion.identity);
            Destroy(onda, 1f); 
        }
    }
}
