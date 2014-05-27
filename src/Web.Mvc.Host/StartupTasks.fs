namespace Archient.Web.Mvc

module StartupTasks = 

    open System
    open System.Web.Mvc
    open Archient.Web.Mvc
    
    type ControllerRouteDefaults = 
        { controller : string
          action : string
          id : System.Web.Mvc.UrlParameter }
    
    let registerDefaultAreas() = 
        Areas.registerAll() 
        |> ignore
    
    let registerDefaultFilters() =
        Filters.getGlobalFilters()
        |> Filters.addFilter (HandleErrorAttribute())
        |> ignore

    let registerDefaultMvcRoutes controllerNamespaces = 
        let controllerDefaults = 
            { controller = "Home"
              action = "Index"
              id = System.Web.Mvc.UrlParameter.Optional }
        
        Routes.getGlobalRoutes()
        |> Routes.ignore "{resource}.axd/{*pathInfo}"
        |> Routes.mapToNamespaces "Default" "{controller}/{action}/{id}" controllerDefaults (controllerNamespaces |> Seq.toArray)
        |> ignore