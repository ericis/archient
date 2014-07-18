namespace Archient.Web.Http

// Documentation is provided in Signature File (.fsi)

module Configuration =
    open System
    open System.Web.Http
    
    let initializeGlobalConfiguration initialization =
        GlobalConfiguration.Configure(Action<_>(initialization))

    let mapAttributeRoutes (configuration:HttpConfiguration) = 
        configuration.MapHttpAttributeRoutes()
        configuration