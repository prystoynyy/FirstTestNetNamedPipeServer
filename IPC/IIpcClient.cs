using System.ServiceModel;

namespace IPC
{
    [ServiceContract]
    public interface IIpcClient
    {
        [OperationContract(IsOneWay = true)]
        void Send(string data);
    }
}