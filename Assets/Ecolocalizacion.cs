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

                // Intentamos modificar el segundo material (OutlineGlow)
                Renderer renderer = hit.collider.GetComponent<MeshRenderer>();
                if (renderer != null)
                {
                    Debug.Log($"Objeto {hit.collider.name} tiene {renderer.materials.Length} materiales");  

                    Material[] materials = renderer.materials; // Obtener todos los materiales

                    if (materials.Length > 1) // Asegurar que hay más de un material
                    {
                        Material outlineMaterial = materials[1]; // Segundo material (OutlineGlow)

                        // Verificar si tiene las propiedades antes de cambiarlas
            
                            // Cambiar color y escala
                            outlineMaterial.SetColor("_Color", Color.magenta * 10.0F);
                            outlineMaterial.SetFloat("_Scale", 1.06f);

                            // Debug para comprobar cambios
                
                        
                    }
                    else
                    {
                        Debug.LogWarning($"El objeto {hit.collider.name} no tiene suficientes materiales.");
                    }
                }
            }
            else
            {
                Debug.DrawLine(transform.position, transform.position + direccion * rangoDeteccion, Color.blue, 1f);
            }
        }
    }
}
