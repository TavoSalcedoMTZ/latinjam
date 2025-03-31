using UnityEngine;

public class Ecolocalizacion : MonoBehaviour
{
    public float rangoDeteccion = 2000000f;  
    public int cantidadRayos = 20;       

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            Debug.Log("Ecolocalización activada"); 
            ActivarEcolocalizacion();
        }
    }

    void ActivarEcolocalizacion()
    {
        for (int i = 0; i < cantidadRayos; i++)
        {
            float angulo = (360f / cantidadRayos) * i;
            Vector3 direccion = Quaternion.Euler(0, angulo, 0) * Vector3.forward; 

            RaycastHit hit;
            if (Physics.Raycast(transform.position, direccion, out hit, rangoDeteccion))
            {
                Debug.DrawLine(transform.position, hit.point, Color.red, 1f);
                Debug.Log($"Impacto en: {hit.collider.name} a {hit.point}");
            }
            else
            {
                Debug.DrawLine(transform.position, transform.position + direccion * rangoDeteccion, Color.blue, 1f);
            }
        }
    }
}
