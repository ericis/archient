namespace Archient.Web.Mvc

module Routes =
    open System.Web.Routing
    open System.Web.Mvc
    
    let getGlobalRoutes() = 
        RouteTable.Routes

    let ignore url (routes:RouteCollection) =
        ignore <| routes.IgnoreRoute(url)
        routes

    let map name url (defaults:obj) (routes:RouteCollection) =
        let route = routes.MapRoute(name, url, defaults)

        routes

    let mapToNamespaces name url (defaults:obj) (namespaces:seq<string>) (routes:RouteCollection) =
        let route = routes.MapRoute(name, url, defaults, namespaces |> Seq.toArray)

        routes

    let removeByName (name:string) (routes:RouteCollection) =
        let route = routes.[name]
        let removed = routes.Remove(route)

        routes