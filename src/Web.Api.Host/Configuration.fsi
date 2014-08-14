namespace Archient.Web.Http

/// F# HTTP Configuration library
module Configuration =
    open System
    open System.Web.Http
    
    //// Using 'GlobalConfiguration.Configuration' to configure at start-up causes conflicts between Web API and MVC routing.
    //// Not sure why, but disabling this function to help others avoid the same.
    ////    let getGlobalConfiguration() = 
    ////        GlobalConfiguration.Configuration
    
    /// <summary>Initializes the HTTP Global Configuration</summary>
    /// <param name="initialization">The initialization action.</param>
    val initializeGlobalConfiguration : initialization:(HttpConfiguration->unit) -> unit
    
    /// <summary>Maps HTTP routes that are applied as attributes in the code.</summary>
    /// <param name="configuration">The configuration to map the routes to.</param>
    /// <returns>The modified configuration.</returns>
    val mapAttributeRoutes : configuration:HttpConfiguration -> HttpConfiguration