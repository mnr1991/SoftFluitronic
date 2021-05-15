using Assets.Paletizador.Script.Clases;
using Assets.Scripts.Escenas;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Paletizador.Script.ConfiPallet
{
    public class ConfiPallet : MonoBehaviour
    {
        public TextMeshProUGUI Nombre1;
        public TextMeshProUGUI Nombre2;
        public List<Pallet> Pallets { get; set; }
        private int indice;
        private Pallet actual;
        private Pallet[] listaPallets;
        public GameObject[] Pallets3D;
        public TMP_InputField inputAncho;
        public TMP_InputField inputLargo;
        public TMP_InputField inputAlto;
        public TMP_InputField inputPeso;


        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < Pallets3D.Length; i++)
            {
                Pallets3D[i].SetActive(false);
              
            }
            // Creamos lista de pallets
            Pallets = new List<Pallet>();
            var pallet = new Pallet();
            pallet.Alto = 144.0;
            pallet.Ancho = 1000.0;
            pallet.Largo = 1200.0;
            pallet.Nombre = "Americano";
            Pallets.Add(pallet);
            pallet = new Pallet();
            pallet.Alto = 144.0;
            pallet.Ancho = 800.0;
            pallet.Largo = 600.0;
            pallet.Nombre = "Medio Europeo";
            Pallets.Add(pallet);
            pallet = new Pallet();
            pallet.Alto = 144.0;
            pallet.Ancho = 800.0;
            pallet.Largo = 1200.0;
            pallet.Nombre = "Europeo";
            Pallets.Add(pallet);
            

            listaPallets = Pallets.ToArray();
            if (GuardaPallet.Instancia.MiPallet != null)
            {
                actual = GuardaPallet.Instancia.MiPallet;
                foreach (Pallet current in Pallets)
                {
                    if (current.Nombre.Trim() == GuardaPallet.Instancia.MiPallet.Nombre.Trim())
                    {
                        actual = current;
                        break;
                    }
                }
                indice = Pallets.IndexOf(actual);
            }
            else
            {
                indice = 2;
                actual = Pallets[indice];
                GuardaPallet.Instancia.MiPallet = actual;
               
            }

            ActualizaTextos();
            Pallets3D[indice].SetActive(true);
            GuardaPallet.Instancia.indice = indice;


        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Siguiente()
        {
            int ant = indice;
            indice += 1;
            if (indice>Pallets.Count-1)
            {
                indice = 0;
            }
            Pallets3D[ant].SetActive(false);
            Pallets3D[indice].SetActive(true);
            actual = listaPallets[indice];
            GuardaPallet.Instancia.MiPallet = actual;
            ActualizaTextos();
            GuardaPallet.Instancia.indice = indice;

        }
        public void Anterior()
        {
            int ant = indice;
            indice -= 1;
            if (indice<0)
            {
                indice = Pallets.Count-1;
            }
            Pallets3D[ant].SetActive(false);
            Pallets3D[indice].SetActive(true);
            actual = listaPallets[indice];
            GuardaPallet.Instancia.MiPallet = actual;
            ActualizaTextos();
            GuardaPallet.Instancia.indice = indice;

        }

        private void ActualizaTextos()
        {
            Nombre1.text = actual.Nombre;
            Nombre2.text = actual.Nombre;
            inputAlto.text = listaPallets[indice].Alto.ToString() + " mm";
            inputAncho.text = listaPallets[indice].Ancho.ToString() + " mm";
            inputLargo.text = listaPallets[indice].Largo.ToString() + " mm";
            inputPeso.text = listaPallets[indice].Peso.ToString() + " Kg";
        }

      
    }
}
