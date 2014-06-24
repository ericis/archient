namespace Archient.Web.Http

module StartupTasks = 
    
    open System
    open System.Web.Http
    
    type ApiRouteDefaults = 
        { id : RouteParameter }
    
    let registerDefaultHttpRoutes controllerNamespaces = 
        
        // configure global
        Configuration.initializeGlobalConfiguration (fun configuration -> 
            
            // map routes based on attributes
            ignore <| Configuration.mapAttributeRoutes configuration
            
            // map Web API Default route
            let httpDefaults = { id = RouteParameter.Optional }
            
            configuration.Routes
            |> Routes.mapHttpToNamespaces "DefaultApi" "api/{controller}/{id}" httpDefaults controllerNamespaces
            |> ignore)