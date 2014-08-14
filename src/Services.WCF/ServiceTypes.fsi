namespace Archient.Services

open System
open Archient.Services.Contracts

/// Base implementation of a service
[<AbstractClass>]
type ServiceBase =
    
    interface IPingService
    interface IHealthCheckService
    
    /// Default constructor
    new : unit -> ServiceBase
    
    /// Ping operation returning a string that indicates the service is operational
    abstract member Ping : unit -> string

    /// Health check operation returning a string that indicates the health of the service under the context of the specified key
    abstract member HealthCheck : key:string -> string