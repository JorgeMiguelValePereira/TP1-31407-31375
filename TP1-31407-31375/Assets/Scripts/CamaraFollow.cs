using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform carTransform;  
    public Vector3 offset;         // Distância entre a câmara e o carro

    void Start()
    {
        // Se a câmara não tiver um offset, definimos uma distância padrão
        if (offset == Vector3.zero)
        {
            offset = new Vector3(0, 5, -10);  
        }
    }

    void Update()
    {
        // Atualiza a posição da câmara para seguir o carro com um offset
        transform.position = carTransform.position + offset;

        transform.LookAt(carTransform);
    }
}

