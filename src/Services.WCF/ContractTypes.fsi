﻿namespace Archient.Services.Contracts

/// <summary>
/// Contract for a 'Ping' interface.
/// Used to test the reachability of a host on an Internet Protocol (IP) network and 
/// to measure the round-trip time for messages sent from the originating host
/// to a destination computer. The name comes from active sonar terminology which 
/// sends a pulse of sound and listens for the echo to detect objects underwater.
/// </summary>
[<Interface>]
type IPingService =
    /// <summary>
    /// Used to test the reachability of a host on an Internet Protocol (IP) network and 
    /// to measure the round-trip time for messages sent from the originating host
    /// to a destination computer. The name comes from active sonar terminology which 
    /// sends a pulse of sound and listens for the echo to detect objects underwater.
    /// </summary>
    /// <returns>A response message indicating a successful 'Ping'.</returns>
    /// <remarks>
    /// An input is not expected by this operation. Although many requirements could be 
    /// invented to suggest an input is required, in practice the root concerns of these 
    /// inputs are best aligned with other solutions (e.g. client security, client logging, etc).
    /// </remarks>
    abstract member Ping : unit -> string

/// <summary>
/// Contract for a health check implementation that reports the health of the component and optionally dependencies.
/// </summary>
/// <remarks>
/// Typically, the health check response is in a structured format (e.g. JSON, XML, etc).
/// While a standardized, structured response could be designed in the contract,
/// the goal of this interface is to standardize the health check interface to components and not 
/// the data controlled by the implementation.
/// </remarks>
[<Interface>]
type IHealthCheckService =
    
    /// <summary>
    /// Reports the health of the component and optionally dependencies associated with the specified key.
    /// </summary>
    /// <param name="key">The key used in the health check.</param>
    /// <returns>The result of the health check.</returns>
    abstract member HealthCheck : key:string -> string