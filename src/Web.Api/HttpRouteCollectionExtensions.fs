namespace Archient.Web.Http

open System
open System.Runtime.CompilerServices
open System.Web.Http

[<Sealed; Extension>]
type RouteCollectionExtensions() = 
    
    [<Extension>]
    static member MapRoute(routes:HttpRouteCollection, name, url, defaults) =
        Routes.mapHttp name url defaults routes