using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CabeceraPaletizador : MonoBehaviour
{
    public TextMeshProUGUI Titulo;
    public Button ConfigPallet;
    public Button ConfigCaja;
    public Button ConfigMosaico;
    public Button Lanzar;
    // Start is called before the first frame update
    void Start()
    {
        var _escena = SceneManager.GetActiveScene();
        Color _color;
        switch (_escena.name)
        {
            case "@ConfiPallet":
                Titulo.text = "CONFIGURACION PALLET";
                _color=ConfigPallet.colors.selectedColor;
                ConfigPallet.image.color = _color;
                break;
            case "@ConfiCaja":
                Titulo.text = "CONFIGURACION CAJA";
                _color = ConfigCaja.colors.selectedColor;
                ConfigCaja.image.color = _color;
                break;
            case "@ConfiMosaico":
                Titulo.text = "CONFIGURACION MOSAICO";
                _color = ConfigMosaico.colors.selectedColor;
                ConfigMosaico.image.color = _color;
                break;
            default:
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cambio(string strEscena)
    {
        SceneManager.LoadScene(strEscena);
    }
}
