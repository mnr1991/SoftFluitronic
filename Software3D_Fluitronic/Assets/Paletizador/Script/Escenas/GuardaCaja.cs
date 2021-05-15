using Assets.Paletizador.Script.Clases;
using UnityEngine;

namespace Assets.Paletizador.Script.Escenas
{
    public class GuardaCaja:MonoBehaviour
    {
        public Caja MiCaja { get; set; }
        public static GuardaCaja Instancia { get; private set; }
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
