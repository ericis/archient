namespace Archient.Web

open System
open System.Collections.Generic
open System.Web

type WebApp() =
    inherit HttpApplication()

    abstract member GetStartupTasks : unit -> IEnumerable<Action>
    default me.GetStartupTasks() = Seq.empty<_>

    abstract member OnStart : unit -> unit
    default me.OnStart() = 
        me.GetStartupTasks()
        |> Seq.iter (fun action -> action.Invoke())

    member me.Application_Start(sender:obj, e:EventArgs) =
        me.OnStart()