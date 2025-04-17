using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 10f;
    public float turnSpeed = 50f;
    private Rigidbody rb;
    public int vidas = 3; // Definir as vidas iniciais

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            vidas--; // Reduz uma vida
            if (vidas <= 0)
            {
                // Game Over
                Debug.Log("Game Over!");
            }
        }
    }

    void FixedUpdate()
    {
        float move = Input.GetAxis("Vertical") * speed;
        float turn = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;

        // Movimento usando MovePosition
        rb.MovePosition(transform.position + transform.forward * move * Time.deltaTime);

        // Rotação
        transform.Rotate(0, turn, 0);
    }
}
