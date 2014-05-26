namespace Services.Clients.WCF.Examples.CSharp
{
    using System;
    using System.ServiceModel;
    using Archient.Services.Clients;
    using Archient.Testing;
    using Xunit;
    using Assert = Xunit.Assert;

    public class ExampleTests : IUseService<ITestService>
    {
        private const string ConfigEndpointName = "svc";

        private const string ExpectedRequest = "request";
        private const string ExpectedSimpleResponse = "response";
        private const string ExpectedResponse = ExpectedRequest + " " + ExpectedSimpleResponse;

        #region Example: Real-World business logic

        // Pretend you have a complex business domain object representing all of your input state
        private class MyBusinessRequest
        {
            public MyBusinessRequest(string message)
            {
                this.Message = message;
            }

            public string Message { get; private set; }
        }

        // Pretend you have a complex business domain object representing all of your output state
        private class MyBusinessResponse
        {
            public MyBusinessResponse(string confirmationMessage)
            {
                this.ConfirmationMessage = confirmationMessage;
            }

            public string ConfirmationMessage { get; private set; }
        }

        // Pretend you want to "hide" the service call as infrastructure 
        //  encapsulating the mappings between the service and business domain state.
        //  *Complex mappings may use AutoMapper or other libraries...
        private static MyBusinessResponse SendAndReceiveWithLogic(
            ITestService testService,
            MyBusinessRequest businessRequest)
        {
            // input transform (Domain POCO -> Service DTO)
            var serviceRequest = businessRequest.Message;

            // service call
            var serviceResponse = testService.PostAndProcess(serviceRequest);

            // output transform (Service DTO -> Domain POCO)
            return new MyBusinessResponse(serviceResponse);
        }

        [Fact, ExampleTest]
        public void Create_Send_And_Receive_With_Logic()
        {
            //// NOTE: This logic never exposes WCF details or the underlying service call details and data mappings.
            ////       Just the business input and output.

            ExecuteWithRunningService(
                () =>
                {
                    var request = new MyBusinessRequest(ExpectedRequest);

                    // internally creates the WCF client proxy and sends the specified request
                    // to the specified operation expecting a response
                    var response = this.CreateSendAndReceive(
                        request,
                        SendAndReceiveWithLogic,
                        ConfigEndpointName);

                    Assert.Equal(ExpectedResponse, response.ConfirmationMessage);
                });
        }

        #endregion

        #region Examples: Simple Tests

        // ** SHARED MUTABLE BOOLEAN, one per test **
        private bool called;

        [Fact, ExampleTest]
        public void Create_Service_Container()
        {
            // Creates a WCF client proxy from an extension method using the specified name
            var serviceContainer = this.Create(ConfigEndpointName);

            Assert.NotNull(serviceContainer);
        }

        [Fact, ExampleTest]
        public void Create_And_Call()
        {
            // Wrapper to start a running WCF service
            ExecuteWithRunningService(
                // creates the WCF client proxy and calls the specified operation
                () => this.CreateAndCall(
                    testService => testService.ExecuteWithNoResponse(),
                    ConfigEndpointName));
        }

        [Fact, ExampleTest]
        public void Create_And_Send()
        {
            ExecuteWithRunningService(
                // creates the WCF client proxy and sends the specified request to the specified operation
                () => this.CreateAndSend(
                    ExpectedRequest,
                    (testService, request) => testService.PostData(request),
                    ConfigEndpointName));

            /***********************************************************************************************
             * 
             * Q: Why would you need to pass the request above vs. just passing as in the example below?
             * 
             * A: Additional logic may be associated with the service call, which may be best encapsulated 
             *    in a single method used as the delegate argument to the above call.  For example, if you 
             *    have transformation logic from business domain entities to and from the service 
             *    request/response, these could be wrapped up in the method call.
             * 
            ***********************************************************************************************/

            // OR, if you have scope to the original request state in the call...
            //ExecuteWithRunningService(
            //    () => this.CreateAndCall(
            //        testService => testService.PostData(ExpectedRequest),
            //        ConfigEndpointName));
        }

        [Fact, ExampleTest]
        public void Create_And_Receive()
        {
            ExecuteWithRunningService(
                () =>
                {
                    // creates the WCF client proxy and calls the specified operation with a response
                    var response = this.CreateAndReceive(
                        testService => testService.GetData(),
                        ConfigEndpointName);

                    Assert.Equal(ExpectedSimpleResponse, response);
                });
        }

        [Fact, ExampleTest]
        public void Create_Send_And_Receive()
        {
            ExecuteWithRunningService(
                () =>
                {
                    // creates the WCF client proxy and sends the specified request to the specified operation with a response
                    var response = this.CreateSendAndReceive(
                        ExpectedRequest,
                        (testService, request) => testService.PostAndProcess(request),
                        ConfigEndpointName);

                    Assert.Equal(ExpectedResponse, response);
                });
        }

        #endregion

        #region Utilities

        private static void ExecuteWithRunningService(Action testAction)
        {
            using (var serviceHost = new ServiceHost(typeof(TestService)))
            {
                serviceHost.Open();

                try
                {
                    testAction();
                }
                finally
                {
                    serviceHost.Close();
                }
            }
        }

        #endregion
    }
}