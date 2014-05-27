namespace Archient.Web.Http

module StartupTasks = 
    (*
        ASP.NET has evolved quite substantially to the current 4.5.2 framework along with versions 
        5.1.2 of MVC and Web API.
        
        As a result, there are a variety of methods to get things done, including "the old way" 
        and "the new way" and everything in between.  The influence of many open-source libraries 
        has also 'pulled' the API in different directions.

        The next version "vNext" is set to clean things up a bit.
        
        One of the goals for the Archient libraries and guidance is to isolate you and your 
        applications from this change.  It's a lofty goal with a moving foundation, but we're 
        aiming high.
    *)

    open System
    open System.Web.Http
    
    type ApiRouteDefaults = 
        { id : RouteParameter }
    
    let registerDefaultHttpRoutes() = 
        
        // configure global
        Configuration.initializeGlobalConfiguration (fun configuration -> 
            
            // map routes based on attributes
            ignore <| Configuration.mapAttributeRoutes configuration
            
            // map Web API Default route
            let httpDefaults = { id = RouteParameter.Optional }
            
            configuration.Routes
            |> Routes.mapHttp "DefaultApi" "api/{controller}/{id}" httpDefaults
            |> ignore)