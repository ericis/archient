namespace Archient.Services.Clients

open System
open System.Runtime.CompilerServices

/// Extensions to the <cref see="IUseService{TService}" /> and <cref see="IServiceContainer{TService}" /> interfaces.
[<Sealed; Extension>]
type WcfClientExtensions () =
    
    /// <summary>Creates a WCF service container of the specified type <c>'TService</c> at the specified <c>endpoint</c>.</summary>
    /// <typeparam name="TService">The type of the WCF service.</typeparam>
    /// <param name="tag">The instance implementing the tag interface.</param>
    /// <param name="configEndpointName">The name of the service endpoint in configuration.</param>
    /// <returns>The service container.</returns>
    [<Extension>]
    static member Create<'TService>(tag:IUseService<'TService>, configEndpointName) =
        let svc = WCF.createFromEndpoint<'TService> configEndpointName

        (new ServiceContainer<_>(svc)) :> IServiceContainer<_>
        
    /// <summary>Calls a WCF service operation specified by <c>operation</c>.</summary>
    /// <typeparam name="TService">The type of the WCF service.</typeparam>
    /// <param name="container">The container instance containing the service instance.</param>
    /// <param name="operation">The WCF service operation.</param>
    [<Extension>]
    static member Call<'TService>(container:IServiceContainer<'TService>, operation:Action<'TService>) =
        container.Service |> WCF.callAndForget (fun svc -> operation.Invoke(svc))

    /// <summary>Sends the specified <c>request</c> to a WCF service operation specified by <c>operation</c>.</summary>
    /// <typeparam name="TService">The type of the WCF service.</typeparam>
    /// <typeparam name="TRequest">The type of the service operation's request.</typeparam>
    /// <param name="container">The container instance containing the service instance.</param>
    /// <param name="request">The request to be sent to the service operation.</param>
    /// <param name="operation">The WCF service operation.</param>
    [<Extension>]
    static member Send<'TService,'TRequest>(container:IServiceContainer<'TService>, request:'TRequest, operation:Action<'TService,'TRequest>) =
        container.Service |> WCF.callAndForget (fun svc -> operation.Invoke(svc, request))
        
    /// <summary>Calls and receives a response from a WCF service operation specified by <c>operation</c>.</summary>
    /// <typeparam name="TService">The type of the WCF service.</typeparam>
    /// <typeparam name="TResponse">The type of the service operation's response.</typeparam>
    /// <param name="container">The container instance containing the service instance.</param>
    /// <param name="operation">The WCF service operation.</param>
    /// <returns>The service operation's response.</returns>
    [<Extension>]
    static member Receive<'TService,'TResponse>(container:IServiceContainer<'TService>, operation:Func<'TService,'TResponse>) =
        container.Service |> WCF.callAndReceive (fun svc -> operation.Invoke(svc))
        
    /// <summary>Sends the specified <c>request</c> and receives a response from a WCF service operation specified by <c>operation</c>.</summary>
    /// <typeparam name="TService">The type of the WCF service.</typeparam>
    /// <typeparam name="TRequest">The type of the service operation's request.</typeparam>
    /// <typeparam name="TResponse">The type of the service operation's response.</typeparam>
    /// <param name="container">The container instance containing the service instance.</param>
    /// <param name="request">The request to be sent to the service operation.</param>
    /// <param name="operation">The WCF service operation.</param>
    /// <returns>The service operation's response.</returns>
    [<Extension>]
    static member SendAndReceive<'TService,'TRequest,'TResponse>(container:IServiceContainer<'TService>, request:'TRequest, operation:Func<'TService,'TRequest,'TResponse>) =
        container.Service |> WCF.callAndReceive (fun svc -> operation.Invoke(svc, request))
        
    /// <summary>Creates a WCF service client channel and calls a service operation specified by <c>operation</c>.</summary>
    /// <typeparam name="TService">The type of the WCF service.</typeparam>
    /// <param name="tag">The instance implementing the tag interface.</param>
    /// <param name="operation">The WCF service operation.</param>
    /// <param name="configEndpointName">The name of the service endpoint in configuration.</param>
    [<Extension>]
    static member CreateAndCall<'TService>(tag:IUseService<'TService>, operation:Action<'TService>, configEndpointName) =
        use svc = WcfClientExtensions.Create<_>(tag, configEndpointName)
        WcfClientExtensions.Call<_>(svc, operation)
        
    /// <summary>Creates a WCF service client channel and sends the specified <c>request</c> to a WCF service operation specified by <c>operation</c>.</summary>
    /// <typeparam name="TService">The type of the WCF service.</typeparam>
    /// <typeparam name="TRequest">The type of the service operation's request.</typeparam>
    /// <param name="tag">The instance implementing the tag interface.</param>
    /// <param name="request">The request to be sent to the service operation.</param>
    /// <param name="operation">The WCF service operation.</param>
    /// <param name="configEndpointName">The name of the service endpoint in configuration.</param>
    [<Extension>]
    static member CreateAndSend<'TService,'TRequest>(tag:IUseService<'TService>, request:'TRequest, operation:Action<'TService,'TRequest>, configEndpointName) =
        use svc = WcfClientExtensions.Create<_>(tag, configEndpointName)
        WcfClientExtensions.Send<_,_>(svc, request, operation)
        
    /// <summary>Creates a WCF service client channel and calls and receives a response from a WCF service operation specified by <c>operation</c>.</summary>
    /// <typeparam name="TService">The type of the WCF service.</typeparam>
    /// <typeparam name="TResponse">The type of the service operation's response.</typeparam>
    /// <param name="tag">The instance implementing the tag interface.</param>
    /// <param name="operation">The WCF service operation.</param>
    /// <param name="configEndpointName">The name of the service endpoint in configuration.</param>
    /// <returns>The service operation's response.</returns>
    [<Extension>]
    static member CreateAndReceive<'TService,'TResponse>(tag:IUseService<'TService>, operation:Func<'TService,'TResponse>, configEndpointName) =
        use svc = WcfClientExtensions.Create<_>(tag, configEndpointName)
        WcfClientExtensions.Receive<_,_>(svc, operation)
        
    /// <summary>Creates a WCF service client channel and sends the specified <c>request</c> and receives a response from a WCF service operation specified by <c>operation</c>.</summary>
    /// <typeparam name="TService">The type of the WCF service.</typeparam>
    /// <typeparam name="TRequest">The type of the service operation's request.</typeparam>
    /// <typeparam name="TResponse">The type of the service operation's response.</typeparam>
    /// <param name="tag">The instance implementing the tag interface.</param>
    /// <param name="request">The request to be sent to the service operation.</param>
    /// <param name="operation">The WCF service operation.</param>
    /// <param name="configEndpointName">The name of the service endpoint in configuration.</param>
    /// <returns>The service operation's response.</returns>
    [<Extension>]
    static member CreateSendAndReceive<'TService,'TRequest,'TResponse>(tag:IUseService<'TService>, request:'TRequest, operation:Func<'TService,'TRequest,'TResponse>, configEndpointName) =
        use svc = WcfClientExtensions.Create<_>(tag, configEndpointName)
        WcfClientExtensions.SendAndReceive<_,_,_>(svc, request, operation)