namespace Archient.Web.Http

module Configuration =
    open System
    open System.Web.Http
    
//// Using 'GlobalConfiguration.Configuration' to configure at start-up causes conflicts between Web API and MVC routing.
//// Not sure why, but disabling this function to help others avoid the same.
////    let getGlobalConfiguration() = 
////        GlobalConfiguration.Configuration
    
    let initializeGlobalConfiguration initialization =
        GlobalConfiguration.Configure(Action<_>(initialization))

    let mapAttributeRoutes (configuration:HttpConfiguration) = 
        configuration.MapHttpAttributeRoutes()
        configuration