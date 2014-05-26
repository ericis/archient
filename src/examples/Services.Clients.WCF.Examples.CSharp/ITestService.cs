namespace Services.Clients.WCF.Examples.CSharp
{
    using System.ServiceModel;

    [ServiceContract]
    public interface ITestService
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

    public class TestService : ITestService
    {
        public void ExecuteWithNoResponse()
        {
            // no-op
        }

        public void PostData(string request)
        {
            // received request
        }

        public string GetData()
        {
            return "response";
        }

        public string PostAndProcess(string request)
        {
            return request + " response";
        }
    }
}