using System;
using System.Messaging;
using System.Threading.Tasks;
using IPC;

namespace FirsttestMessageQueueServer
{
    public sealed class MessageQueueServer : IIpcServer
    {
        private MessageQueue queue;

        void IDisposable.Dispose()
        {
            this.Stop();

            this.queue.Dispose();
        }

        public void Start()
        {
            Task.Factory.StartNew(() =>
            {
                var name = string.Format(".\\Private$\\{0}", typeof(IIpcClient).Name);

                if (MessageQueue.Exists(name) == true)
                {
                    queue = new MessageQueue(name);
                }
                else
                {
                    queue = MessageQueue.Create(name);
                }

                queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

                while (true)
                {
                    var msg = queue.Receive();
                    var data = msg.Body.ToString();
                    this.OnReceived(new DataReceivedEventArgs(data));
                }
            });
        }

        private void OnReceived(DataReceivedEventArgs e)
        {
            var handler = this.Received;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void Stop()
        {
            this.queue.Close();
        }

        public event EventHandler<DataReceivedEventArgs> Received;
    }
}