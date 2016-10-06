using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTestSocketsServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var server = new SocketServer();
                server.Received += (sender, eventArgs) =>
                {
                    Console.WriteLine("Data from client: " + eventArgs.Data + "| " + DateTime.Now);
                };
                server.Start();
                Console.WriteLine("Server was started");
                Console.WriteLine("Press enter for close");
                Console.ReadLine();
                server.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                Console.ReadLine();
            }
        }
    }
}
