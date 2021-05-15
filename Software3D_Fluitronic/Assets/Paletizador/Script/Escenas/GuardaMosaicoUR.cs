using System.Collections;
using UnityEngine;
namespace Assets.Paletizador.Script.Escenas
{
    public class GuardaMosaicoUR:MonoBehaviour
    {
        public Hashtable miMosaico;
        public static GuardaMosaicoUR Instancia { get; private set; }
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
