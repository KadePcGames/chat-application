﻿using System.Collections.Generic;
using System.Net.Sockets;

namespace ChatServer
{
    class Account
    {
        public static List<Account> registeredAccounts = new List<Account>();
        public static Account serverAcc = new Account();
        public string username { get; set; }
        public string password { get; set; }
        public NetworkStream stream { get; set; }
    }
}
