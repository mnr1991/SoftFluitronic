using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UR.Comunicacion
{
    public class EnlaceUI_UR : MonoBehaviour
    {
        public GameObject objControl;
        private ControlUR Control;
        public Text j1;
        public Text j2;
        public Text j3;
        public Text j4;
        public Text j5;
        public Text j6;
        public Text X;
        public Text Y;
        public Text Z;
        public Text RX;
        public Text RY;
        public Text RZ;
        public InputField cmd;
        private Thread main;
        private Thread freedrive;

        private void Start()
        {
            Control=objControl.GetComponent<ControlUR>();
        }

        private void Update()
        {
            if (Control != null)
            {
                j1.text = Control.Posicion_J1.ToString();
                j2.text = Control.Posicion_J2.ToString();
                j3.text = Control.Posicion_J3.ToString();
                j4.text = Control.Posicion_J4.ToString();
                j5.text = Control.Posicion_J5.ToString();
                j6.text = Control.Posicion_J6.ToString();
                X.text = Control.Posicion_X.ToString();
                Y.text = Control.Posicion_Y.ToString();
                Z.text = Control.Posicion_Z.ToString();
                RX.text = Control.Posicion_RX.ToString();
                RY.text = Control.Posicion_RY.ToString();
                RZ.text = Control.Posicion_RZ.ToString();
            }
        }
        public void conectar()
        {
            if (Control != null)
            {
                Control.Conectar();
            }
            
        }
        public void Escribe()
        {
            if (Control != null)
            {
                //Control.Escribir(cmd.text.Trim());
                iniciarMain();
                /*if (freedrive == null)
                {
                    iniciarFreedrive();
                }
                else
                {
                    if (freedrive.IsAlive) freedrive.Abort();
                    freedrive = null;
                }*/
            }
        }

        
        private void iniciarMain()
        {
            main = new Thread(()=>hiloMain());
            main.IsBackground = true;
            main.Start();
        }
        private void hiloMain()
        {
            double[] target1 = new double[6] { 0.3*1000.0,-0.4 * 1000.0, 0.3 * 1000.0, 1.0,-2.0,0.4};
            double[] target2 = new double[6] { 0.8 * 1000.0, -0.4 * 1000.0, 0.3 * 1000.0, 1.0, -2.0, 0.4 };
            


            for (int i = 0; i < 10; i++)
            {
                Control.Escribir("movej(p[0.3,-0.4,0.3,1,-2,0.4])");
                bool res1 = false;
                while (!res1)
                {
                    double[] actual = new double[6] {Control.Posicion_X, Control.Posicion_Y,Control.Posicion_Z, Control.Posicion_RX_rad, Control.Posicion_RY_rad,Control.Posicion_RZ_rad };
                    res1 = ptoTolerancia(target1,actual,0.1);
                }
                Control.Escribir("movej(p[0.8,-0.4,0.3,1,-2,0.4])");
                bool res2 = false;
                while (!res2)
                {
                    double[] actual = new double[6] { Control.Posicion_X, Control.Posicion_Y, Control.Posicion_Z, Control.Posicion_RX_rad, Control.Posicion_RY_rad, Control.Posicion_RZ_rad };
                    res2 = ptoTolerancia(target2, actual, 0.1);
                }
                
            }
            if (main.IsAlive) main.Abort();
        
        }

        private void iniciarFreedrive()
        {
            freedrive = new Thread(() => hiloFreedrive());
            freedrive.IsBackground = true;
            freedrive.Start();
        }
        private void hiloFreedrive()
        {
            while(true)
            Control.Escribir("freedrive_mode()");
        }
        private bool ptoTolerancia(double[]target,double[] actual,double tolerancia)
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
    }
}
