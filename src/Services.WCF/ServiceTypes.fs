namespace Archient.Services

open System
open Archient.Services.Contracts

[<AbstractClass>]
type ServiceBase() =
    
    abstract member Ping : unit -> string
    default me.Ping() = 
        DateTime.UtcNow.ToString()
    
    abstract member HealthCheck : key:string -> string
    
    interface IPingService with
        override me.Ping() = 
            me.Ping()

    interface IHealthCheckService with
        override me.HealthCheck(key) = 
            me.HealthCheck(key)