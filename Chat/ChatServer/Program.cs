using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace ChatServer
{
    class Program
    {
        private static List<Account> onlineUsers = new List<Account>();
        private static List<MsgInfo> msgQueue = new List<MsgInfo>();

        static void Main(string[] args)
        {
            foreach (var line in File.ReadAllLines("users.txt"))
            {
                string[] info = line.Split('\\');
                Account acc = new Account();
                acc.username = info[0];
                acc.password = info[1];

                Account.registeredAccounts.Add(acc);
            }

            Account.serverAcc.username = "[SERVER]";
            Account.serverAcc.password = "ryiujhroijhroithgjieuthg9i8tyhg4958yuhgjietuh8r5ehgie4rthg3945thy40t9gbet9u8rhg9856hj694twyho95huo96ty";
            
            TcpListener listener = new TcpListener(IPAddress.Any, 6969);
            listener.Start();

            Task.Run(() => SendQueuedMessages());

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();

                Task.Run(() => Handle(client));
            }
        }

        private static void Handle(TcpClient client)
        {
            NetworkStream stream = client.GetStream();

            Task.Run(() => MessageHandler(stream));
        }

        private static void SendQueuedMessages()
        {
            while (true)
            {
                if (msgQueue.Count > 0)
                {
                    string msg = msgQueue[0].user.username + " - " + msgQueue[0].message;

                    foreach (Account acc in onlineUsers)
                    {
                        if (acc.stream != null)
                            SendData(acc.stream, msg);
                    }
                    
                    msgQueue.RemoveAt(0);

                    Thread.Sleep(100);
                }

                Thread.Sleep(1000);
            }
        }

        private static void MessageHandler(NetworkStream stream)
        {
            string[] data;
            Account newUser = new Account();

            data = Recieve(stream);

            if (data[0] == "reg")
            {
                newUser.username = data[1];
                newUser.password = data[2];

                Account.registeredAccounts.Add(newUser);

                StreamWriter writer = File.AppendText("users.txt");
                writer.WriteLine(data[1] + "\\" + data[2]);

                writer.Close();
            }
            else
            {
                bool result = false;

                foreach (var registered in Account.registeredAccounts)
                {
                    if (CheckAcc(registered, data[1], data[2]))
                    {
                        SendData(stream, "True");

                        newUser.username = data[1];
                        newUser.password = data[2];
                        newUser.stream = stream;

                        onlineUsers.Add(newUser);

                        result = true;

                        SendServerMsg($"{newUser.username} has connected");

                        break;
                    }
                }

                if (!result)
                {
                    SendData(stream, "False");
                    return;
                }

                while (true)
                {
                    data = Recieve(stream);

                    switch (data[0])
                    {
                        case "msg":
                            if (!CheckAcc(newUser, data[1], data[2]))
                                return;

                            MsgInfo info = new MsgInfo();

                            info.user = newUser;
                            info.message = data[3];

                            msgQueue.Add(info);
                            break;
                        case "exit":
                            if (!CheckAcc(newUser, data[1], data[2]))
                                return;

                            foreach (var user in onlineUsers)
                            {
                                if (user.username == data[1])
                                {
                                    onlineUsers.Remove(user);

                                    SendServerMsg($"{user.username} has disconnected");
                                }
                            }

                            break;
                    }
                }
            }
        }

        private static void SendServerMsg(string data)
        {
            MsgInfo info = new MsgInfo();
            info.user = Account.serverAcc;
            info.message = data;

            msgQueue.Add(info);
        }

        private static bool CheckAcc(Account acc, string username, string password)
        {
            return acc.username == username && acc.password == password;
        }

        private static void SendData(NetworkStream stream, string data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data + "\\");

            stream.Write(bytes, 0, bytes.Length);
        }

        private static string[] Recieve(NetworkStream stream)
        {
            byte[] bytes = new byte[1024];

            stream.Read(bytes, 0, bytes.Length);

            string[] data = Encoding.UTF8.GetString(bytes).Split('\\');

            stream.Flush();

            return data;
        }
    }
}