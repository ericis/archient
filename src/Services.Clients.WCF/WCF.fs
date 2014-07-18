namespace Archient.Services.Clients

// Documentation is provided in Signature File (.fsi)

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

    let createFromEndpoint<'TService> (configEndpointName:string)  =
        
        let factory = new ChannelFactory<'TService>(configEndpointName)
        
        factory.CreateChannel()
        
    let callAndForget<'TService> (operation:'TService->unit) (service:'TService) =
        try
            async { operation service }
            |> Async.RunSynchronously
        finally finalizeCall (tryGetCommunicationObject service)

    let callAndReceive<'TService,'TResponse> (operation:'TService->'TResponse) (service:'TService) =
        try
            async { return (operation service) }
            |> Async.RunSynchronously
        finally finalizeCall (tryGetCommunicationObject service)