using UnityEngine;

public class GestorDeVariables : MonoBehaviour
{
    public bool stateZonaSegura;
    public GameObject playermodel;
    private RotationCamara rotationCamara;

    private void Start()
    {
        rotationCamara = FindObjectOfType<RotationCamara>();
    }
    public void ActivarEsconderse()
    {
        playermodel.SetActive(false);
        stateZonaSegura = true;

        
    }

    public void DesactivarEsconderse()
    {
        playermodel.SetActive(true);
        stateZonaSegura = false;
        rotationCamara.DesactivarCursor();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
