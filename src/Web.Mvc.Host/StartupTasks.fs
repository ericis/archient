namespace Archient.Web.Mvc

module StartupTasks = 

    open System.Web.Mvc
    open Archient.Web.Mvc

    let registerDefaultAreas() = 
        Areas.registerAll() 
        |> ignore
    
    let registerDefaultFilters() =
        Filters.getGlobalFilters()
        |> Filters.addFilter (HandleErrorAttribute())
        |> ignore

    let registerDefaultMvcRoutes controllerNamespaces = 
        let controllerDefaults = 
            {
                Archient.Web.Mvc.Routes.ControllerDefaults.controller = "Home"
                Archient.Web.Mvc.Routes.ControllerDefaults.action = "Index"
                Archient.Web.Mvc.Routes.ControllerDefaults.id = System.Web.Mvc.UrlParameter.Optional
            }
        
        Routes.getGlobalRoutes()
        |> Routes.ignore "{resource}.axd/{*pathInfo}"
        |> Routes.mapToNamespaces "Default" "{controller}/{action}/{id}" controllerDefaults (controllerNamespaces |> Seq.toArray)
        |> ignore