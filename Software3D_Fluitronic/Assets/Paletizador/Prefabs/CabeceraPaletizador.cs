using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Paletizador.Prefabs
{
    public class CabeceraPaletizador : MonoBehaviour
    {
        public TextMeshProUGUI Titulo;
        public Button ConfigPallet;
        public Button ConfigCaja;
        public Button ConfigMosaico;
        public Button Lanzar;

        public RawImage LineaPallet;
        public RawImage LineaCaja;
        public RawImage LineaMosaico;
        public RawImage LineaLanzar;

        // Start is called before the first frame update
        void Start()
        {
            var _escena = SceneManager.GetActiveScene();
            Color _color;
            LineaPallet.gameObject.SetActive(false);
            LineaCaja.gameObject.SetActive(false);
            LineaMosaico.gameObject.SetActive(false);
            LineaLanzar.gameObject.SetActive(false);
            
            switch (_escena.name)
            {
                case "@ConfiPallet":
                    Titulo.text = "CONFIGURACION PALLET";
                    _color = ConfigPallet.colors.selectedColor;
                    ConfigPallet.image.color = _color;
                    LineaPallet.gameObject.SetActive(true);
                    break;
                case "@ConfiCaja":
                    Titulo.text = "CONFIGURACION CAJA";
                    _color = ConfigCaja.colors.selectedColor;
                    ConfigCaja.image.color = _color;
                    LineaCaja.gameObject.SetActive(true);
                    break;
                case "@ConfiMosaico":
                    Titulo.text = "CONFIGURACION MOSAICO";
                    _color = ConfigMosaico.colors.selectedColor;
                    ConfigMosaico.image.color = _color;
                    LineaMosaico.gameObject.SetActive(true);
                    break;
                case "@Lanzar":
                    Titulo.text = "LANZAR";
                    _color = Lanzar.colors.selectedColor;
                    Lanzar.image.color = _color;
                    LineaLanzar.gameObject.SetActive(true);
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
}