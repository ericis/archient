namespace Archient.Web.Http

open System
open System.Runtime.CompilerServices
open System.Web.Http

/// Extensions to the <cref see="HttpRouteCollection" /> class.
[<Sealed; Extension>]
type HttpRouteCollectionExtensions() = 
    
    /// <summary>Maps a new route to the route collection.</summary>
    /// <param name="routes">The collection of routes to modify.</param>
    /// <param name="name">The name of the route.</param>
    /// <param name="routeTemplate">The URL template associated with the route.</param>
    /// <param name="defaults">The default data values applied to the route.</param>
    /// <returns>The modified route collection.</returns>
    [<Extension>]
    static member MapRoute(routes:HttpRouteCollection, name, routeTemplate, defaults) =
        Routes.mapHttp name routeTemplate defaults routes