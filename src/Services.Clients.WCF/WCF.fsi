namespace Archient.Services.Clients

/// F# functions for clients of WCF service
module WCF =
    open System
    open System.ServiceModel

    /// <summary>Creates a WCF channel for the specified service type <c>'TService</c> at the specified <c>endpoint</c>.</summary>
    /// <typeparam name="TService">The type of the WCF service.</typeparam>
    /// <param name="configEndpointName">The name of the service endpoint in configuration.</param>
    /// <returns>The WCF service channel.</returns>
    val createFromEndpoint<'TService> : string -> 'TService
    
    /// <summary>Calls a WCF service operation without a response.</summary>
    /// <typeparam name="TService">The type of the WCF service.</typeparam>
    /// <param name="op">The service operation call definition.</param>
    /// <param name="svc">The WCF service.</param>
    /// <remarks>
    /// <para>
    /// The service operation is executed internally as an F# asynchronous call to free the client 
    /// from IO blocks and simplify the complexity of authoring traditional .NET Async code.  The 
    /// method name is not suffixed with <c>Async</c> because it is designed to neither require the 
    /// use of C# <c>async</c> keyword nor to support the original .NET <c>AsyncCallback</c> delegate 
    /// pattern.  Auto-generated WCF proxies historically provide an implementation of the older 
    /// <c>AsyncCallback</c> delegate pattern and newer versions also support use of the C# 
    /// <c>async</c> keyword.
    /// </para>
    /// <para>
    /// The WCF client channel is automatically closed (success) or aborted (error) if the 
    /// <c>svc</c> is an instance of <see cref="ICommunicationObject"/>.
    /// </para>
    /// </remarks>
    val callAndForget<'TService> : ('TService->unit) -> 'TService -> unit

    /// <summary>Calls a WCF service operation and returns the response.</summary>
    /// <typeparam name="TService">The type of the WCF service.</typeparam>
    /// <typeparam name="TResponse">The type of the WCF service operation's response.</typeparam>
    /// <param name="op">The service operation call definition.</param>
    /// <param name="svc">The WCF service.</param>
    /// <returns>The service operation's response.</returns>
    /// <remarks>
    /// <para>
    /// The service operation is executed internally as an F# asynchronous call to free the client 
    /// from IO blocks and simplify the complexity of authoring traditional .NET Async code.  The 
    /// method name is not suffixed with <c>Async</c> because it is designed to neither require the 
    /// use of C# <c>async</c> keyword nor to support the original .NET <c>AsyncCallback</c> delegate 
    /// pattern.  Auto-generated WCF proxies historically provide an implementation of the older 
    /// <c>AsyncCallback</c> delegate pattern and newer versions also support use of the C# 
    /// <c>async</c> keyword.
    /// </para>
    /// <para>
    /// The WCF client channel is automatically closed (success) or aborted (error) if the 
    /// <c>svc</c> is an instance of <see cref="ICommunicationObject"/>.
    /// </para>
    /// </remarks>
    val callAndReceive<'TService,'TResponse> : ('TService->'TResponse) -> 'TService -> 'TResponse