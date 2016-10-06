using System.Messaging;
using IPC;

namespace FirstTestMessageQueueClient
{
    public class MessageQueueClient : IIpcClient
    {
        public void Send(string data)
        {
            var name = $".\\Private$\\{typeof(IIpcClient).Name}";

            var queue = MessageQueue.Exists(name) == true ? new MessageQueue(name) : MessageQueue.Create(name);

            using (queue)
            {
                queue.Send(data);
            }
        }
    }
}