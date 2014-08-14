namespace Archient.Services.Clients

module WcfTests =
    open Archient.Testing
    open System.ServiceModel
    open Xunit
    
    let private createSvc () =
        WCF.createFromEndpoint<ITestService> "svc"

    let private getSvcWithState state onAbort onClose =
        {
            new ICommunicationObject with

                override me.Abort() = onAbort()

                override me.Close() = onClose()
                override me.Close(_) = onClose()
                override me.BeginClose(_,_) = Unchecked.defaultof<_>
                override me.BeginClose(_,_,_) = Unchecked.defaultof<_>
                override me.EndClose(_) = onClose()

                override me.Open() = ()
                override me.Open(_) = ()
                override me.BeginOpen(_,_) = Unchecked.defaultof<_>
                override me.BeginOpen(_,_,_) = Unchecked.defaultof<_>
                override me.EndOpen(_) = ()

                override me.get_State() = state

                override me.add_Opening(_) = ()
                override me.remove_Opening(_) = ()
                override me.add_Opened(_) = ()
                override me.remove_Opened(_) = ()
                override me.add_Closing(_) = ()
                override me.remove_Closing(_) = ()
                override me.add_Closed(_) = ()
                override me.remove_Closed(_) = ()
                override me.add_Faulted(_) = ()
                override me.remove_Faulted(_) = ()
        }

    let [<Fact; UnitTest>] ``WCF.createFromEndpoint`` () =
        
        let svc = createSvc()

        svc |> Assert.isNotNull
        svc |> Assert.isType<ITestService>
        svc |> Assert.isOfType<ICommunicationObject> true
        
        let comm = svc :?> ICommunicationObject

        comm.State |> Assert.areEqual CommunicationState.Created

    let [<Fact; UnitTest>] ``WCF.callAndForget`` () =
        
        let called = ref false

        let svc = createSvc()

        svc |> WCF.callAndForget (fun svc -> called := true)
        
        let comm = svc :?> ICommunicationObject

        comm.State |> Assert.areEqual CommunicationState.Closed
        
        !called |> Assert.isTrue

    let [<Fact; UnitTest>] ``WCF.callAndForget - Closing`` () =
        
        let called = ref false
        let closed = ref false

        let closingSvc = getSvcWithState CommunicationState.Closing (fun _ -> ()) (fun _ -> closed := true)

        closingSvc |> WCF.callAndForget (fun svc -> called := true)
        
        closingSvc.State |> Assert.areEqual CommunicationState.Closing
        
        !called |> Assert.isTrue
        !closed |> Assert.isFalse

    let [<Fact; UnitTest>] ``WCF.callAndForget - Closed`` () =
        
        let called = ref false
        let closed = ref false

        let closedSvc = getSvcWithState CommunicationState.Closed (fun _ -> ()) (fun _ -> closed := true)

        closedSvc |> WCF.callAndForget (fun svc -> called := true)
        
        closedSvc.State |> Assert.areEqual CommunicationState.Closed
        
        !called |> Assert.isTrue
        !closed |> Assert.isFalse

    let [<Fact; UnitTest>] ``WCF.callAndForget - Faulted`` () =
        
        let called = ref false
        let aborted = ref false

        let faultedSvc = getSvcWithState CommunicationState.Faulted (fun _ -> aborted := true) (fun _ -> ())

        faultedSvc |> WCF.callAndForget (fun svc -> called := true)
        
        faultedSvc.State |> Assert.areEqual CommunicationState.Faulted
        
        !called |> Assert.isTrue
        !aborted |> Assert.isTrue

    let [<Fact; UnitTest>] ``WCF.callAndReceive`` () =
        
        let called = ref false

        let response = "Hello world!"
        
        let svc = createSvc()

        svc
        |> WCF.callAndReceive (fun svc -> response)
        |> Assert.areEqual response
        
        let comm = svc :?> ICommunicationObject

        comm.State |> Assert.areEqual CommunicationState.Closed