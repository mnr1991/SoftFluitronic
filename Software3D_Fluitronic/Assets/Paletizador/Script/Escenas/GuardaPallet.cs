using Assets.Paletizador.Script.Clases;
using UnityEngine;

namespace Assets.Scripts.Escenas
{
    public class GuardaPallet : MonoBehaviour
    {
        public Pallet MiPallet { get; set; }
        public static GuardaPallet Instancia { get; private set; }
        private void Awake()
        {
            if (Instancia == null)
            {
                Instancia = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
