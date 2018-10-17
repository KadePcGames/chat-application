using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Linq;
using System;

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

            MessageHandler(stream);
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
                }

                Thread.Sleep(500);
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
            else if (data[0] == "administration")
            {
                newUser.username = "administration";
                newUser.password = "idksomepassnoonereallygivesafuckdontleaktho";

                if (!Account.CheckAcc(newUser, data[0], data[1]))
                {
                    SendData(stream, "False\\Incorrect admin password");
                    return;
                }

                switch (data[2])
                {
                    case "ping":
                        SendData(stream, "True");
                        break;

                    case "getreg":
                        StringBuilder builder = new StringBuilder();
                        foreach (var regUser in File.ReadAllLines("users.txt"))
                            builder.Append("\\" + regUser);

                        SendData(stream, "True" + builder.ToString());
                        break;

                    case "remusr":
                        string[] users = UserParser.GetUsers();

                        foreach (var user in  users)
                        {
                            string[] splitted = UserParser.ParseUserInfo(user);

                            if (splitted[0] == data[3])
                            {
                                List<string> temp = users.ToList();

                                temp.Remove(user);

                                File.WriteAllLines("users.txt", temp.ToArray());

                                SendData(stream, "True");
                            }
                        }
                        break;
                    case "addusr":
                        StreamWriter writer = File.AppendText("users.txt");
                        writer.WriteLine(data[3] + "\\" + data[4]);
                        writer.Close();

                        SendData(stream, "True");
                        break;
                    case "getusr":
                        if (!UserParser.Exists(data[3]))
                        {
                            SendData(stream, "User does not exist");
                            return;
                        }

                        string userInfo = UserParser.FindUser(data[3]);

                        SendData(stream, "True\\" + userInfo);
                        break;
                    default:
                        SendData(stream, "False\\Command does not exist");
                        break;
                }

                return;
            }
            else
            {
                string errorCode = "Incorrect username or password";
                bool result = false;

                foreach (var registered in Account.registeredAccounts)
                {
                    if (Account.CheckAcc(registered, data[1], data[2]))
                    {
                        if (onlineUsers.Exists(on => on.username == registered.username))
                        {
                            errorCode = "User is already logged in";
                            break;
                        }
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
                    SendData(stream, "False\\" + errorCode);
                    return;
                }

                while (true)
                {
                    data = Recieve(stream);

                    switch (data[0])
                    {
                        case "msg":
                            if (!Account.CheckAcc(newUser, data[1], data[2]))
                                return;

                            MsgInfo info = new MsgInfo();

                            info.user = newUser;
                            info.message = data[3];

                            msgQueue.Add(info);
                            break;
                     
                        case "exit":
                            if (!Account.CheckAcc(newUser, data[1], data[2]))
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
