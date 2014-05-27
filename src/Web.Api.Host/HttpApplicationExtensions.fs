namespace Archient.Web.Http

open System
open System.Runtime.CompilerServices
open System.Web
open System.Web.Http

[<Sealed; Extension>]
type HttpApplicationExtensions() =
    
    [<Extension>]
    static member GetHttpConfiguration(app:HttpApplication) =
        Configuration.getGlobalConfiguration()
    
    [<Extension>]
    static member MapAttributeRoutes(cfg:HttpConfiguration) =
        ignore <| cfg.MapHttpAttributeRoutes()
        cfg