using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPC;

namespace FirstTestNamedPipeClient
{
    public class NamedPipeClient : IIpcClient
    {
        public void Send(string data)
        {
            using (var client = new NamedPipeClientStream(".", typeof(IIpcClient).Name, PipeDirection.Out))
            {
                client.Connect();

                using (var writer = new StreamWriter(client))
                {
                    writer.WriteLine(data);
                }
            }
        }
    }
}
