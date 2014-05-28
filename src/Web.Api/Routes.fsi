namespace Archient.Web.Http

module Routes =
    open System.Web.Http
    
    /// <summary>Maps a new HTTP routing rule.</summary>
    /// <param name="name">The name of the route.</param>
    /// <param name="routeTemplate">The URL template associated with the route.</param>
    /// <param name="defaults">The default data values applied to the route.</param>
    /// <param name="routes">The route collection to add the route to.</param>
    /// <returns>The modified route collection.</param>
    val mapHttp : name:string -> routeTemplate:string -> defaults:obj -> routes:HttpRouteCollection -> HttpRouteCollection