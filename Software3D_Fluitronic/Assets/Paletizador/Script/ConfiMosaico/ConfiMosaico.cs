using Assets.Paletizador.Script.Clases;
using Assets.Paletizador.Script.Escenas;
using Assets.Scripts.Escenas;
using UnityEngine;

namespace Assets.Paletizador.Script.ConfiMosaico
{
    public class ConfiMosaico:MonoBehaviour
    {
        private Pallet Pactual;
        private Caja Cactual;
        public GameObject CajaOriginal;
        private int indice = 0;
        public GameObject[] Pallets3D;
        private int separacionLargo = 1;
        private int separacionAncho = 1;
        private Caja[] Cajas;
        Vector3 PosPallet;

        private void Start()
        {
            for (int i = 0; i < Pallets3D.Length; i++)
            {
                Pallets3D[i].SetActive(false);
            }
            CajaOriginal.SetActive(false);
            if (GuardaPallet.Instancia.MiPallet!=null)
            {
                indice = GuardaPallet.Instancia.indice;
                Pallets3D[indice].SetActive(true);
                Pactual = GuardaPallet.Instancia.MiPallet;
                if (GuardaCaja.Instancia.MiCaja!=null)
                {
                    Cactual = GuardaCaja.Instancia.MiCaja;
                    Vector3 scala = new Vector3((float)Cactual.Ancho, (float)Cactual.Alto, (float)Cactual.Largo);
                    CajaOriginal.transform.localScale=scala;
                    CajaOriginal.SetActive(true);
                }
                

            }
            PosPallet = new Vector3(0f,0f,0f);
        }

        private void CalcularMosaicoAutomatico()
        {
            // Calculo de columnas y filas
            int cols, filas;
            cols = (int)(Pactual.Ancho / Cactual.Ancho);
            filas = (int)(Pactual.Largo/Cactual.Largo);
            // Numero de cajas sin girar
            double distanciaAncho = cols * Cactual.Ancho + (cols - 1) * separacionAncho;
            double distanciaLargo = filas * Cactual.Largo + (filas - 1) * separacionLargo;

            if (Pactual.Largo < distanciaLargo && filas>0) filas -= 1;
            if (Pactual.Ancho < distanciaAncho && cols>0) cols -= 1;

            // Cajas giradas
            int Cajas_Pintadas = cols * filas;
            int CajasTotales = (int)((Pactual.Largo * Pactual.Ancho) /(Cactual.Ancho * Cactual.Largo));
            int CajasPosibles = CajasTotales - Cajas_Pintadas;

            // Posiciones cajas
            Cajas = new Caja[CajasPosibles];
            // Recorremos filas
            for (int i = 1; i <= filas; i++)
            {
                // Recorremos columnas
                for (int j = 1; j <= cols; j++)
                {
                    Caja cajaNueva = new Caja();
                    float posX, posY, posZ;
                    //posX=PosPallet.x+Cactual.Ancho
                }
            }
        }

    }
}
