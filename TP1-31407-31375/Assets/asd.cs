using UnityEngine;
using UnityEngine.SceneManagement;

public class asd : MonoBehaviour
{

    public GameObject Instrucoes;
   public void LoadScene()
   {
       SceneManager.LoadScene("SampleScene");
   }

   public void MostrarInformacoes()
   {
        Instrucoes.SetActive(true);
   }
}
