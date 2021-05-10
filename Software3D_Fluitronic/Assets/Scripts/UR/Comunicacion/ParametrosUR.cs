namespace Assets.Scripts.UR.Comunicacion
{
    public class ParametrosUR
    {
        // -------------------- Bool -------------------- //
        public  bool ur_dt_enable_tcpip_read { get; set; }
        public  bool ur_dt_enable_tcpip_write { get; set; }
        public  bool connect { get; set; }
        public  bool disconnect { get; set; }
        
        // -------------------- String -------------------- //
        public  string ur_tcpip_read_config_str { get; set; }
        public  int ur_tcpip_read_config_int { get; set; }
        public  string ur_tcpip_write_config_str { get; set; } 
        public  int ur_tcpip_write_config_int { get; set; }

        // -------------------- Float -------------------- //
        private  float[] robotBaseRotLink_UR_j = { 0f, 0f, 0f, 0f, 0f, 0f };
        // -------------------- Double -------------------- //
        private  double[] robotBaseRotLink_UR_c = { 0f, 0f, 0f, 0f, 0f, 0f };
        private double[] robotBaseRotLink_UR_rad = { 0f, 0f, 0f};
        // -------------------- String -------------------- //
        private  string aux_command_str="";
        // -------------------- Byte -------------------- //
        private  byte[] command;

        // -------------------- Propiedades -------------------- //
        public  float[] RobotBaseRotLink_UR_j { get=> robotBaseRotLink_UR_j; set=> robotBaseRotLink_UR_j=value; }
        public  double[] RobotBaseRotLink_UR_c { get => robotBaseRotLink_UR_c; set => robotBaseRotLink_UR_c = value; }
        public double[] RobotBaseRotLink_UR_rad { get => robotBaseRotLink_UR_rad; set => robotBaseRotLink_UR_rad = value; }
        public  string Aux_command_str { get => aux_command_str; set => aux_command_str = value; }
        public  byte[] Command { get => command; set => command = value; }


    }
}
