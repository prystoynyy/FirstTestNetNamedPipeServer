using System.Net.Sockets;
using System.Text;
using IPC;

namespace FirstTEstSocketsClient
{
    public class SocketClient : IIpcClient
    {
        public void Send(string data)
        {
            using (var client = new UdpClient())
            {
                client.Connect("localhost", 9000);

                var bytes = Encoding.Default.GetBytes(data);

                client.Send(bytes, bytes.Length);
            }
        }
    }
}