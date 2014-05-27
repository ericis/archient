namespace Archient.Web

open System

type ArchientApp() =
    inherit WebApp()

    override me.GetStartupTasks() =
        StartupTasks.getStartupTasks()
        // public API expects System.Action for CLR compatibility
        |> Seq.map (fun task -> Action(task))