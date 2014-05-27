namespace Archient.Web.Mvc

module Routes =
    open System
    open System.Web.Mvc
    open System.Web.Routing
    
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

    let prettyPrint (routes:RouteCollection) =
        routes
        |> Seq.iter (fun route -> 
            try
                match route with
                | :? System.Web.Routing.Route as r -> 
                    let map (routeValues:System.Web.Routing.RouteValueDictionary) =
                        try
                            match routeValues.Count with
                            | 0 -> Array.empty
                            | _ -> routeValues |> Seq.map (fun routeValue -> sprintf "{ '%s' = '%A' }" routeValue.Key routeValue.Value) |> Seq.toArray
                        with | _ -> Array.empty

                    let constraints, data, defaults = 
                        (
                            String.Join(",", map r.Constraints), 
                            String.Join(",", map r.DataTokens), 
                            String.Join(",", map r.Defaults))

                    System.Diagnostics.Debug.WriteLine(sprintf "%s to %s: { 'constraints': [%s], 'data': [%s], 'defaults': [%s] }" (r.GetType().FullName) r.Url constraints data defaults)

                | :? System.Web.Routing.RouteBase as br -> System.Diagnostics.Debug.WriteLine(sprintf "Base route: %A" br)
                | r -> System.Diagnostics.Debug.WriteLine(sprintf "Unknown route: %A" r)
            with | _ -> System.Diagnostics.Debug.WriteLine("Error processing route") )