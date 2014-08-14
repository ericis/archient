namespace Archient.Web.Mvc

/// F# MVC Routing library
module Routes =
    open System.Web.Mvc
    open System.Web.Routing
    
    /// Controller defaults for a route
    type ControllerDefaults =
        {
            /// Gets the default route controller name
            controller : string
            /// Gets the default route controller action name
            action : string
            /// Gets the default route controller action id
            id : UrlParameter
        }
    
    /// Gets the MVC route collection from global-scope
    val getGlobalRoutes : unit -> RouteCollection

    /// Adds an 'ignore' route to the route collection and returns the modified collection
    val ignore : url:string -> routes:RouteCollection -> RouteCollection
    
    /// Maps a route to the route collection and returns the modified collection
    val map : name:string -> url:string -> defaults:obj -> routes:RouteCollection -> RouteCollection
    
    /// Maps a route constrained to the specified namespaces to the route collection and returns the modified collection
    val mapToNamespaces : name:string -> url:string -> defaults:obj -> namespaces:seq<string> -> routes:RouteCollection -> RouteCollection

    /// Removes a route by name from the route collectioon and returns the modified collection
    val removeByName : name:string -> routes:RouteCollection -> RouteCollection

    /// Prints the route collection to a string
    val prettyPrint : routes:RouteCollection -> string