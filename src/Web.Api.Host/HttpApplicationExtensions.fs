namespace Archient.Web.Http

open System
open System.Runtime.CompilerServices
open System.Web
open System.Web.Http

/// Extensions to the <cref see="HttpApplication" /> class.
[<Sealed; Extension>]
type HttpApplicationExtensions() =
    
    /// <summary>Initializes the HTTP configuration.</summary>
    /// <param name="application">The web application.</param>
    /// <param name="initialization">The initialization action to setup the HTTP Configuration.</param>
    [<Extension>]
    static member InitializeHttpConfiguration(application:HttpApplication, initialization:Action<_>) =
        Configuration.initializeGlobalConfiguration(fun configuration -> initialization.Invoke(configuration))
    
    /// <summary>Maps HTTP routes that are applied as attributes in the code.</summary>
    /// <param name="configuration">The configuration to map the routes to.</param>
    /// <returns>The modified configuration.</returns>
    [<Extension>]
    static member MapAttributeRoutes(configuration:HttpConfiguration) =
        ignore <| configuration.MapHttpAttributeRoutes()
        configuration