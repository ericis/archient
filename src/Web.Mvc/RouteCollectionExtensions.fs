namespace Archient.Web.Mvc

open System
open System.Runtime.CompilerServices
open System.Web
open System.Web.Routing

/// Extensions to the Route Collection
[<Sealed; Extension>]
type RouteCollectionExtensions() = 
    
    /// <summary>Adds a new 'ignore' route to the route collection</summary>
    /// <param name="routes">The route collection</param>
    /// <param name="url">The url pattern for the route to ignore</param>
    /// <remarks>The built-in 'Ignore' operation does not return the modified route collection and simply mutates the collection</remarks>
    /// <returns>The modified route collection</returns>
    [<Extension>]
    static member IgnoreRoute(routes:RouteCollection, url) =
        Routes.ignore url routes
    
    /// <summary>Maps a new route to the specified route collection</summary>
    /// <param name="routes">The route collection</param>
    /// <param name="name">The name of the mapped route</param>
    /// <param name="url">The url pattern for the route to map</param>
    /// <param name="defaults">The default values for the route to map</param>
    /// <remarks>The built-in 'MapRoute' operation does not return the modified route collection and simply mutates the collection</remarks>
    /// <returns>The modified route collection</returns>
    [<Extension>]
    static member MapRoute(routes:RouteCollection, name, url, defaults) =
        Routes.map name url defaults routes