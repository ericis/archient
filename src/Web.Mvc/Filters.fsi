namespace Archient.Web.Mvc

/// F# MVC Filters library
module Filters =
    
    open System.Web.Mvc

    /// Retrieves the MVC Filters from global scope
    val getGlobalFilters : unit -> GlobalFilterCollection

    /// Adds the specified filter to the filter collection and returns the modified collection
    val addFilter : filter:obj -> filters:GlobalFilterCollection -> GlobalFilterCollection