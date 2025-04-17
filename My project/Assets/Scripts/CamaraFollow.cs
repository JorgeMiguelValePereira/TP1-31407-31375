using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform carTransform;  // Referência ao carro
    public Vector3 offset;         // Distância entre a câmara e o carro

    void Start()
    {
        // Se a câmara não tiver um offset, definimos uma distância padrão
        if (offset == Vector3.zero)
        {
            offset = new Vector3(0, 5, -10);  // Ajusta esses valores conforme necessário
        }
    }

    void Update()
    {
        // Atualiza a posição da câmara para seguir o carro com um offset
        transform.position = carTransform.position + offset;

        // A câmara olha sempre para o carro
        transform.LookAt(carTransform);
    }
}

