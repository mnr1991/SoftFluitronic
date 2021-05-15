using System.Numerics;

namespace Assets.Paletizador.Script.Clases
{
    public class Caja
    {
        public double Ancho { get; set; }
        public double Largo { get; set; }
        public double Alto { get; set; }
        public double Peso { get; set; }
        public int NumeroCaja { get; set; }
        public string Descripcion { get; set; }
        public double x_ur;
        public double y_ur;
        public double z_ur;
        public double rx_ur;
        public double ry_ur;
        public double rz_ur;
        public int capa;
        public Vector3 posUnity;
    }
}