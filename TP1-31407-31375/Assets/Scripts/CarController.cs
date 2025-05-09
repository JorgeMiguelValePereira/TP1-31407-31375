using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    public float speed = 10f;
    public float turnSpeed = 50f;
    public int vidas = 3;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Reforçar estabilidade descendo o centro de massa
        rb.centerOfMass = new Vector3(0f, -1.0f, 0f);
    }

    void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        // Direção para frente, corrigida para ficar no plano (sem levantar)
        Vector3 forwardFlat = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
        Vector3 velocity = forwardFlat * moveInput * speed;

        // Evita escorregar de lado (drift)
        Vector3 localVelocity = transform.InverseTransformDirection(rb.linearVelocity);
        localVelocity.x = 0f; // remove movimento lateral
        rb.linearVelocity = transform.TransformDirection(localVelocity);

        // Aplica velocidade para frente
        rb.linearVelocity = velocity;

        // Rotação controlada
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, turnInput * turnSpeed * Time.fixedDeltaTime, 0f));
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            vidas--;
            if (vidas <= 0)
            {
                Debug.Log("Game Over!");
            }
        }
    }
}
