namespace Services.Clients.WCF.Examples.CSharp
{
    using Archient.Services;

    public class TestService : ServiceBase, ITestService
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

        public override string HealthCheck(string key)
        {
            //// Return a structured health-check response to the caller

            return "<response>Healthy</response>"; // return XML
            ////return "{ 'response': 'Healthy' }"; // return JSON
        }
    }
}