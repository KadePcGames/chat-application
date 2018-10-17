using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace ChatAdministrationTool
{
    class Server
    {
        public static string adminPass = "idksomepassnoonereallygivesafuckdontleaktho";

        public static NetworkStream stream { get; set; }

        public static bool SendData(string data)
        {
            if (!Connect())
            {
                Console.WriteLine("error: unable to reach the server");
                return false;
            }

            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes($"administration\\{adminPass}\\" + data + "\\");

                stream.Write(bytes, 0, bytes.Length);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string[] Recieve()
        {
            try
            {
                byte[] bytes = new byte[1024];

                stream.Read(bytes, 0, bytes.Length);

                string[] data = Encoding.UTF8.GetString(bytes).Split('\\');

                stream.Flush();

                return data;
            }
            catch (Exception)
            {
                return new string[] { "failed to recieve message" };
            }
        }

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
    }
}
