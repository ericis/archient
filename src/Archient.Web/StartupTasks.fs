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

    open System.Web.Mvc

    open Archient.Contracts
    open Archient.Web.Mvc
    open Archient.Web.Http
    
    let getStartupTasks() = 
        [
            //// Virtualize MVC Views
            fun () ->
                let virtualViewPrefix = "@inherits System.Web.Mvc.WebViewPage<dynamic>\n"

                let cmsViewProviderStrategy =
                    Contract.createValueProviderStrategy<string,IVirtualFile>
                        (fun path -> path.Contains("/cms/"))
                        (fun path -> 
                            let virtualContent =
                                let name = System.IO.Path.GetFileNameWithoutExtension(path.Trim()).ToLowerInvariant()
                                match name with
                                | "helloworld" -> "<strong>Hello virtual world!</strong>"
                                | "footer" -> "<strong>--FOOTER--</strong>"
                                | _ -> ""
                            Virtual.createFileFromString (virtualViewPrefix + virtualContent) path)

                let testViewProviderStrategy =
                    Contract.createValueProviderStrategy<string,IVirtualFile>
                        (fun path -> path.Contains("/virtual/"))
                        (fun path -> 
                            let virtualContent =
                                let name = System.IO.Path.GetFileNameWithoutExtension(path.Trim()).ToLowerInvariant()
                                match name with
                                | "tada" -> "<strong>Tada!</strong>"
                                | _ -> ""
                            Virtual.createFileFromString (virtualViewPrefix + virtualContent) path)

                let virtualViewProviderStrategies =
                    [|cmsViewProviderStrategy; testViewProviderStrategy|]

                ViewEngines.Engines
                |> Virtual.virtualizeViews
                    (fun path -> virtualViewProviderStrategies |> Seq.exists (fun provider -> provider.CanProvideValue(path)))
                    (fun path -> 
                        let strategy =
                            virtualViewProviderStrategies
                            |> Seq.find (fun provider -> provider.CanProvideValue(path))
                        strategy.GetValue(path))
                |> ignore

            //// MVC Areas
            StartupTasks.registerDefaultAreas
            
            //// Facebook

            //// Web API Routes
            fun () -> 
                StartupTasks.registerDefaultHttpRoutes [ typeof<PagesController>.Namespace ]
            
            //// MVC Filters
            StartupTasks.registerDefaultFilters
            
            //// MVC Routes
            fun () -> 
                let controllerDefaults = 
                    {
                        Archient.Web.Mvc.Routes.ControllerDefaults.controller = "Home"
                        Archient.Web.Mvc.Routes.ControllerDefaults.action = "Index"
                        Archient.Web.Mvc.Routes.ControllerDefaults.id = System.Web.Mvc.UrlParameter.Optional
                    }
                
                Routes.getGlobalRoutes()
                |> Routes.ignore "{resource}.axd/{*pathInfo}"
                |> Routes.mapToNamespaces "Default" "{*pathInfo}" controllerDefaults ([ typeof<HomeController>.Namespace ] |> Seq.toArray)
                |> ignore

                //StartupTasks.registerDefaultMvcRoutes [ typeof<HomeController>.Namespace ]

            //// Web Optimization Bundling
            
            //// Debug Routes
            fun () ->
                System.Diagnostics.Debug.WriteLine(Routes.prettyPrint System.Web.Routing.RouteTable.Routes)
        ]