using Assets.Paletizador.Prefabs;
using Assets.Paletizador.Script.Clases;
using Assets.Paletizador.Script.Escenas;
using Assets.Scripts.Escenas;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Paletizador.Script.ConfiMosaico
{
    public class ConfiMosaico : MonoBehaviour
    {
        private Pallet Pactual;
        private Caja Cactual;
        public GameObject CajaOriginal;
        public Camera camara;
        public GameObject posCam3D;
        public GameObject posCam2D;
        private int indice = 0;
        public GameObject[] Pallets3D;
        private GameObject Pallet3D;
        private int separacionLargo = 1;
        private int separacionAncho = 5;
        private int toleranciaBordes = 1;
        private Caja[] Cajas;
        Vector3 PosPallet;
        private GameObject objMosaico;
        private GameObject[] objCapas;
        GameObject CuerpoCaja;
        private int contadorCapas = 0;
        public Button[] BotonesCapa;
        public InputField NumeroCapas;

        private void Start()
        {
            for (int i = 0; i < BotonesCapa.Length; i++)
            {
                BotonesCapa[i].gameObject.SetActive(false);
                //BotonesCapa[i].transform.GetChild(1).gameObject.SetActive(false);
                //UnityEngine.Events.UnityAction call = new UnityEngine.Events.UnityAction(() => eventoBotonCapa(BotonesCapa[i].onClick));
                //BotonesCapa[i].onClick.AddListener(call);
            }


            for (int i = 0; i < Pallets3D.Length; i++)
            {
                Pallets3D[i].SetActive(false);
            }
            CajaOriginal.SetActive(false);
            if (GuardaPallet.Instancia.MiPallet != null)
            {
                indice = GuardaPallet.Instancia.indice;
                Pallets3D[indice].SetActive(true);
                Pallet3D = Pallets3D[indice];
                Pactual = GuardaPallet.Instancia.MiPallet;
                if (GuardaCaja.Instancia.MiCaja != null)
                {
                    Cactual = GuardaCaja.Instancia.MiCaja;
                    Vector3 scala = new Vector3((float)Cactual.Ancho, (float)Cactual.Alto, (float)Cactual.Largo);
                    CajaOriginal.transform.localScale = scala;
                    CajaOriginal.SetActive(true);
                    var numero = CajaOriginal.transform.childCount;
                    CuerpoCaja = CajaOriginal.transform.GetChild(numero - 1).gameObject;
                }


            }
            if (Pallet3D != null)
            {
                PosPallet = Pallet3D.transform.position;
            }
            else
            {
                PosPallet = new Vector3(0f, 0f, 0f);
            }
            if (Pallet3D != null)
            {
                objMosaico = new GameObject();
                objMosaico.transform.name = "Mosaico";
                //objMosaico.transform.parent = Pallet3D.transform;
                //objMosaico.AddComponent(typeof(BoxCollider));
                //objMosaico.GetComponent<BoxCollider>().size = new Vector3((float)Pactual.Ancho/1000f, (float)Cactual.Alto / 1000f, (float)Pactual.Largo / 1000f);
                float alt = ((float)Pactual.Alto + (float)Cactual.Alto / 2f) / 1000f;
                objMosaico.transform.position = new Vector3(Pallet3D.transform.position.x, Pallet3D.transform.position.y + alt, Pallet3D.transform.position.z);
                Pallet3D.AddComponent<BoxCollider>();
                Pallet3D.GetComponent<BoxCollider>().size= new Vector3(0.001f *(float)Pactual.Ancho, 0.001f * (float)Pactual.Largo, 0.001f * (float)Pactual.Alto);
                Pallet3D.GetComponent<BoxCollider>().center = new Vector3(0f,0f,(float)Pactual.Alto/2000f);
            }



        }

        public void CalcularMosaicoAutomatico()
        {
            int cols, filas, contador;
            // Creamos objeto capa
            objCapas = new GameObject[1];
            objCapas[0] = new GameObject();
            objCapas[0].transform.name = "Capa" + (contadorCapas + 1).ToString();


            // Calculo de columnas y filas
            cols = (int)(Pactual.Ancho / Cactual.Ancho);
            filas = (int)(Pactual.Largo / Cactual.Largo);
            // Numero de cajas sin girar
            double distanciaAncho = cols * Cactual.Ancho + (cols - 1) * separacionAncho;
            double distanciaLargo = filas * Cactual.Largo + (filas - 1) * separacionLargo;

            if ((Pactual.Largo + toleranciaBordes) < (distanciaLargo) && filas > 0) filas -= 1;
            if ((Pactual.Ancho + toleranciaBordes) < (distanciaAncho) && cols > 0) cols -= 1;
            distanciaAncho = cols * Cactual.Ancho + (cols - 1) * separacionAncho;
            distanciaLargo = filas * Cactual.Largo + (filas - 1) * separacionLargo;
            // Cajas giradas
            int Cajas_Pintadas = cols * filas;
            int CajasTotales = (int)((Pactual.Largo * Pactual.Ancho) / (Cactual.Ancho * Cactual.Largo));
            int CajasPosibles = CajasTotales - Cajas_Pintadas;
            
            Vector3 cajaAntLinea = new Vector3(0f, 0f, 0f);
            contador = 0;

            pintaCajas(filas,cols,ref contador,(float)Cactual.Ancho,(float)Cactual.Largo, CajaOriginal,(float)separacionLargo, (float)separacionAncho,cajaAntLinea,ref objCapas[0]);

            float espacioLargo0 = (float)Pactual.Largo-((float)distanciaLargo + (float)separacionLargo);
            float espacioAncho0 = (float)Pactual.Ancho-((float)distanciaAncho + (float)separacionAncho);
            int cols1, filas1;
            filas1 = (int)(espacioLargo0 / (float)Cactual.Ancho);
            cols1 = (int)(espacioAncho0 / (float)Cactual.Largo);

            float distanciaLargo1, distanciaAncho1;
            distanciaLargo1=(float)( filas1 * Cactual.Ancho + (filas1 - 1) * separacionLargo);
            distanciaAncho1 = (float)(cols1 * Cactual.Largo + (cols1 - 1) * separacionAncho);

            if ((espacioLargo0 + toleranciaBordes) < (distanciaLargo1) && filas1 > 0) filas1 -= 1;
            if ((espacioAncho0 + toleranciaBordes) < (distanciaAncho1) && cols1 > 0) cols1 -= 1;
            distanciaAncho1 =(float)( cols1 * Cactual.Largo + (cols1 - 1) * separacionAncho);
            distanciaLargo1 = (float)(filas1 * Cactual.Ancho + (filas1 - 1) * separacionLargo);
            cajaAntLinea = new Vector3(cajaAntLinea.x,cajaAntLinea.y,cajaAntLinea.z+(float)separacionLargo);
            GameObject patron2 = CajaOriginal;
            patron2.transform.rotation = new Quaternion(CajaOriginal.transform.rotation.x,CajaOriginal.transform.rotation.y+90,CajaOriginal.transform.rotation.z,CajaOriginal.transform.rotation.w);
            pintaCajas(filas1, cols1, ref contador, (float)Cactual.Largo, (float)Cactual.Ancho,patron2, (float)separacionLargo, (float)separacionAncho, cajaAntLinea, ref objCapas[0]);
            
            // Recolocamos capa
            float posX0, posY0, posZ0;
            GameObject obj = null;
            for (int i = 0; i < Pallet3D.transform.childCount; i++)
            {
                obj = Pallet3D.transform.GetChild(i).gameObject;
                if (obj.transform.name == "refPallet")
                {
                    break;
                }
            }
            // Espacio para centrar
            float espacioAncho = ((float)Pactual.Ancho - (float)distanciaAncho) / 1000f;
            float espacioLargo = ((float)Pactual.Largo - (float)distanciaLargo) / 1000f;
            objMosaico.transform.position = new Vector3(0f, 0f, 0f);
            objCapas[0].transform.position = obj.transform.position;
            objCapas[0].transform.position = new Vector3(obj.transform.position.x + ((float)Cactual.Ancho / 2) / 1000f + espacioAncho / 2f, obj.transform.position.y + ((float)Cactual.Alto / 2) / 1000f, obj.transform.position.z + ((float)Cactual.Largo / 2) / 1000f + espacioLargo / 2f);
            objCapas[0].transform.parent = objMosaico.transform;
            
            Destroy(CajaOriginal);
            contadorCapas = 1;
            BotonesCapa[contadorCapas - 1].gameObject.SetActive(true);
            BotonesCapa[contadorCapas - 1].transform.GetChild(1).gameObject.SetActive(false);
        }

        private int pintaCajas(int _filas, int _cols,ref int contador,float _ancho,float _largo,GameObject patron,float _separacionLargo,float _separacionAncho,Vector3 _cajaAntLinea,ref GameObject _objCapas)
        {

            // Recorremos filas
            for (int i = 1; i <= _filas; i++)
            {
                GameObject cajaTemp;
                float posX, posY, posZ;
                Vector3 cajaAnt;

                if (_objCapas.transform.childCount == 0)
                {
                    posX = _objCapas.transform.position.x;
                    posY = _objCapas.transform.position.y;
                    posZ = _objCapas.transform.position.z;

                }
                else
                {
                    posX = _cajaAntLinea.x;
                    posY = _cajaAntLinea.y;
                    posZ = _cajaAntLinea.z + _separacionLargo / 1000f + _largo / 1000f;

                }
                cajaAnt = new Vector3(posX, posY, posZ);
                cajaTemp = Instantiate(patron, cajaAnt, Quaternion.identity);
                cajaTemp.transform.localScale = CajaOriginal.transform.localScale;
                cajaTemp.transform.name = "Caja" + (contador + 1).ToString();
                cajaTemp.transform.parent = _objCapas.transform;

                cajaTemp.AddComponent<BoxCollider>();
                //cajaTemp.GetComponent<BoxCollider>().size = new Vector3(0.001f * (cajaTemp.transform.localScale.x / cajaTemp.transform.localScale.x)+0.0001f, 0.001f * (cajaTemp.transform.localScale.y / cajaTemp.transform.localScale.y) + 0.0001f, 0.001f * (cajaTemp.transform.localScale.z / cajaTemp.transform.localScale.z) + 0.0001f);
                cajaTemp.GetComponent<BoxCollider>().size = new Vector3(0.001f * (cajaTemp.transform.localScale.x / cajaTemp.transform.localScale.x), 0.001f * (cajaTemp.transform.localScale.y / cajaTemp.transform.localScale.y), 0.001f * (cajaTemp.transform.localScale.z / cajaTemp.transform.localScale.z));
                //cajaTemp.AddComponent<Rigidbody>();

                //cajaTemp.GetComponent<Rigidbody>().mass =(float) Cactual.Peso;
                //cajaTemp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
                cajaTemp.AddComponent<DragCaja>();
                cajaTemp.GetComponent<DragCaja>().mainCamera = camara;

                _cajaAntLinea = cajaAnt;
                contador += 1;
                // Recorremos columnas
                for (int j = 1; j < _cols; j++)
                {
                    posX = cajaAnt.x + _separacionAncho / 1000f + _ancho / 1000f;
                    posY = cajaAnt.y;
                    posZ = cajaAnt.z;
                    cajaAnt = new Vector3(posX, posY, posZ);
                    cajaTemp = Instantiate(patron, cajaAnt, Quaternion.identity);
                    cajaTemp.transform.name = "Caja" + (contador + 1).ToString();
                    cajaTemp.transform.parent = _objCapas.transform;

                    cajaTemp.AddComponent<BoxCollider>();
                    //cajaTemp.GetComponent<BoxCollider>().size = new Vector3(0.001f*(cajaTemp.transform.localScale.x/ cajaTemp.transform.localScale.x) + 0.0001f, 0.001f * (cajaTemp.transform.localScale.y / cajaTemp.transform.localScale.y) + 0.0001f, 0.001f * (cajaTemp.transform.localScale.z / cajaTemp.transform.localScale.z) + 0.0001f);
                    cajaTemp.GetComponent<BoxCollider>().size = new Vector3(0.001f * (cajaTemp.transform.localScale.x / cajaTemp.transform.localScale.x), 0.001f * (cajaTemp.transform.localScale.y / cajaTemp.transform.localScale.y), 0.001f * (cajaTemp.transform.localScale.z / cajaTemp.transform.localScale.z));
                    //cajaTemp.AddComponent<Rigidbody>();
                    //cajaTemp.GetComponent<Rigidbody>().mass = (float)Cactual.Peso;
                    //cajaTemp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
                    cajaTemp.AddComponent<DragCaja>();
                    cajaTemp.GetComponent<DragCaja>().mainCamera = camara;

                    contador += 1;
                }
            }

            return contador;
        }

        public void eventoBotonCapa(int capa)
        {
            for (int i = 0; i < BotonesCapa.Length; i++)
            {
                if (i == capa)
                {
                    //BotonesCapa[i].colors.normalColor = botonCapaSel; ty
                    BotonesCapa[i].transform.GetChild(1).gameObject.SetActive(true);
                    //Array.Resize(ref objCapas, i + 1);
                    //objCapas[i].gameObject.SetActive(true);

                }
                else
                {
                    BotonesCapa[i].transform.GetChild(1).gameObject.SetActive(false);
                   
                }
            }
            for (int i = 0; i < objCapas.Length; i++)
            {
                if (i <= capa)
                {
                    objCapas[i].gameObject.SetActive(true);
                }
                else
                {
                    objCapas[i].gameObject.SetActive(false);
                }
                
            }
            


        }
        public void cambiaNumeroCapas()
        {
            if (objCapas != null && objCapas.Length > 0)
            {
                int ncapas = Convert.ToInt32(NumeroCapas.text);
                if (ncapas < contadorCapas)
                {
                    int capasBorrar = contadorCapas - ncapas;
                    for (int i =0; i < capasBorrar; i++)
                    {
                        Destroy(objCapas[objCapas.Length-(1+i)]);
                    }
                    Array.Resize(ref objCapas, ncapas);
                }
                
                
                

                else if (ncapas > contadorCapas)
                {
                    Array.Resize(ref objCapas, ncapas);
                    for (int i = contadorCapas; i < ncapas; i++)
                    {
                        objCapas[i] = Instantiate(objCapas[i - 1], objCapas[i - 1].transform.position, Quaternion.identity);
                        objCapas[i].transform.name = "Capa" + (i + 1).ToString();
                        float posX, posY, posZ;
                        posX = objCapas[i - 1].transform.position.x;
                        posY = objCapas[i - 1].transform.position.y + (float)Cactual.Alto / 1000f;
                        posZ = objCapas[i - 1].transform.position.z;
                        objCapas[i].transform.position = new Vector3(posX, posY, posZ);
                        objCapas[i].transform.parent = objMosaico.transform;
                    }
                }
                
                contadorCapas = ncapas;
                for (int j = 0; j < BotonesCapa.Length; j++)
                {
                    if (j < contadorCapas)
                    {
                        BotonesCapa[j].gameObject.SetActive(true);
                        BotonesCapa[j].transform.GetChild(1).gameObject.SetActive(false);
                    }
                    else
                    {
                        BotonesCapa[j].gameObject.SetActive(false);
                        //BotonesCapa[i].transform.GetChild(1).gameObject.SetActive(false);
                    }
                }

            }
        }
        public void cambiaCamara()
        {
            if (camara.orthographic)
            {
                camara.transform.position = posCam3D.transform.position;
                camara.transform.rotation = posCam3D.transform.rotation;
                camara.transform.GetComponent<Camera>().orthographic = false;
                camara.transform.GetComponent<Camera>().fieldOfView = 35f;
            }
            else
            {
                camara.transform.position = posCam2D.transform.position;
                camara.transform.rotation = posCam2D.transform.rotation;
                camara.orthographic = true;
            }
        }
        public void fisica()
        {
            if (objCapas != null && objCapas.Length > 0)
            {
                for (int i = 0; i < objCapas.Length; i++)
                {
                    for (int j = 0; j < objCapas[i].transform.childCount; j++)
                    {
                        objCapas[i].transform.GetChild(j).gameObject.AddComponent<Rigidbody>();
                        objCapas[i].transform.GetChild(j).gameObject.GetComponent<Rigidbody>().mass= (float)Cactual.Peso;
                        objCapas[i].transform.GetChild(j).gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
                        //cajaTemp.GetComponent<Rigidbody>().mass = (float)Cactual.Peso;
                        //cajaTemp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
                    }
                }
            }
        }

    }
}
