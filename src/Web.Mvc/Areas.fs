namespace Archient.Web.Mvc

module Areas =
    open System.Web.Mvc
    
    let registerAll() =
        AreaRegistration.RegisterAllAreas()