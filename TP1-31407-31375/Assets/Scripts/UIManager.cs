using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI statusText;

    public void ShowSuccessMessage()
    {
        statusText.text = "Parabéns! Estacionaste com sucesso!";
        statusText.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
}
