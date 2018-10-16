using System;
using System.Net.Sockets;
using System.Text;

namespace ChatClient
{
    class Chat
    {
        public static NetworkStream stream { get; set; }
        public static string username { get; set; }
        public static string password { get; set; }

        public static bool Connect()
        {
            try
            {
                TcpClient client = new TcpClient("127.0.0.1", 6969);

                stream = client.GetStream();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool SendData(string data)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(data + "\\");

                stream.Write(bytes, 0, bytes.Length);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string Recieve()
        {
            try
            {
                byte[] bytes = new byte[1024];

                stream.Read(bytes, 0, bytes.Length);

                string data = Encoding.UTF8.GetString(bytes).Split('\\')[0];

                stream.Flush();

                return data;
            }
            catch (Exception)
            {
                return "failed to recieve message";
            }
        }
    }
}
