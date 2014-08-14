namespace Archient.Web.Mvc

open System
open System.Runtime.CompilerServices
open System.Web
open Archient.Web.Mvc

/// Extensions to an HTTP Application
[<Sealed; Extension>]
type HttpApplicationExtensions() = 
    
    /// <summary>Registers MVC Areas with the application</summary>
    /// <param name="app">The HTTP Application</param>
    /// <returns>Returns the modified application</returns>
    [<Extension>]
    static member RegisterAreas(app:HttpApplication) =
        Areas.registerAll()
        app
        
    /// <summary>Retrieves the application's routes</summary>
    /// <param name="app">The HTTP Application</param>
    /// <returns>The application's routes</returns>
    [<Extension>]
    static member GetRoutes(app:HttpApplication) =
        Routes.getGlobalRoutes()