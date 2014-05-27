namespace Archient.Web.Http

module Configuration =
    open System.Web.Http
    
    let getGlobalConfiguration() = 
        GlobalConfiguration.Configuration
    
    let mapAttributeRoutes (configuration:HttpConfiguration) = 
        configuration.MapHttpAttributeRoutes()
        configuration