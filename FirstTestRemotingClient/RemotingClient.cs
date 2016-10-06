using System;
using System.Collections;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Serialization.Formatters;
using IPC;

namespace FirstTestRemotingClient
{
    public class RemotingClient : IIpcClient
    {
        private static readonly IServerChannelSinkProvider serverSinkProvider = new BinaryServerFormatterSinkProvider { TypeFilterLevel = TypeFilterLevel.Full };

        public void Send(string data)
        {
            var properties = new Hashtable();
            properties["portName"] = Guid.NewGuid().ToString();
            properties["exclusiveAddressUse"] = false;

            var channel = new IpcChannel(properties, null, serverSinkProvider);

            try
            {
                ChannelServices.RegisterChannel(channel, true);
            }
            catch
            {
                //the channel might be already registered, so ignore it
            }

            var uri = string.Format("ipc://{0}/{1}.rem", typeof(IIpcClient).Name, typeof(RemoteObject).Name);
            var svc = Activator.GetObject(typeof(RemoteObject), uri) as IIpcClient;

            svc.Send(data);

            try
            {
                ChannelServices.UnregisterChannel(channel);
            }
            catch
            {
            }
        }
    }
}