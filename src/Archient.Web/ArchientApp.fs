namespace Archient.Web

open System
open Archient.Web.Mvc
open Archient.Web.Http

type ControllerRouteDefaults = { controller: string; action: string; id: System.Web.Mvc.UrlParameter }
type ApiRouteDefaults = { id: System.Web.Http.RouteParameter }

type ArchientApp() =
    inherit WebApp()

    let registerAreas() =
        Areas.registerAll()
        |> ignore

    let registerMvcRoutes() =
        let controllerDefaults = { controller = "Home"; action = "Index"; id = System.Web.Mvc.UrlParameter.Optional }

        let namespaces = [typeof<HomeController>.FullName]

        Routes.getGlobalRoutes()
        |> Routes.ignore "{resource}.axd/{*pathInfo}"
        |> Routes.mapToNamespaces "Default" "{controller}/{action}/{id}" controllerDefaults namespaces
        |> ignore

    let registerHttpRoutes() =
        
        let configuration =
            Configuration.getGlobalConfiguration()
            |> Configuration.mapAttributeRoutes

        let httpDefaults = { id = System.Web.Http.RouteParameter.Optional }

        configuration.Routes
        |> Routes.mapHttp "DefaultApi" "api/{controller}/{id}" httpDefaults
        |> ignore

    override me.GetStartupTasks() =
        [
            Action(fun _ -> registerAreas())
            Action(fun _ -> registerHttpRoutes())
            Action(fun _ -> registerMvcRoutes())
        ]
        |> List.toSeq