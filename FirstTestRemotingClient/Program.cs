﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTestRemotingClient
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var client = new RemotingClient();
                Console.WriteLine("Enter text or 'exit':");
                var text = "";
                do
                {
                    Console.Write("->");
                    text = Console.ReadLine();
                    client.Send(text);
                } while (text != "exit");

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}
