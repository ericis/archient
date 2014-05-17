namespace Archient.Services.Clients

open System.ServiceModel

[<ServiceContract>]
type ITestService =
    [<OperationContract>]
    abstract member Send : unit -> unit
    
    [<OperationContract>]
    abstract member Receive : unit -> string
    
    [<OperationContract>]
    abstract member SendReceive : request:string -> string

type TestServiceClient() =
    interface IUseService<ITestService>