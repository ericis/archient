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
    static member ServiceCreate<'TService>(tag:IUseService<'TService>, configEndpointName) =
        let svc = WCF.createFromEndpoint<'TService> configEndpointName

        (new ServiceContainer<_>(svc)) :> IServiceContainer<_>
        
    /// <summary>Calls a WCF service operation specified by <c>operation</c>.</summary>
    /// <typeparam name="TService">The type of the WCF service.</typeparam>
    /// <param name="container">The container instance containing the service instance.</param>
    /// <param name="operation">The WCF service operation.</param>
    [<Extension>]
    static member ServiceCall<'TService>(container:IServiceContainer<'TService>, operation:Action<'TService>) =
        container.InternalService |> WCF.callAndForget (fun svc -> operation.Invoke(svc))

    /// <summary>Sends the specified <c>request</c> to a WCF service operation specified by <c>operation</c>.</summary>
    /// <typeparam name="TService">The type of the WCF service.</typeparam>
    /// <typeparam name="TRequest">The type of the service operation's request.</typeparam>
    /// <param name="container">The container instance containing the service instance.</param>
    /// <param name="request">The request to be sent to the service operation.</param>
    /// <param name="operation">The WCF service operation.</param>
    [<Extension>]
    static member ServiceSend<'TService,'TRequest>(container:IServiceContainer<'TService>, request:'TRequest, operation:Action<'TService,'TRequest>) =
        container.InternalService |> WCF.callAndForget (fun svc -> operation.Invoke(svc, request))
        
    /// <summary>Calls and receives a response from a WCF service operation specified by <c>operation</c>.</summary>
    /// <typeparam name="TService">The type of the WCF service.</typeparam>
    /// <typeparam name="TResponse">The type of the service operation's response.</typeparam>
    /// <param name="container">The container instance containing the service instance.</param>
    /// <param name="operation">The WCF service operation.</param>
    /// <returns>The service operation's response.</returns>
    [<Extension>]
    static member ServiceReceive<'TService,'TResponse>(container:IServiceContainer<'TService>, operation:Func<'TService,'TResponse>) =
        container.InternalService |> WCF.callAndReceive (fun svc -> operation.Invoke(svc))
        
    /// <summary>Sends the specified <c>request</c> and receives a response from a WCF service operation specified by <c>operation</c>.</summary>
    /// <typeparam name="TService">The type of the WCF service.</typeparam>
    /// <typeparam name="TRequest">The type of the service operation's request.</typeparam>
    /// <typeparam name="TResponse">The type of the service operation's response.</typeparam>
    /// <param name="container">The container instance containing the service instance.</param>
    /// <param name="request">The request to be sent to the service operation.</param>
    /// <param name="operation">The WCF service operation.</param>
    /// <returns>The service operation's response.</returns>
    [<Extension>]
    static member ServiceSendAndReceive<'TService,'TRequest,'TResponse>(container:IServiceContainer<'TService>, request:'TRequest, operation:Func<'TService,'TRequest,'TResponse>) =
        container.InternalService |> WCF.callAndReceive (fun svc -> operation.Invoke(svc, request))
        
    /// <summary>Creates a WCF service client channel and calls a service operation specified by <c>operation</c>.</summary>
    /// <typeparam name="TService">The type of the WCF service.</typeparam>
    /// <param name="tag">The instance implementing the tag interface.</param>
    /// <param name="operation">The WCF service operation.</param>
    /// <param name="configEndpointName">The name of the service endpoint in configuration.</param>
    [<Extension>]
    static member ServiceCreateAndCall<'TService>(tag:IUseService<'TService>, operation:Action<'TService>, configEndpointName) =
        use svc = WcfClientExtensions.ServiceCreate<_>(tag, configEndpointName)
        WcfClientExtensions.ServiceCall<_>(svc, operation)
        
    /// <summary>Creates a WCF service client channel and sends the specified <c>request</c> to a WCF service operation specified by <c>operation</c>.</summary>
    /// <typeparam name="TService">The type of the WCF service.</typeparam>
    /// <typeparam name="TRequest">The type of the service operation's request.</typeparam>
    /// <param name="tag">The instance implementing the tag interface.</param>
    /// <param name="request">The request to be sent to the service operation.</param>
    /// <param name="operation">The WCF service operation.</param>
    /// <param name="configEndpointName">The name of the service endpoint in configuration.</param>
    [<Extension>]
    static member ServiceCreateAndSend<'TService,'TRequest>(tag:IUseService<'TService>, request:'TRequest, operation:Action<'TService,'TRequest>, configEndpointName) =
        use svc = WcfClientExtensions.ServiceCreate<_>(tag, configEndpointName)
        WcfClientExtensions.ServiceSend<_,_>(svc, request, operation)
        
    /// <summary>Creates a WCF service client channel and calls and receives a response from a WCF service operation specified by <c>operation</c>.</summary>
    /// <typeparam name="TService">The type of the WCF service.</typeparam>
    /// <typeparam name="TResponse">The type of the service operation's response.</typeparam>
    /// <param name="tag">The instance implementing the tag interface.</param>
    /// <param name="operation">The WCF service operation.</param>
    /// <param name="configEndpointName">The name of the service endpoint in configuration.</param>
    /// <returns>The service operation's response.</returns>
    [<Extension>]
    static member ServiceCreateAndReceive<'TService,'TResponse>(tag:IUseService<'TService>, operation:Func<'TService,'TResponse>, configEndpointName) =
        use svc = WcfClientExtensions.ServiceCreate<_>(tag, configEndpointName)
        WcfClientExtensions.ServiceReceive<_,_>(svc, operation)
        
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
    static member ServiceCreateSendAndReceive<'TService,'TRequest,'TResponse>(tag:IUseService<'TService>, request:'TRequest, operation:Func<'TService,'TRequest,'TResponse>, configEndpointName) =
        use svc = WcfClientExtensions.ServiceCreate<_>(tag, configEndpointName)
        WcfClientExtensions.ServiceSendAndReceive<_,_,_>(svc, request, operation)