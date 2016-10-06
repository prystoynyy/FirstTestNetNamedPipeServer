using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using IPC;

namespace FirstTestSocketsServer
{
    public sealed class SocketServer : IIpcServer
    {
        private readonly UdpClient server = new UdpClient(9000);

        void IDisposable.Dispose()
        {
            Stop();

            (server as IDisposable).Dispose();
        }

        public void Start()
        {
            Task.Factory.StartNew(() =>
            {
                var ip = new IPEndPoint(IPAddress.Any, 9000);

                while (true)
                {
                    var bytes = server.Receive(ref ip);
                    var data = Encoding.Default.GetString(bytes);
                    OnReceived(new DataReceivedEventArgs(data));
                }
            });
        }

        private void OnReceived(DataReceivedEventArgs e)
        {
            var handler = this.Received;

            handler?.Invoke(this, e);
        }

        public void Stop()
        {
            server.Close();
        }

        public event EventHandler<DataReceivedEventArgs> Received;
    }
}