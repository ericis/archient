namespace Archient.Web.Http

open System
open System.Runtime.CompilerServices
open System.Web
open System.Web.Http

[<Sealed; Extension>]
type HttpApplicationExtensions() =
    
    [<Extension>]
    static member InitializeHttpConfiguration(app:HttpApplication, initialization:Action<_>) =
        Configuration.initializeGlobalConfiguration(fun configuration -> initialization.Invoke(configuration))
    
    [<Extension>]
    static member MapAttributeRoutes(cfg:HttpConfiguration) =
        ignore <| cfg.MapHttpAttributeRoutes()
        cfg