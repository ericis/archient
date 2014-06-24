namespace Archient.Web.Http

module Routes =
    open System.Web.Http
    
    let mapHttp name routeTemplate (defaults:obj) (routes:HttpRouteCollection) =
        let route = routes.MapHttpRoute(name, routeTemplate, defaults)

        routes

    let mapHttpToNamespaces name routeTemplate (defaults:obj) (namespaces:seq<string>) (routes:HttpRouteCollection) =
        let route = routes.MapHttpRoute(name, routeTemplate, defaults)
//
//        let nsKey = "Namespaces"
//
//        match route.DataTokens.ContainsKey(nsKey) with
//        | true -> route.DataTokens.[nsKey] <- namespaces |> Seq.toArray
//        | false -> route.DataTokens.Add(nsKey, namespaces |> Seq.toArray)

        routes