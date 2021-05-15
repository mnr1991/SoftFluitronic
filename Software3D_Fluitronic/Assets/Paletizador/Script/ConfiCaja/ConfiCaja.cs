using Assets.Paletizador.Script.Clases;
using Assets.Paletizador.Script.Escenas;
using System;
using TMPro;
using UnityEngine;

namespace Assets.Paletizador.Script.ConfiCaja
{
    public class ConfiCaja:MonoBehaviour
    {
        private Caja cajaActual;
        public GameObject GameCaja;
        public TMP_InputField inputAltura;
        public TMP_InputField inputAncho;
        public TMP_InputField inputLargo;
        public TMP_InputField inputPeso;
        private void Start()
        {

            if (GuardaCaja.Instancia.MiCaja == null)
            {
                cajaActual = new Caja();
                cajaActual.Alto = GameCaja.transform.localScale.z;
                cajaActual.Ancho = GameCaja.transform.localScale.x;
                cajaActual.Largo = GameCaja.transform.localScale.y;
                GuardaCaja.Instancia.MiCaja = cajaActual;
            }
            else
            {
                cajaActual=GuardaCaja.Instancia.MiCaja;
                if (GameCaja != null)
                {
                    Vector3 vector3 = new Vector3((float)cajaActual.Ancho, (float)cajaActual.Largo, (float)cajaActual.Alto);
                    GameCaja.transform.localScale = vector3;
                }
            }
            ActualizaTexto();
        }

        private void Update()
        {
            
        }
        public void actualizaAltura()
        {
            float altura =(float) Convert.ToDouble(inputAltura.text.Replace("mm",""));
            float x, y, z;
            x = GameCaja.transform.localScale.x;
            z = altura;
            y = GameCaja.transform.localScale.y;
            cajaActual.Alto = z;
            GameCaja.transform.localScale=new Vector3(x,y,z);
            ActualizaTexto();
        }

        public void actualizaAncho()
        {
            float altura = (float)Convert.ToDouble(inputAncho.text.Replace("mm", ""));
            float x, y, z;
            z = GameCaja.transform.localScale.z;
            x = altura;
            y = GameCaja.transform.localScale.y;
            cajaActual.Ancho = x;
            GameCaja.transform.localScale = new Vector3(x, y, z);
            ActualizaTexto();
        }

        public void actualizaLargo()
        {
            float altura = (float)Convert.ToDouble(inputLargo.text.Replace("mm", ""));
            float x, y, z;
            x = GameCaja.transform.localScale.x;
            y = altura;
            z = GameCaja.transform.localScale.z;
            cajaActual.Largo = y;
            GameCaja.transform.localScale = new Vector3(x, y, z);
            ActualizaTexto();
        }
        private void ActualizaTexto()
        {
            
            inputAltura.text = (cajaActual.Alto).ToString("F") + " mm";
            inputAncho.text = (cajaActual.Ancho).ToString("F") + " mm";
            inputLargo.text = (cajaActual.Largo).ToString("F") + " mm";
            inputPeso.text = (cajaActual.Peso).ToString("F") + " Kg";

            //fjhfjgkjh
        }
    }
}
