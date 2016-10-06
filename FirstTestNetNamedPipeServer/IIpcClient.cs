using System.ServiceModel;

namespace FirstTestNetNamedPipeServer
{
    [ServiceContract]
    public interface IIpcClient
    {
        [OperationContract(IsOneWay = true)]
        void Send(string data);
    }
}