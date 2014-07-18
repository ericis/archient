namespace Services.Clients.WCF.Examples.CSharp
{
    using System.ServiceModel;
    using Archient.Services.Contracts;

    [ServiceContract]
    public interface ITestService : IPingService, IHealthCheckService
    {
        [OperationContract]
        void ExecuteWithNoResponse();

        [OperationContract]
        void PostData(string request);

        [OperationContract]
        string GetData();
        
        [OperationContract]

        string PostAndProcess(string request);
    }
}