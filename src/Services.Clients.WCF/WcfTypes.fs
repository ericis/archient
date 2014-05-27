namespace Archient.Services.Clients

open System

type IUseService<'TService> = interface end

type IServiceContainer<'TService> = 
    inherit IDisposable

    abstract member InternalService : 'TService with get

[<Sealed>]
type ServiceContainer<'TService> (svc:'TService) =
    interface IServiceContainer<'TService> with
        
        override me.InternalService = svc

        override me.Dispose() =
            match box svc with
            | null -> ()
            | :? IDisposable as disposable -> disposable.Dispose()
            | _ -> ()