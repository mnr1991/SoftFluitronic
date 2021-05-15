using Assets.Paletizador.Script.Clases;
using Assets.Paletizador.Script.Escenas;
using Assets.Scripts.UR.Comunicacion;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Paletizador.Script.LanzarUR
{
    public class LanzarUR:MonoBehaviour
    {
        public GameObject CajasGuardadas;
        private Hashtable hashCajas;
        public GameObject ObjControl;
        private ControlUR control;
        private Thread main;
        public bool startMosaico;
        private bool pausaMosaico;

        public Text[] Js;
        public Text[]Coordenas;

        private void Start()
        {
            hashCajas = CajasGuardadas.GetComponent<GuardaMosaicoUR>().miMosaico;
            control = ObjControl.GetComponent<ControlUR>();
        }

        private void Update()
        {
            if (control != null)
            {
               Js[0].text = control.Posicion_J1.ToString();
                Js[1].text = control.Posicion_J2.ToString();
                Js[2].text = control.Posicion_J3.ToString();
                Js[3].text = control.Posicion_J4.ToString();
                Js[4].text = control.Posicion_J5.ToString();
                Js[5].text = control.Posicion_J6.ToString();
                Coordenas[0].text = control.Posicion_X.ToString();
                Coordenas[1].text = control.Posicion_Y.ToString();
                Coordenas[2].text = control.Posicion_Z.ToString();
                Coordenas[3].text = control.Posicion_RX.ToString();
                Coordenas[4].text = control.Posicion_RY.ToString();
                Coordenas[5].text = control.Posicion_RZ.ToString();
            }
        }

        /// <summary>
        /// Preparamos una trama de puntos segun las coordenadas de una caja
        /// </summary>
        /// <param name="caja"></param>
        /// <returns></returns>
        string PreparoTrama(Caja caja)
        {
            string trama = "";
            trama = "movej(p[";
            trama += caja.x_ur.ToString() + ","; trama += caja.y_ur.ToString() + ","; trama += caja.z_ur.ToString() + ",";
            trama += caja.rx_ur.ToString() + ","; trama += caja.ry_ur.ToString() + ","; trama += caja.rz_ur.ToString() + "])";
            return trama;
        }

        /// <summary>
        /// Conectamos con el UR
        /// </summary>
        public void ConectarUR()
        {

            control.Conectar();
        }

        /// <summary>
        /// Calcula la aproximacion al punto con una tolerancia 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="actual"></param>
        /// <param name="tolerancia"></param>
        /// <returns></returns>
        private bool ptoTolerancia(double[] target, double[] actual, double tolerancia)
        {
            bool res = true;
            for (int i = 0; i < 6; i++)
            {
                double resta = target[i] - actual[i];
                if (resta < 0.0) resta = resta * -1.0;
                if (resta > tolerancia)
                {
                    res = false;
                    break;
                }
            }
            return res;
        }

        /// <summary>
        /// Empezamos con la ejecucion del mosaico y creamos el hilo 
        /// </summary>
        public void StartMosaico()
        {

            main = new Thread(() => CreaMosaico());
            main.IsBackground = true;
            startMosaico = true;
            main.Start();
        }

        /// <summary>
        /// Empezamos con el paletizado y ejecutamos sus puntos
        /// </summary>
        private void CreaMosaico()
        {
            while (startMosaico)
            {
                var capas = hashCajas.Count;
                /*
                 * 
                 * 
                 * Tengo array de puntos de recogida e intermedio
                 * 
                 */

                // Recorro capas
                foreach (DictionaryEntry de in hashCajas)
                {
                    Caja[] cajas =(Caja[]) de.Value;

                    //Recorro cajas
                    foreach (Caja caja in cajas)
                    {
                        if (!startMosaico) break;
                        if (!pausaMosaico)
                        {
                         /*
                         * 
                         *Voy a punto de cogida e intermedios
                         *
                         */
                        }


                        string trama = PreparoTrama(caja);

                        
                        if (!pausaMosaico)
                        {
                            control.Escribir(trama);
                        }

                        bool finEspera=false;
                        double[] target = new double[6] {caja.x_ur, caja.y_ur , caja.z_ur , caja.rx_ur , caja.ry_ur , caja.rz_ur };
                        // Bucle hasta que alcanza el punto enviado
                        while (!pausaMosaico && !startMosaico && finEspera)
                        {
                            double[] actual = new double[6] { control.Posicion_X, control.Posicion_Y, control.Posicion_Z, control.Posicion_RX_rad, control.Posicion_RY_rad, control.Posicion_RZ_rad };
                            finEspera = ptoTolerancia(target,actual,0.1);
                        }

                    }
                    if (!startMosaico) break;
                }

                // Finalizamos y cerramos el subproceso
                startMosaico = false;
                if (main.IsAlive) main.Abort();
                
            }

            
        }

        /// <summary>
        /// Paramos la ejecucion del mosaico
        /// </summary>
        public void StopMosaico()
        {
            if (main.IsAlive) main.Abort();
        }

        /// <summary>
        /// Pausamos la ejecucion del mosaico
        /// </summary>
        public void PausaMosaico()
        {
            if (main != null && main.IsAlive && main.ThreadState == ThreadState.Running) pausaMosaico = true;
        }

        /// <summary>
        /// Renaudamos la ejecucion del mosaico
        /// </summary>
        public void RenaudarMosaico()
        {
            if (main != null && main.IsAlive) pausaMosaico = false;
        }
    }
}
