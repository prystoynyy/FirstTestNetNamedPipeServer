using System;

namespace IPC
{
    public class RemoteObject : MarshalByRefObject, IIpcClient
    {
        private readonly IIpcClient svc;

        public RemoteObject()
        {
        }

        public RemoteObject(IIpcClient svc)
        {
            this.svc = svc;
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        void IIpcClient.Send(string data)
        {
            if (this.svc != null)
            {
                this.svc.Send(data);
            }
        }
    }
}