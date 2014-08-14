namespace Archient.Web.Mvc

module Filters =
    
    open System.Web.Mvc

    let getGlobalFilters () = 
        GlobalFilters.Filters

    let addFilter (filter:obj) (filters:GlobalFilterCollection) = 
        filters.Add(filter)
        filters