using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento
    public float jumpForce = 10f; // Fuerza de salto
    private bool isGrounded; // Para verificar si el jugador está en el suelo

    void Update()
    {
        // Moverse horizontalmente con las flechas de dirección
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speed * Time.deltaTime;
        transform.Translate(movement);

        // Saltar si el jugador está en el suelo
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verificar si el jugador está en el suelo al colisionar con un objeto etiquetado como "Ground"
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ButtonCube"))
        {
            other.GetComponent<ButtonCubeController>().ToggleCube();
        }
    }
}