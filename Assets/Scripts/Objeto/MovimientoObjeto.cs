using UnityEngine;

public class MovimientoObjeto : MonoBehaviour
{
    public float velocidadRotacion = 50f;
    public float amplitudMovimiento = 0.5f;
    public float velocidadMovimiento = 2f;

    private Vector3 posicionInicial;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        // Rotar
        transform.Rotate(Vector3.up * velocidadRotacion * Time.deltaTime);

        // Subir y bajar (movimiento senoidal)
        float desplazamientoY = Mathf.Sin(Time.time * velocidadMovimiento) * amplitudMovimiento;
        transform.position = new Vector3(
            posicionInicial.x,
            posicionInicial.y + desplazamientoY,
            posicionInicial.z
        );
    }
}
