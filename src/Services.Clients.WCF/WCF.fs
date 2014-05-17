namespace Archient.Services.Clients

/// F# functions for clients of WCF service
module WCF =
    open System
    open System.ServiceModel

    let private tryGetCommunicationObject svc =
        let osvc = box svc
        match osvc with
        | :? ICommunicationObject as comm ->
            Some(comm)
        | _ -> None

    let private finalizeCall (comm:ICommunicationObject option) =
        if comm.IsSome then
            let state = comm.Value.State
            match state with
            | CommunicationState.Closed | CommunicationState.Closing -> ()
            | CommunicationState.Faulted -> comm.Value.Abort()
            | _ -> comm.Value.Close()

    let createFromEndpoint<'TService> (configEndPointName:string)  =
        
        let factory = new ChannelFactory<'TService>(configEndPointName)
        
        factory.CreateChannel()
        
    let callAndForget<'TService> (op:'TService->unit) (svc:'TService) =
        try
            async { op svc }
            |> Async.RunSynchronously
        finally finalizeCall (tryGetCommunicationObject svc)

    let callAndReceive<'TService,'TResponse> (op:'TService->'TResponse) (svc:'TService) =
        try
            async { return (op svc) }
            |> Async.RunSynchronously
        finally finalizeCall (tryGetCommunicationObject svc)