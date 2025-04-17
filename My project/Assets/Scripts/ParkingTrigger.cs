using UnityEngine;

public class ParkingTrigger : MonoBehaviour
{
    public UIManager uiManager;

    [System.Obsolete]
    void Start()
    {
        // Se o uiManager não for atribuído no Inspector, tenta encontrá-lo na cena
        if (uiManager == null)
        {
            uiManager = FindObjectOfType<UIManager>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Exibe o nome e a tag do objeto que entrou no trigger
        Debug.Log("Entrou no trigger com: " + other.name + " - Tag: " + other.tag);

        // Verifica se o objeto que entrou tem a tag "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("Carro estacionado!");
            if (uiManager != null)
            {
                uiManager.ShowSuccessMessage(); // Exibe a mensagem de sucesso
            }
            else
            {
                Debug.LogError("uiManager não está atribuído!");
            }
        }
        else
        {
            // Se não for o carro (objeto com tag "Player"), exibe o tipo de objeto
            Debug.Log("Objeto não é Player, é: " + other.tag);
        }
    }
}
