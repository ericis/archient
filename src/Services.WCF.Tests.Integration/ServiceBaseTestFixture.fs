namespace Archient.Services.Tests.Integration

module ServiceBaseTestFixture = 
    
    open System
    open System.ServiceModel
    open Xunit
    open Archient.Testing
    open Archient.Services
    open Archient.Services.Clients
    open Archient.Services.Contracts
    
    type TestService() =
        inherit ServiceBase()

        override me.HealthCheck(key) =
            key

    let [<Fact; IntegrationTest>]``ServiceBase.Ping``() =
        
        let endpoint = "http://localhost:8088/archient-test"
        let addr = Uri(endpoint)
        use host = new ServiceHost(typeof<TestService>, addr)

        host.Open()
        
        let pingResponse = 
            WCF.createFromEndpoint<IPingService> "TestService"
            |> WCF.callAndReceive (fun svc -> svc.Ping())

        Assert.isFalse (String.IsNullOrWhiteSpace(pingResponse))

        host.Close()