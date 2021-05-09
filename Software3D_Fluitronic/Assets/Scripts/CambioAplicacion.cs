using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioAplicacion : MonoBehaviour
{
    /// <summary>
    /// Cambiamos de escena a la escena con el mismo nobre que el argumento
    /// </summary>
    /// <param name="strEscena"></param>
    public void CambiandoAplicacion(string strEscena)
    {
        SceneManager.LoadScene(strEscena);
    }

    /// <summary>
    /// Salimos del programa
    /// </summary>
    public void Salir()
    {
        Application.Quit();
    }
}
