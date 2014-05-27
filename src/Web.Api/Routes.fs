namespace Archient.Web.Http

module Routes =
    open System.Web.Http
    
    let mapHttp name routeTemplate (defaults:obj) (routes:HttpRouteCollection) =
        let route = routes.MapHttpRoute(name, routeTemplate, defaults)

        routes