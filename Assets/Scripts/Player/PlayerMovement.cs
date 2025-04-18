using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speedOrigin = 2.0f;
    public float speed = 2.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;
    public float stamina;
    public float staminaIncrease;
    public float staminaDecrease;
    private GestorDeVariables gestorDeVariables;


    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        gestorDeVariables = FindObjectOfType<GestorDeVariables>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!gestorDeVariables.stateZonaSegura)
        {
            isGrounded = controller.isGrounded;
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }


            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 move = transform.right * x + transform.forward * z;


            if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
            {
                speed = speedOrigin + 3;

                stamina -= (Time.deltaTime * staminaDecrease);

                if (stamina <= 0.0f)
                {
                    speed = 0.5f;
                }

            }
            else
            {
                speed = speedOrigin;
                if (stamina <= 10)
                {
                    stamina += (Time.deltaTime * staminaIncrease);
                }

            }




            controller.Move(move * speed * Time.deltaTime);



            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
        else
        {

        }
    }
}