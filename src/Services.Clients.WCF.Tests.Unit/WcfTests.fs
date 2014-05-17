namespace Archient.Services.Clients

module WcfTests =
    open Archient.Testing
    open System.ServiceModel
    open Xunit
    
    let private createSvc () =
        WCF.createFromEndpoint<ITestService> "svc"

    let private convertSvcToComm (svc:obj) =
        svc :?> ICommunicationObject

    let [<Fact>] ``WCF.createFromEndpoint`` () =
        
        let svc = createSvc()

        svc |> Assert.isNotNull
        svc |> Assert.isType<ITestService>
        svc |> Assert.isOfType<ICommunicationObject> true
        
        let comm = convertSvcToComm svc

        comm.State |> Assert.areEqual CommunicationState.Created

    let [<Fact>] ``WCF.callAndForget`` () =
        
        let called = ref false

        let svc = createSvc()

        svc |> WCF.callAndForget (fun svc -> called := true)
        
        let comm = convertSvcToComm svc

        comm.State |> Assert.areEqual CommunicationState.Closed
        
        !called |> Assert.isTrue

    let [<Fact>] ``WCF.callAndReceive`` () =
        
        let called = ref false

        let response = "Hello world!"
        
        let svc = createSvc()

        svc
        |> WCF.callAndReceive (fun svc -> response)
        |> Assert.areEqual response
        
        let comm = convertSvcToComm svc

        comm.State |> Assert.areEqual CommunicationState.Closed