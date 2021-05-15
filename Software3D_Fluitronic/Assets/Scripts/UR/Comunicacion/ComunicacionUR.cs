using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Assets.Scripts.UR.Comunicacion
{
    public class ComunicacionUR 
    {
        public ParametrosUR ParametrosUR;
        // -------------------- Thread -------------------- //
        private Thread tcpip_read_Thread, tcpip_write_Thread;
        // -------------------- Socket -------------------- //
        private Socket socket_read = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private Socket socket_write = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        // -------------------- Float -------------------- //
        private float[] robotBaseRotLink_UR_before = { 0f, 0f, 0f, 0f, 0f, 0f };
        private float[] robotBaseRotLink_UR_aux = { 0f, 0f, 0f, 0f, 0f, 0f };
        // -------------------- Byte -------------------- //
        private byte[] packet_ur = new byte[746];
        private byte[] joint_1 = new byte[8]; private byte[] joint_2 = new byte[8]; private byte[] joint_3 = new byte[8];
        private byte[] joint_4 = new byte[8]; private byte[] joint_5 = new byte[8]; private byte[] joint_6 = new byte[8];
        private byte[] x_cartesian = new byte[8]; private byte[] y_cartesian = new byte[8];
        private byte[] z_cartesian = new byte[8]; private byte[] rx_cartesian = new byte[8];
        private byte[] ry_cartesian = new byte[8]; private byte[] rz_cartesian = new byte[8];
        // -------------------- Int -------------------- //
        private int byteCount_s;

        public ComunicacionUR()
        {
            ParametrosUR = new ParametrosUR();
        }
        public void conectar()
        {
            
            ParametrosUR.ur_dt_enable_tcpip_read = true;
            ParametrosUR.ur_dt_enable_tcpip_write = true;
            

            tcpip_write_Thread = new Thread(() => TCPip_write_thread_function(ParametrosUR.ur_tcpip_write_config_str, ParametrosUR.ur_tcpip_write_config_int));
            tcpip_write_Thread.IsBackground = true;
            tcpip_write_Thread.Start();

            tcpip_read_Thread = new Thread(() => TCPip_read_thread_function(ParametrosUR.ur_tcpip_read_config_str, ParametrosUR.ur_tcpip_read_config_int));
            tcpip_read_Thread.IsBackground = true;
            tcpip_read_Thread.Start();


        }
    

        void TCPip_write_thread_function(string ip_adr, int port_adr)
        {
            if (ParametrosUR.ur_dt_enable_tcpip_write)
            {
                // Initialization TCP/IP Communication
                IPAddress ip = IPAddress.Parse(ip_adr);
                IPEndPoint ip_end = new IPEndPoint(ip, port_adr);

                if (!socket_write.Connected)
                {
                    // connect to controller -> if the controller is disconnected
                    socket_write.Connect(ip_end);
                }
            }

            // Threading while {write command}
            while (ParametrosUR.ur_dt_enable_tcpip_write)
            {
                if (ParametrosUR.Aux_command_str != "")
                {
                    // send command (byte) -> speed control of the robot (X,Y,Z and EA{RX, RY, RZ})
                    ParametrosUR.Command = new UTF8Encoding().GetBytes(ParametrosUR.Aux_command_str);
                    socket_write.Send(ParametrosUR.Command);
                    ParametrosUR.Aux_command_str = "";
                }
                // Thread sleep (2 ms)
                Thread.Sleep(2);
            }
            // disconnect tcp/ip communication (read write) after stopping the threading
            socket_write.Disconnect(true);
        }
        

        void TCPip_read_thread_function(string ip_adr, int port_adr)
        {
            if (ParametrosUR.ur_dt_enable_tcpip_read == true)
            {
                // Initialization TCP/IP Communication
                IPAddress ip = IPAddress.Parse(ip_adr);
                IPEndPoint ip_end = new IPEndPoint(ip, port_adr);

                if (!socket_read.Connected)
                {
                    // connect to controller -> if the controller is disconnected
                    socket_read.Connect(ip_end);
                }
            }
            

            // Threading while {read date}
            while (ParametrosUR.ur_dt_enable_tcpip_read && socket_read.Connected)
            {
                // Receive data from UR packet - Maximum {TCP/IP} -> 749
                byteCount_s = socket_read.Receive(packet_ur);

                // Check Length
                if (byteCount_s == packet_ur.Length)
                {
                    // -------------------- JOINT {Read} -------------------- //
                    // Joint 1
                    joint_1[0] = packet_ur[259]; joint_1[1] = packet_ur[258]; joint_1[2] = packet_ur[257];
                    joint_1[3] = packet_ur[256]; joint_1[4] = packet_ur[255]; joint_1[5] = packet_ur[254];
                    joint_1[6] = packet_ur[253]; joint_1[7] = packet_ur[252];
                    // Joint 2
                    joint_2[0] = packet_ur[267]; joint_2[1] = packet_ur[266]; joint_2[2] = packet_ur[265];
                    joint_2[3] = packet_ur[264]; joint_2[4] = packet_ur[263]; joint_2[5] = packet_ur[262];
                    joint_2[6] = packet_ur[261]; joint_2[7] = packet_ur[260];
                    // Joint 3
                    joint_3[0] = packet_ur[275]; joint_3[1] = packet_ur[274]; joint_3[2] = packet_ur[273];
                    joint_3[3] = packet_ur[272]; joint_3[4] = packet_ur[271]; joint_3[5] = packet_ur[270];
                    joint_3[6] = packet_ur[269]; joint_3[7] = packet_ur[268];
                    // Joint 4
                    joint_4[0] = packet_ur[283]; joint_4[1] = packet_ur[282]; joint_4[2] = packet_ur[281];
                    joint_4[3] = packet_ur[280]; joint_4[4] = packet_ur[279]; joint_4[5] = packet_ur[278];
                    joint_4[6] = packet_ur[277]; joint_4[7] = packet_ur[276];
                    // Joint 5
                    joint_5[0] = packet_ur[291]; joint_5[1] = packet_ur[290]; joint_5[2] = packet_ur[289];
                    joint_5[3] = packet_ur[288]; joint_5[4] = packet_ur[287]; joint_5[5] = packet_ur[286];
                    joint_5[6] = packet_ur[285]; joint_5[7] = packet_ur[284];
                    // Joint 6
                    joint_6[0] = packet_ur[299]; joint_6[1] = packet_ur[298]; joint_6[2] = packet_ur[297];
                    joint_6[3] = packet_ur[296]; joint_6[4] = packet_ur[295]; joint_6[5] = packet_ur[294];
                    joint_6[6] = packet_ur[293]; joint_6[7] = packet_ur[292];

                    // data editing for reading
                    robotBaseRotLink_UR_aux[0] = (float)Math.Round(BitConverter.ToDouble(joint_1, 0) * (180 / Math.PI), 2);
                    robotBaseRotLink_UR_aux[1] = (float)Math.Round(BitConverter.ToDouble(joint_2, 0) * (180 / Math.PI), 2);
                    robotBaseRotLink_UR_aux[2] = (float)Math.Round(BitConverter.ToDouble(joint_3, 0) * (180 / Math.PI), 2);
                    robotBaseRotLink_UR_aux[3] = (float)Math.Round(BitConverter.ToDouble(joint_4, 0) * (180 / Math.PI), 2);
                    robotBaseRotLink_UR_aux[4] = (float)Math.Round(BitConverter.ToDouble(joint_5, 0) * (180 / Math.PI), 2);
                    robotBaseRotLink_UR_aux[5] = (float)Math.Round(BitConverter.ToDouble(joint_6, 0) * (180 / Math.PI), 2);

                    if ((robotBaseRotLink_UR_aux[0] != 0) && (robotBaseRotLink_UR_aux[1] != 0) &&
                        (robotBaseRotLink_UR_aux[2] != 0) && (robotBaseRotLink_UR_aux[3] != 0) &&
                        (robotBaseRotLink_UR_aux[4] != 0) && (robotBaseRotLink_UR_aux[5] != 0))
                    {
                        ParametrosUR.RobotBaseRotLink_UR_j[0] = (float)Math.Round(BitConverter.ToDouble(joint_1, 0) * (180 / Math.PI), 2);
                        ParametrosUR.RobotBaseRotLink_UR_j[1] = (float)Math.Round(BitConverter.ToDouble(joint_2, 0) * (180 / Math.PI), 2);
                        ParametrosUR.RobotBaseRotLink_UR_j[2] = (float)Math.Round(BitConverter.ToDouble(joint_3, 0) * (180 / Math.PI), 2);
                        ParametrosUR.RobotBaseRotLink_UR_j[3] = (float)Math.Round(BitConverter.ToDouble(joint_4, 0) * (180 / Math.PI), 2);
                        ParametrosUR.RobotBaseRotLink_UR_j[4] = (float)Math.Round(BitConverter.ToDouble(joint_5, 0) * (180 / Math.PI), 2);
                        ParametrosUR.RobotBaseRotLink_UR_j[5] = (float)Math.Round(BitConverter.ToDouble(joint_6, 0) * (180 / Math.PI), 2);

                        robotBaseRotLink_UR_before[0] = ParametrosUR.RobotBaseRotLink_UR_j[0];
                        robotBaseRotLink_UR_before[1] = ParametrosUR.RobotBaseRotLink_UR_j[1];
                        robotBaseRotLink_UR_before[2] = ParametrosUR.RobotBaseRotLink_UR_j[2];
                        robotBaseRotLink_UR_before[3] = ParametrosUR.RobotBaseRotLink_UR_j[3];
                        robotBaseRotLink_UR_before[4] = ParametrosUR.RobotBaseRotLink_UR_j[4];
                        robotBaseRotLink_UR_before[5] = ParametrosUR.RobotBaseRotLink_UR_j[5];
                    }
                    else
                    {
                        ParametrosUR.RobotBaseRotLink_UR_j[0] = robotBaseRotLink_UR_before[0];
                        ParametrosUR.RobotBaseRotLink_UR_j[1] = robotBaseRotLink_UR_before[1];
                        ParametrosUR.RobotBaseRotLink_UR_j[2] = robotBaseRotLink_UR_before[2];
                        ParametrosUR.RobotBaseRotLink_UR_j[3] = robotBaseRotLink_UR_before[3];
                        ParametrosUR.RobotBaseRotLink_UR_j[4] = robotBaseRotLink_UR_before[4];
                        ParametrosUR.RobotBaseRotLink_UR_j[5] = robotBaseRotLink_UR_before[5];
                    }

                    // -------------------- CARTESIAN {Read} -------------------- //
                    // X
                    x_cartesian[0] = packet_ur[451]; x_cartesian[1] = packet_ur[450]; x_cartesian[2] = packet_ur[449];
                    x_cartesian[3] = packet_ur[448]; x_cartesian[4] = packet_ur[447]; x_cartesian[5] = packet_ur[446];
                    x_cartesian[6] = packet_ur[445]; x_cartesian[7] = packet_ur[444];
                    // Y
                    y_cartesian[0] = packet_ur[459]; y_cartesian[1] = packet_ur[458]; y_cartesian[2] = packet_ur[457];
                    y_cartesian[3] = packet_ur[456]; y_cartesian[4] = packet_ur[455]; y_cartesian[5] = packet_ur[454];
                    y_cartesian[6] = packet_ur[453]; y_cartesian[7] = packet_ur[452];
                    // Z
                    z_cartesian[0] = packet_ur[467]; z_cartesian[1] = packet_ur[466]; z_cartesian[2] = packet_ur[465];
                    z_cartesian[3] = packet_ur[464]; z_cartesian[4] = packet_ur[463]; z_cartesian[5] = packet_ur[462];
                    z_cartesian[6] = packet_ur[461]; z_cartesian[7] = packet_ur[460];
                    // RX
                    rx_cartesian[0] = packet_ur[475]; rx_cartesian[1] = packet_ur[474]; rx_cartesian[2] = packet_ur[473];
                    rx_cartesian[3] = packet_ur[472]; rx_cartesian[4] = packet_ur[471]; rx_cartesian[5] = packet_ur[470];
                    rx_cartesian[6] = packet_ur[469]; rx_cartesian[7] = packet_ur[468];
                    // RY
                    ry_cartesian[0] = packet_ur[483]; ry_cartesian[1] = packet_ur[482]; ry_cartesian[2] = packet_ur[481];
                    ry_cartesian[3] = packet_ur[480]; ry_cartesian[4] = packet_ur[479]; ry_cartesian[5] = packet_ur[478];
                    ry_cartesian[6] = packet_ur[477]; ry_cartesian[7] = packet_ur[476];
                    // RZ
                    rz_cartesian[0] = packet_ur[491]; rz_cartesian[1] = packet_ur[490]; rz_cartesian[2] = packet_ur[489];
                    rz_cartesian[3] = packet_ur[488]; rz_cartesian[4] = packet_ur[487]; rz_cartesian[5] = packet_ur[486];
                    rz_cartesian[6] = packet_ur[485]; rz_cartesian[7] = packet_ur[484];

                    // data editing for reading
                    ParametrosUR.RobotBaseRotLink_UR_c[0] = Math.Round(BitConverter.ToDouble(x_cartesian, 0) * (1000), 2);
                    ParametrosUR.RobotBaseRotLink_UR_c[1] = Math.Round(BitConverter.ToDouble(y_cartesian, 0) * (1000), 2);
                    ParametrosUR.RobotBaseRotLink_UR_c[2] = Math.Round(BitConverter.ToDouble(z_cartesian, 0) * (1000), 2);
                    ParametrosUR.RobotBaseRotLink_UR_rad[0] = Math.Round(BitConverter.ToDouble(rx_cartesian, 0),3);
                    ParametrosUR.RobotBaseRotLink_UR_rad[1] = Math.Round(BitConverter.ToDouble(ry_cartesian, 0), 3);
                    ParametrosUR.RobotBaseRotLink_UR_rad[2] = Math.Round(BitConverter.ToDouble(rz_cartesian, 0), 3);
                    ParametrosUR.RobotBaseRotLink_UR_c[3] = ParametrosUR.RobotBaseRotLink_UR_rad[0] * (180 / Math.PI);
                    ParametrosUR.RobotBaseRotLink_UR_c[4] = ParametrosUR.RobotBaseRotLink_UR_rad[1] * (180 / Math.PI);
                    ParametrosUR.RobotBaseRotLink_UR_c[5] = ParametrosUR.RobotBaseRotLink_UR_rad[2] * (180 / Math.PI);

                }
            }
            // disconnect tcp/ip communication (read data) after stopping the threading
            socket_read.Disconnect(true);
        }

        public void desconectar()
        {
            try
            {
                // Control -> Stop {Read TCP/IP data}
                ParametrosUR.ur_dt_enable_tcpip_read = false;
                // Control -> Stop {Write TCP/IP data}
                ParametrosUR.ur_dt_enable_tcpip_write = false;

                // Abort threading block {TCP/Ip -> read data}
                if (tcpip_read_Thread.IsAlive == true)
                {
                    tcpip_read_Thread.Abort();
                }

                // Abort threading block {TCP/Ip  -> write data}
                if (tcpip_write_Thread.IsAlive == true)
                {
                    tcpip_write_Thread.Abort();
                }

                // Socket Read {shutdown -> both (Receive, Send)}
                socket_read.Shutdown(SocketShutdown.Both);
                // Socket Write {shutdown -> both (Receive, Send)}
                socket_write.Shutdown(SocketShutdown.Both);
            }
            catch (Exception e)
            {
                // Destroy all
                //Destroy(this);
                
            }
            finally
            {
                // Socket Read {close}
                socket_read.Close();
                // Socket Write {close}
                socket_write.Close();

                // Destroy all
                //Destroy(this);
            }
        }
    }

   
}
