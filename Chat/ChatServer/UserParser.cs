using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    enum UserProperty
    {
        Name,
        Password,
    }

    class UserParser
    {
        public static string[] GetUsers()
        {
            return File.ReadAllLines("users.txt");
        }

        public static string[] ParseUserInfo(string raw)
        {
            return raw.Split('\\');
        }

        public static bool Exists(string username)
        {
            foreach (var user in GetUsers())
            {
                string[] splitted = ParseUserInfo(user);

                if (username == splitted[0])
                    return true;
            }

            return false;
        }

        public static string FindUser(string username)
        {
            foreach (var user in GetUsers())
            {
                string[] splitted = ParseUserInfo(user);

                if (username == splitted[0])
                    return user;
            }

            return "False";
        }

        public static void RemoveUser(string username, string password)
        {
            foreach (var user in GetUsers())
            {
                string[] splitted = ParseUserInfo(user);

                if (username == splitted[0] && password == splitted[1])
                {
                    List<string> temp = splitted.ToList();

                    temp.Remove(user);

                    File.WriteAllLines("users.txt", temp.ToArray());
                    break;
                }
            }
        }

        public static void ChangeProperty(string username, string password, UserProperty property, string newProp)
        {
            string[] users = GetUsers();

            for (int i = 0; i < users.Length; i++)
            {
                string[] splitted = ParseUserInfo(users[i]);

                if (username == splitted[0] && password == splitted[1])
                {
                    splitted[(int)property] = newProp;

                    users[i] = splitted[0] + "\\" + splitted[1];
                }
            }

            File.WriteAllLines("users.txt", users);
        }
    }
}
