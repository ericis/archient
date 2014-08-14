namespace Archient.Web

// Documentation is provided in Signature File (.fsi)

open System
open System.Collections.Generic
open System.Web
open System.Diagnostics

type WebApp() =
    inherit HttpApplication()

    abstract member GetStartupTasks : unit -> IEnumerable<Action>
    default me.GetStartupTasks() = Seq.empty<_>

    abstract member OnStart : unit -> unit
    default me.OnStart() = 
        me.GetStartupTasks()
        |> Seq.iter (fun action -> action.Invoke())

    abstract member OnError : exn -> unit
    default me.OnError(error) = 
        if error <> null then
            Debug.WriteLine(error.Message)
            Debug.WriteLine(error.ToString())
            Trace.TraceError(error.Message)
        ()

    abstract member Application_Start : sender:obj*e:EventArgs -> unit
    default me.Application_Start(sender:obj, e:EventArgs) =
        me.OnStart()
        
    abstract member Application_Error : sender:obj*e:EventArgs -> unit
    default me.Application_Error(sender:obj, e:EventArgs) =
        let error = me.Server.GetLastError()
        me.OnError(error)