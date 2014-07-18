namespace Archient.Services.Contracts

// Documentation is provided in Signature File (.fsi)

open System.ServiceModel

[<ServiceContract>]
type IPingService =
    
    [<OperationContract>]
    abstract member Ping : unit -> string
    
[<ServiceContract>]
type IHealthCheckService =
    
    [<OperationContract>]
    abstract member HealthCheck : key:string -> string