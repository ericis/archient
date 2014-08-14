namespace Archient.Web.Mvc

/// F# application startup tasks library
module StartupTasks = 
    
    /// Registers the default MVC Areas with the web application
    val registerDefaultAreas : unit -> unit
    
    /// Registers the default MVC Filters with the web application
    val registerDefaultFilters : unit -> unit
    
    /// Registers the default MVC Routes with the web application
    val registerDefaultMvcRoutes : seq<string> -> unit