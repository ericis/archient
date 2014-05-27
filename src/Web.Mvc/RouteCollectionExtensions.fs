namespace Archient.Web.Mvc

open System
open System.Runtime.CompilerServices
open System.Web
open System.Web.Routing

[<Sealed; Extension>]
type RouteCollectionExtensions() = 
    
    [<Extension>]
    static member IgnoreRoute(routes:RouteCollection, url) =
        Routes.ignore url routes
    
    [<Extension>]
    static member MapRoute(routes:RouteCollection, name, url, defaults) =
        Routes.map name url defaults routes