using UnityEngine;

namespace Assets.Scripts.UR.Comunicacion
{
    public class ControlUR : MonoBehaviour
    {
        public string IP { get; set; }
        public int Puerto { get; set; }
        public double Posicion_X { get; set; }
        public double Posicion_Y { get; set; }
        public double Posicion_Z { get; set; }
        public double Posicion_RX { get; set; }
        public double Posicion_RY { get; set; }
        public double Posicion_RZ { get; set; }
        public double Posicion_RX_rad { get; set; }
        public double Posicion_RY_rad { get; set; }
        public double Posicion_RZ_rad { get; set; }
        public double Posicion_J1 { get; set; }
        public double Posicion_J2 { get; set; }
        public double Posicion_J3 { get; set; }
        public double Posicion_J4 { get; set; }
        public double Posicion_J5 { get; set; }
        public double Posicion_J6 { get; set; }

        //private UTF8Encoding utf8 = new UTF8Encoding();

        public ComunicacionUR ComunicacionUR;

        private void Start()
        {
            ComunicacionUR = new ComunicacionUR();
            // Position {Cartesian} -> X..Z
            Posicion_X = 0.0;
            Posicion_Y = 0.0;
            Posicion_Z = 0.0;
            // Position {Rotation} -> EulerAngles(RX..RZ)
            Posicion_RX = 0.0;
            Posicion_RY = 0.0;
            Posicion_RZ = 0.0;
            Posicion_RX_rad = 0.0;
            Posicion_RY_rad = 0.0;
            Posicion_RZ_rad = 0.0;
            // Position Joint -> 1 - 6
            Posicion_J1 = 0.0;
            Posicion_J2 = 0.0;
            Posicion_J3 = 0.0;
            Posicion_J4 = 0.0;
            Posicion_J5 = 0.0;
            Posicion_J6 = 0.0;

            // Robot IP Address
            IP= "192.168.0.2";
            Puerto = 30003;

            
        }

        private void Update()
        {
            /*
            // Robot IP Address (Read) -> TCP/IP 
            ComunicacionUR.ParametrosUR.ur_tcpip_read_config_str = IP;
            ComunicacionUR.ParametrosUR.ur_tcpip_read_config_int = Puerto;
            // Robot IP Address (Write) -> TCP/IP 
            ComunicacionUR.ParametrosUR.ur_tcpip_write_config_str = IP;
            ComunicacionUR.ParametrosUR.ur_tcpip_write_config_int = Puerto;
            */
            // ------------------------ Connection Information ------------------------//
            // If the button (connect/disconnect) is pressed, change the color and text
            if (ComunicacionUR!=null && ComunicacionUR.ParametrosUR.connect)
            {
                // De momento nada
            }
            else if (ComunicacionUR!=null && ComunicacionUR.ParametrosUR.disconnect)
            {
               // De momento nada
            }

            if (ComunicacionUR != null)
            {
                // ------------------------ Cyclic read parameters {diagnostic panel} ------------------------ //
                // Position {Cartesian} -> X..Z
                Posicion_X = ComunicacionUR.ParametrosUR.RobotBaseRotLink_UR_c[0];
                Posicion_Y = ComunicacionUR.ParametrosUR.RobotBaseRotLink_UR_c[1];
                Posicion_Z = ComunicacionUR.ParametrosUR.RobotBaseRotLink_UR_c[2];
                // Position {Rotation} -> EulerAngles(RX..RZ)
                Posicion_RX = ComunicacionUR.ParametrosUR.RobotBaseRotLink_UR_c[3];
                Posicion_RY = ComunicacionUR.ParametrosUR.RobotBaseRotLink_UR_c[4];
                Posicion_RZ = ComunicacionUR.ParametrosUR.RobotBaseRotLink_UR_c[5];
                Posicion_RX_rad = ComunicacionUR.ParametrosUR.RobotBaseRotLink_UR_rad[0];
                Posicion_RY_rad = ComunicacionUR.ParametrosUR.RobotBaseRotLink_UR_rad[1];
                Posicion_RZ_rad = ComunicacionUR.ParametrosUR.RobotBaseRotLink_UR_rad[2];
                // Position Joint -> 1 - 6
                Posicion_J1 = ComunicacionUR.ParametrosUR.RobotBaseRotLink_UR_j[0];
                Posicion_J2 = ComunicacionUR.ParametrosUR.RobotBaseRotLink_UR_j[1];
                Posicion_J3 = ComunicacionUR.ParametrosUR.RobotBaseRotLink_UR_j[2];
                Posicion_J4 = ComunicacionUR.ParametrosUR.RobotBaseRotLink_UR_j[3];
                Posicion_J5 = ComunicacionUR.ParametrosUR.RobotBaseRotLink_UR_j[4];
                Posicion_J6 = ComunicacionUR.ParametrosUR.RobotBaseRotLink_UR_j[5];
            }
        }

        private void OnApplicationQuit()
        {
            Desconectar();
        }

        private void OnDestroy()
        {
            Desconectar();
        }

        private void OnDisable()
        {
            Desconectar();
        }

        public void Conectar()
        {
            //ComunicacionUR = new ComunicacionUR();
            // Robot IP Address (Read) -> TCP/IP 
            ComunicacionUR.ParametrosUR.ur_tcpip_read_config_str = IP;
            ComunicacionUR.ParametrosUR.ur_tcpip_read_config_int = Puerto;
            // Robot IP Address (Write) -> TCP/IP 
            ComunicacionUR.ParametrosUR.ur_tcpip_write_config_str = IP;
            ComunicacionUR.ParametrosUR.ur_tcpip_write_config_int = Puerto;
            ComunicacionUR.conectar();
            
            // Auxiliary first command -> Write initialization position/rotation with acceleration/time to the robot controller
            // command (string value)
            //Escribir("speedl([0.0,0.0,0.0,0.0,0.0,0.0], a = 0.15, t = 0.03)");
            //ComunicacionUR.ParametrosUR.Aux_command_str = "speedl([0.0,0.0,0.0,0.0,0.0,0.0], a = 0.15, t = 0.03)" + "\n";
            // get bytes from string command
            //ComunicacionUR.ParametrosUR.Command = utf8.GetBytes(ComunicacionUR.ParametrosUR.Aux_command_str);

        }

        public void Escribir(string cmd)
        {
            if (ComunicacionUR != null)
            {
                string cmd2 = cmd + "\n";
                //ComunicacionUR.Escribir(cmd2);
                ComunicacionUR.ParametrosUR.Aux_command_str = cmd+"\n";
            }
        }
        public void Desconectar()
        {
            if (ComunicacionUR != null)
            {
                ComunicacionUR.desconectar();
            }
            Destroy(this);
        }
    }
}
