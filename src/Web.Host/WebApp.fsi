namespace Archient.Web

open System
open System.Collections.Generic
open System.Web

/// Web application base implementation
type WebApp =
    inherit HttpApplication

    /// Default constructor
    new : unit -> WebApp

    /// Retrieves a collection of application startup tasks
    abstract member GetStartupTasks : unit -> IEnumerable<Action>
    default GetStartupTasks : unit -> IEnumerable<Action>

    /// Called when the application starts
    abstract member OnStart : unit -> unit
    default OnStart : unit -> unit
    
    /// Called when an unexpected application error occurs
    abstract member OnError : exn -> unit
    default OnError : exn -> unit
    
    /// ASP.NET application event delegate for application start
    abstract member Application_Start : sender:obj*e:EventArgs -> unit
    default Application_Start : sender:obj*e:EventArgs -> unit
    
    /// ASP.NET application event delegate for application errors
    abstract member Application_Error : sender:obj*e:EventArgs -> unit
    default Application_Error : sender:obj*e:EventArgs -> unit