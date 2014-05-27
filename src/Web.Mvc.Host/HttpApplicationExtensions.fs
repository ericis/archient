namespace Archient.Web.Mvc

open System
open System.Runtime.CompilerServices
open System.Web
open Archient.Web.Mvc

[<Sealed; Extension>]
type HttpApplicationExtensions() = 
    
    [<Extension>]
    static member RegisterAreas(app:HttpApplication) =
        Areas.registerAll()
        app

    [<Extension>]
    static member GetRoutes(app:HttpApplication) =
        Routes.getGlobalRoutes()