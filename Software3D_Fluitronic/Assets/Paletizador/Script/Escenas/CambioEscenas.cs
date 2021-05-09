using UnityEngine;
using UnityEngine.SceneManagement;
public class CambioEscenas:MonoBehaviour
{
    /// <summary>
    /// Cambiamos de escena a la escena con el mismo nobre que el argumento
    /// </summary>
    /// <param name="strEscena"></param>
    public void CambiandoEscena(string strEscena)
    {
        SceneManager.LoadScene(strEscena);
    }

    
}
  
