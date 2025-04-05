using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCamara : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    private GestorDeVariables gestorDeVariables;

    private float xRotation = 0f;

    void Start()
    {
        gestorDeVariables = FindObjectOfType<GestorDeVariables>(); 
        Cursor.lockState = CursorLockMode.Locked;


    }

    void Update()
    {
        if (!gestorDeVariables.stateZonaSegura)
        {
            

            // Leer la entrada del mouse
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;


            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
        else
        {

        }
    }

    public void DesactivarCursor() { Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false; }
}