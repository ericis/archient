namespace Archient.Services

open System
open Archient.Services.Contracts

[<AbstractClass>]
type ServiceBase =
    
    interface IPingService
    interface IHealthCheckService
    
    new : unit -> ServiceBase
    
    abstract member Ping : unit -> string

    abstract member HealthCheck : key:string -> string