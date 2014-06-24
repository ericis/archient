namespace Archient.Web

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

    open Archient.Web.Mvc
    open Archient.Web.Http
    
    let getStartupTasks() = 
        [
            // MVC Areas
            StartupTasks.registerDefaultAreas
            
            //// Facebook

            // Web API Routes
            fun () -> 
                StartupTasks.registerDefaultHttpRoutes [ typeof<PagesController>.Namespace ]
            
            // MVC Filters
            StartupTasks.registerDefaultFilters
            
            // MVC Routes
            fun () -> 
                StartupTasks.registerDefaultMvcRoutes [ typeof<HomeController>.Namespace ]

            //// Web Optimization Bundling

            fun () ->
                Routes.prettyPrint System.Web.Routing.RouteTable.Routes
        ]