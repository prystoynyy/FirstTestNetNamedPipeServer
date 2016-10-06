using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPC;

namespace FirstTestNamedPipeServer
{
    public sealed class NamedPipeServer : IIpcServer
    {
        private readonly NamedPipeServerStream server = new NamedPipeServerStream(typeof(IIpcClient).Name, PipeDirection.In);

        private void OnReceived(DataReceivedEventArgs e)
        {
            var handler = this.Received;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<DataReceivedEventArgs> Received;

        public void Start()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    this.server.WaitForConnection();

                    try
                    {
                        var reader = new StreamReader(this.server);
                        this.OnReceived(new DataReceivedEventArgs(reader.ReadToEnd()));
                        this.server.Disconnect();
                    }
                    finally {}
                }
            });
        }

        public void Stop()
        {
            this.server.Disconnect();
        }

        void IDisposable.Dispose()
        {
            this.Stop();

            this.server.Dispose();
        }
    }
}
