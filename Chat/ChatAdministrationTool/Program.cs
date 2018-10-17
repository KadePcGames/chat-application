using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ChatAdministrationTool
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "CAT";
            Console.WriteLine("Welcome to CAT! (Chat Administration Tool)");
            Console.Write("Attempting to connect to the server... ");

            bool isWorked = Server.SendData("ping");

            if (!isWorked)
            {
                Thread.Sleep(3000);
                return;
            }

            string[] response = Server.Recieve();

            if (!bool.Parse(response[0]))
            {
                Console.WriteLine("Failed, error: " + response[1]);
                Thread.Sleep(3000);
                return;
            }

            Console.WriteLine("Succeded!");

            while (true)
            {
                Console.Write("input~# ");
                string[] input = Console.ReadLine().Split(' ');

                Commands.HandleCmd(input);
            }
        }
    }
}
