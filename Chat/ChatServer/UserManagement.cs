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

    class UserManagement
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
            return FindUser(username) != "False";
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

        public static bool RemoveUser(string username)
        {
            List<string> temp = GetUsers().ToList();

            int index = temp.IndexOf(FindUser(username));

            if (index == -1)
                return false;

            temp.RemoveAt(index);

            File.WriteAllLines("users.txt", temp.ToArray());
            return true;
        }

        public static void AddUser(string username, string password)
        {
            List<string> temp = GetUsers().ToList();

            temp.Add(username + "\\" + password);

            File.WriteAllLines("users.txt", temp.ToArray());
        }

        public static void ChangeProperty(string username, UserProperty property, string newProp)
        {
            List<string> users = GetUsers().ToList();

            int index = users.FindIndex(us => us.Split('\\')[0] == username);

            string[] splitted = ParseUserInfo(users[index]);

            splitted[(int)property] = newProp;

            users[index] = splitted[0] + "\\" + splitted[1];
            
            File.WriteAllLines("users.txt", users);
        }
    }
}
