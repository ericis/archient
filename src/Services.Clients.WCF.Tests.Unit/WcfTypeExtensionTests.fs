namespace Archient.Services.Clients

module WcfTypeExtensionsTests =
    open Archient.Testing
    open System
    open System.ServiceModel
    open Xunit

    let private configEndpointName = "svc"

    let private createSvc () =
        new TestServiceClient()

    let [<Fact; UnitTest>] ``IUseWcfService.Create``() =
        
        let svc = createSvc()

        let container = svc.ServiceCreate(configEndpointName)
        
        container |> Assert.isNotNull

    let [<Fact; UnitTest>] ``IUseWcfService.CreateAndCall``() =
        
        let svc = createSvc()
        let called = ref false
        let operation = new Action<_>(fun _ -> called := true)
        
        svc.ServiceCreateAndCall(operation, configEndpointName)
        
        !called |> Assert.isTrue

    let [<Fact; UnitTest>] ``IUseWcfService.CreateAndSend``() =
        
        let expectedRequest = "request"

        let svc = createSvc()
        let called = ref false
        let operation = 
            new Action<_,_>(
                fun _ -> 
                    fun request -> 
                        request |> Assert.areEqual expectedRequest
                        called := true)
        
        svc.ServiceCreateAndSend(expectedRequest, operation, configEndpointName)
        
        !called |> Assert.isTrue

    let [<Fact; UnitTest>] ``IUseWcfService.CreateAndReceive``() =
        
        let expectedResponse = "response"

        let svc = createSvc()
        let called = ref false
        let operation = 
            new Func<_,_>(
                fun _ -> 
                    called := true
                    expectedResponse)
        
        svc.ServiceCreateAndReceive(operation, configEndpointName)
        |> Assert.areEqual expectedResponse
        
        !called |> Assert.isTrue

    let [<Fact; UnitTest>] ``IUseWcfService.CreateSendAndReceive``() =
        
        let expectedRequest = "request"
        let simpleResponse = "response"
        let expectedResponse = sprintf "%s %s" expectedRequest simpleResponse

        let svc = createSvc()
        let called = ref false
        let operation = 
            new Func<_,_,_>(
                fun _ -> 
                    fun request -> 
                        called := true
                        sprintf "%s %s" request simpleResponse)
        
        svc.ServiceCreateSendAndReceive(expectedRequest, operation, configEndpointName)
        |> Assert.areEqual expectedResponse
        
        !called |> Assert.isTrue

    let [<Fact; UnitTest>] ``IServiceContainer.Call``() =
        
        let svc = createSvc()
        let called = ref false
        let operation = new Action<_>(fun _ -> called := true)

        let container = svc.ServiceCreate(configEndpointName)
        
        !called |> Assert.isFalse

        container.ServiceCall(operation)

        !called |> Assert.isTrue

    let [<Fact; UnitTest>] ``IServiceContainer.Send``() =
        
        let svc = createSvc()
        let expectedRequest = "request"
        let called = ref false
        let operation = 
            new Action<_,_>(
                fun _ -> 
                    fun request -> 
                        request |> Assert.areEqual expectedRequest
                        called := true)

        let container = svc.ServiceCreate(configEndpointName)
        
        !called |> Assert.isFalse

        container.ServiceSend(expectedRequest, operation)

        !called |> Assert.isTrue

    let [<Fact; UnitTest>] ``IServiceContainer.Receive``() =
        
        let svc = createSvc()
        let expectedResponse = "response"
        let called = ref false
        let operation = 
            new Func<_,_>(
                fun _ -> 
                    called := true
                    expectedResponse)

        let container = svc.ServiceCreate(configEndpointName)
        
        !called |> Assert.isFalse

        container.ServiceReceive(operation)
        |> Assert.areEqual expectedResponse

        !called |> Assert.isTrue

    let [<Fact; UnitTest>] ``IServiceContainer.SendAndReceive``() =
        
        let expectedRequest = "request"
        let simpleResponse = "response"
        let expectedResponse = sprintf "%s %s" expectedRequest simpleResponse

        let svc = createSvc()
        let called = ref false
        let operation = 
            new Func<_,_,_>(
                fun _ -> 
                    fun request -> 
                        called := true
                        sprintf "%s %s" request simpleResponse)

        let container = svc.ServiceCreate(configEndpointName)
        
        !called |> Assert.isFalse

        container.ServiceSendAndReceive(expectedRequest, operation)
        |> Assert.areEqual expectedResponse

        !called |> Assert.isTrue