using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAdministrationTool
{
    class Commands
    {
        private static Dictionary<string, string> commands = new Dictionary<string, string>()
        {
            {"cmds", "Shows a list of commands" },
            {"getreg", "Gets the registered users" },
            {"remusr [username]", "Removes a user" },
            {"addusr [username] [password]", "Adds a user" },
            {"change [username] [property] [value]", "Changes a property for the specified user" },
            {"getusr [username]", "Gets information of user" }
        };

        private static Dictionary<string, string> accProps = new Dictionary<string, string>()
        {
            {"name", "username" },
            {"pass", "password" }
        };

        public static void HandleCmd(string[] input)
        {
            string[] data;

            switch (input[0])
            {
                case "cmds":
                    Help();
                    break;
                case "getreg":
                    data = GetReg();
                    if (!bool.Parse(data[0]))
                    {
                        Console.WriteLine("error: " + data[1]);
                        return;
                    }

                    data = Parsing.RemoveNulls(Parsing.RemoveTilIndex(data, 1));

                    for (int i = 0; i < data.Length; i += 2)
                    {
                        string result = data[i] + " - " + data[i + 1];
                        Console.WriteLine(result);
                    }
                        
                    break;
                case "remusr":
                    data = RemoveUser(input[1]);
                    if (!bool.Parse(data[0]))
                    {
                        Console.WriteLine("error: " + data[1]);
                        return;
                    }
                    else
                        Console.WriteLine("Success!");
                    break;
                case "addusr":
                    data = AddUser(input[1], input[2]);
                    if (!bool.Parse(data[0]))
                    {
                        Console.WriteLine("error: " + data[1]);
                        return;
                    }
                    else
                        Console.WriteLine("Success!");
                    break;
                case "change":
                    Console.WriteLine("error: currently unavailable");
                    return;
                    data = ChangeProp(input[1], input[2], input[3]);
                    if (!bool.Parse(data[0]))
                    {
                        Console.WriteLine("error: " + data[1]);
                        return;
                    }
                    else
                        Console.WriteLine("Success!");
                    break;
                case "getusr":
                    data = GetUser(input[1]);
                    if (!bool.Parse(data[0]))
                    {
                        Console.WriteLine("error: " + data[1]);
                        return;
                    }
                    else
                    {
                        Console.WriteLine($"Account information for {data[1]}:");
                        foreach (var info in Parsing.RemoveTilIndex(data, 1))
                            Console.WriteLine(info);
                    }
                    break;
            }
        }

        public static void Help()
        {
            StringBuilder builder = new StringBuilder();

            foreach (var cmd in commands)
                builder.AppendLine(cmd.Key + " - " + cmd.Value);

            Console.WriteLine(builder);
        }

        public static string[] GetReg()
        {
            Server.SendData("getreg");

            return Server.Recieve();
        }

        public static string[] RemoveUser(string username)
        {
            Server.SendData("remusr\\" + username);

            return Parsing.RemoveNulls(Server.Recieve());
        }

        public static string[] AddUser(string username, string password)
        {
            Server.SendData($"addusr\\{username}\\{password}");

            return Parsing.RemoveNulls(Server.Recieve());
        }

        public static string[] ChangeProp(string username, string property, string value)
        {
            string translatedProp;
            if (accProps.TryGetValue(property, out translatedProp))
            {
                Server.SendData($"change\\{translatedProp}\\{value}");

                return Parsing.RemoveNulls(Server.Recieve());
            }
            else
                return null;
        }

        public static string[] GetUser(string username)
        {
            Server.SendData($"getusr\\{username}");

            return Parsing.RemoveNulls(Server.Recieve());
        }
    }
}
