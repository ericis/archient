namespace Archient.Services.Clients

open System

type IUseService<'TService> = interface end

type IServiceContainer<'TService> = 
    inherit IDisposable

    abstract member InternalService : 'TService with get

[<Sealed>]
type ServiceContainer<'TService> (service:'TService) =
    interface IServiceContainer<'TService> with
        
        override me.InternalService = service

        override me.Dispose() =
            match box service with
            | null -> ()
            | :? IDisposable as disposable -> disposable.Dispose()
            | _ -> ()