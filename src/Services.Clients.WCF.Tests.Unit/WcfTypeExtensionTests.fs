namespace Archient.Services.Clients

module WcfTypeExtensionsTests =
    open Archient.Testing
    open System
    open System.ServiceModel
    open Xunit

    let private createSvc () =
        new TestServiceClient()

    let [<Fact; UnitTest>] ``IUseWcfService.Create``() =
        
        let svc = createSvc()

        let container = svc.Create("svc")
        
        container |> Assert.isNotNull

    let [<Fact; UnitTest>] ``IUseWcfService.CreateAndCall``() =
        
        let svc = createSvc()
        let called = ref false
        let action = new Action<_>(fun _ -> called := true)
        
        svc.CreateAndCall(action, "svc")
        
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
        
        svc.CreateAndSend(expectedRequest, operation, "svc")
        
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
        
        svc.CreateAndReceive(operation, "svc")
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
        
        svc.CreateSendAndReceive(expectedRequest, operation, "svc")
        |> Assert.areEqual expectedResponse
        
        !called |> Assert.isTrue