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
    open System.Web.Hosting

    open Archient.Web.Mvc
    open Archient.Web.Http
    
    let getStartupTasks() = 
        [
            fun () ->
                
                let viewEngines = ViewEngines.Engines
                
                viewEngines.Clear()
                //viewEngines.Remove(viewEngines.OfType<WebFormViewEngine>().First())
                viewEngines.Add(MyViewEngine())
                //viewEngines.Add(RazorViewEngine())

                System.Diagnostics.Debug.WriteLine("RegisterVirtualPathProvider([VirtualViewPathProvider])-start")
                HostingEnvironment.RegisterVirtualPathProvider(VirtualViewPathProvider())
                System.Diagnostics.Debug.WriteLine("RegisterVirtualPathProvider([VirtualViewPathProvider])-end")
                
                ()

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

            // Web Optimization Bundling
            fun () ->
                System.Diagnostics.Debug.WriteLine(Routes.prettyPrint System.Web.Routing.RouteTable.Routes)
        ]