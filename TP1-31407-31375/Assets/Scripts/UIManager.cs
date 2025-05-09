using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text statusText;
    public Text countdownText;

    public GameObject nextParkingPromptPanel;
    public Button yesButton;
    public Button noButton;

    private ParkingManager parkingManager;

    private void Start()
    {
        statusText.gameObject.SetActive(false);
        countdownText.gameObject.SetActive(false);
        nextParkingPromptPanel.SetActive(false);
    }

    public void SetManager(ParkingManager manager)
    {
        parkingManager = manager;

        // Botão "Sim" → continua para outro estacionamento
        yesButton.onClick.AddListener(() =>
        {
            statusText.gameObject.SetActive(false); // esconde "Parabéns"
            nextParkingPromptPanel.SetActive(false);
            Time.timeScale = 1f;
            parkingManager.OnUserChoseNext();
        });

        // Botão "Não" → mostra mensagem final
        noButton.onClick.AddListener(() =>
        {
            nextParkingPromptPanel.SetActive(false);
            statusText.text = "Obrigado por jogar!";
            statusText.gameObject.SetActive(true);
            parkingManager.OnUserChoseEnd();
        });
    }

    public void ShowSuccessMessage()
    {
        statusText.text = "Parabéns! Estacionaste com sucesso!";
        statusText.gameObject.SetActive(true);
        countdownText.gameObject.SetActive(false);
        Time.timeScale = 0f;
    }

    public void UpdateCountdown(int seconds)
    {
        countdownText.gameObject.SetActive(true);
        countdownText.text = "Estacionar: " + seconds.ToString();
    }

    public void HideCountdown()
    {
        countdownText.gameObject.SetActive(false);
    }

    public void ShowNextParkingPrompt()
    {
        statusText.gameObject.SetActive(false); // esconde "Parabéns" antes de perguntar
        nextParkingPromptPanel.SetActive(true);
    }
}
