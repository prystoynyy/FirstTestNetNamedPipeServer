using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTestNetNamedPipeClient
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var client = new WcfClient();
                Console.WriteLine("Enter text or 'exit':");
                var text = "";
                do
                {
                    Console.Write("->");
                    text = Console.ReadLine();
                    client.Send(text);
                } while (text != "exit");

                client.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: "+e.Message);
            }           
        }
    }
}
