namespace Archient.VisualStudio.Guidance.Templates.Wizards

open System
open System.Collections.Generic
open System.Diagnostics
open System.IO
open System.Threading
open System.Threading.Tasks
open Microsoft.VisualStudio.TemplateWizard

type BlankSolutionWizard() =
    
    let log (message:string) (level:TraceLevel) =
        
        match level with
        | TraceLevel.Off
        | TraceLevel.Verbose -> () // ignore
        | TraceLevel.Warning -> ignore <| System.Windows.MessageBox.Show(message, "Warning", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning)
        | TraceLevel.Error -> ignore <| System.Windows.MessageBox.Show(message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error)
        // Info
        | _ -> ignore <| System.Windows.MessageBox.Show(message, "Information", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information)
    
    let visualStudioAutomation = ref Unchecked.defaultof<EnvDTE.DTE>
    let lastReplacementsDictionary = new Dictionary<string,string>()

    let getSolutionFilePathFromReplacements () =
        match lastReplacementsDictionary.ContainsKey("$solutionrootpath$") with
        | true -> 
            match lastReplacementsDictionary.["$solutionrootpath$"] with
            | emptyString when String.IsNullOrWhiteSpace(emptyString) -> None
            | path -> 
                match Path.HasExtension(path) with
                | true -> Some(path)
                | false -> 
                    lastReplacementsDictionary.["$solutionrootpath$"] <- path + ".sln"
                    Some(path + ".sln")
        | false -> None

    let getSolutionFilePathFallbackFromReplacements() =
        match lastReplacementsDictionary.ContainsKey("$destinationdirectory$") with
        | true -> 
            match lastReplacementsDictionary.["$destinationdirectory$"] with
            | emptyString when String.IsNullOrWhiteSpace(emptyString) -> None
            | path -> 
                match Path.HasExtension(path) with
                | true -> Some(path)
                | false -> 
                    lastReplacementsDictionary.["$destinationdirectory$"] <- path + ".sln"
                    Some(path + ".sln")
        | false -> None

    let getSolutionFilePathFromDTE (dte:EnvDTE.DTE) =
        try
            match dte with
            | null -> None
            | dte ->
                match dte.Solution.FullName with
                | emptyString when String.IsNullOrWhiteSpace(emptyString) -> None
                | name -> 
                    match Path.HasExtension(name) with
                    | true -> Some(name)
                    | false -> Some(name + ".sln")
        with | ex -> None

    let getSolutionFilePath () =
        let solutionPath =
            match getSolutionFilePathFromReplacements() with
            | None -> getSolutionFilePathFromDTE !visualStudioAutomation
            | value -> value
        
        match solutionPath with
        | None -> getSolutionFilePathFallbackFromReplacements()
        | value -> value

    let getSolutionFilePathFromProject (project:EnvDTE.Project) =
        match getSolutionFilePathFromReplacements() with
        | None ->
            let projectSolutionFilePath =
                match project with
                | null -> None
                | _ -> getSolutionFilePathFromDTE project.DTE
            
            match projectSolutionFilePath with
            | None -> getSolutionFilePath()
            | value -> value
        | value -> value

    interface IWizard with

        override me.RunStarted(automationObject, replacementsDictionary, runKind, customParams) = 
            log "RunStarted" TraceLevel.Verbose

            match automationObject with
            | :? EnvDTE.DTE as dte -> visualStudioAutomation := dte
            | _ -> log "Unable to get VS automationObject" TraceLevel.Warning

            lastReplacementsDictionary.Clear()

            replacementsDictionary.Keys
            |> Seq.iter (fun key -> lastReplacementsDictionary.Add(key, replacementsDictionary.[key]))

            match getSolutionFilePath() with
            | None -> log "Unable to get solution path" TraceLevel.Warning
            | Some(solutionPath) -> 
                let solutionDirectoryPath = Path.GetDirectoryName(solutionPath)

                if not (replacementsDictionary.ContainsKey("$solutionrootpath$")) then
                    replacementsDictionary.Add("$solutionrootpath$", solutionDirectoryPath)

//                let runningTemplate =
//                    match customParams with
//                    | null -> None
//                    | emptyArray when emptyArray.Length = 0 -> None
//                    | customParams -> 
//                        match string customParams.[0] with
//                        | emptyValue when String.IsNullOrWhiteSpace(emptyValue) -> None
//                        | paramValue -> Some(paramValue)
//                
//                match runningTemplate with
//                | Some(templateName) when templateName.StartsWith("BlankSolution", StringComparison.CurrentCultureIgnoreCase) ->
//                    match (!visualStudioAutomation).Solution with
//                    | :? EnvDTE.Solution
//                | _ -> ()

            ()

        override me.ShouldAddProjectItem(filePath) = 
            log "ShouldAddProjectItem" TraceLevel.Verbose

            true
        
        override me.BeforeOpeningFile(projectItem) = 
            log "BeforeOpeningFile" TraceLevel.Verbose
            ()

        override me.ProjectItemFinishedGenerating(projectItem) = 
            log "ProjectItemFinishedGenerating" TraceLevel.Verbose

            ()

        override me.ProjectFinishedGenerating(project) = 
            log "ProjectFinishedGenerating" TraceLevel.Verbose

//                    match nuget "update -Self -NonInteractive" with
//                    | 0 -> 
//                    | nugetReturnCode -> log (sprintf "'NuGet.exe update -self' failed with the code: %d" nugetReturnCode) TraceLevel.Error
                
////            match project with
////            | null -> log "project was <null>" TraceLevel.Warning
////            | _ ->
////                let projectName = project.Name
////
////                let moveSolutionItems (targetDirectoryName:string) =
////                    let projectDirectoryPath = Path.GetDirectoryName(project.FullName)
////                    let solutionItemsDirectoryPath = Path.Combine(projectDirectoryPath, "Solution Items")
////
////                    match Directory.Exists(solutionItemsDirectoryPath) with
////                    | false -> log "Project expected to have a 'Solution Items' directory" TraceLevel.Warning
////                    | true ->
////                        
////                        match getSolutionFilePathFromProject project with
////                        | None -> log "Error: Solution path was empty" TraceLevel.Error
////                        | Some(solutionPath) ->
////                            let solutionDirectoryPath =
////                                try Path.GetDirectoryName(solutionPath)
////                                with | ex -> 
////                                    log (sprintf "Error retrieving Solution directory '%s': %s" solutionPath ex.Message) TraceLevel.Error
////                                    ""
////
////                            let targetDirectoryPath = Path.Combine(solutionDirectoryPath, targetDirectoryName)
////                            
////                            match Directory.Exists(targetDirectoryPath) with
////                            | true ->
////                                // move items
////                                Directory.EnumerateFiles(solutionItemsDirectoryPath)
////                                |> Seq.iter (fun solutionItemFileName ->
////                                    let sourceFilePath = Path.Combine(solutionItemsDirectoryPath, solutionItemFileName)
////                                    let targetFilePath = Path.Combine(targetDirectoryPath, solutionItemFileName)
////
////                                    match File.Exists(targetFilePath) with
////                                    | false -> File.Move(sourceFilePath, targetFilePath)
////                                    | true -> 
////                                        log (sprintf "Skipping. Solution item already exists at: %s" targetFilePath) TraceLevel.Warning
////                                        File.Delete(sourceFilePath)
////                                
////                                    )
////                            | false ->
////                                // copy items
////                                let maxTries = 5
////                                let tries = ref 0
////                            
////                                while !tries < maxTries do
////                                    tries := !tries + 1
////                                    try
////                                        Directory.Move(solutionItemsDirectoryPath, targetDirectoryPath)
////                                        tries := maxTries
////                                    with
////                                    | :? IOException as ioEx ->
////                                        log (sprintf "IO Error - copying 'Solution Items': %s" ioEx.Message) TraceLevel.Error
////                                    
////                                        match !tries >= maxTries with
////                                        | true -> log (sprintf "Failed: copy of 'Solution Items': Max attempts #%d" !tries) TraceLevel.Warning
////                                        | false -> 
////                                            log (sprintf "Retry copy of 'Solution Items': #%d" !tries) TraceLevel.Info
////                                            // wait and try again
////                                            Thread.Sleep(500 * !tries)
////                                    | ex ->
////                                        log (sprintf "Unexpected Error - copying 'Solution Items': %s" ex.Message) TraceLevel.Error
////                                        tries := maxTries // stop!
////                
////                match projectName with
////                | projectName when String.IsNullOrWhiteSpace(projectName) ->
////                    log "project name was <null> or empty" TraceLevel.Warning
////                | projectName when String.Equals(projectName, "SolutionItems.Build", StringComparison.CurrentCultureIgnoreCase) ->
////                    moveSolutionItems ".build"
////                | projectName when String.Equals(projectName, "SolutionItems.NuGet", StringComparison.CurrentCultureIgnoreCase) ->
////                    moveSolutionItems ".nuget"
////                | projectName ->
////                    log (sprintf "Unexpected project name: %s" projectName) TraceLevel.Verbose

        override me.RunFinished() = 
            log "RunFinished" TraceLevel.Verbose

            let queueDelayedLongRunningAction (action:unit->unit) (wait:TimeSpan) =
                let delayedAction = fun _ ->
                    Thread.Sleep(wait)
                    action()

                ignore <| Task.Factory.StartNew(
                    delayedAction, 
                    TaskCreationOptions.LongRunning)

            let solutionPath = getSolutionFilePath()

            match solutionPath with
            | None -> log (sprintf "Could not find solution path from the runtime environment") TraceLevel.Error
            | Some(solutionPath) ->

                log (sprintf "Solution: %s" solutionPath) TraceLevel.Info
                
                let solutionName = Path.GetFileNameWithoutExtension(solutionPath)
                let solutionDirectoryPath = Path.GetDirectoryName(solutionPath)
                let solutionSubdirectoryPath = Path.Combine(solutionDirectoryPath, solutionName)

                if (Directory.Exists(solutionSubdirectoryPath)) then
                    log (sprintf "Deleting empty solution sub-directory at location: '%s'" solutionSubdirectoryPath) TraceLevel.Info

                    let deleteDirectory path =
                        if (Directory.Exists(path)) then
                            // attempt delete
                            try Directory.Delete(path)
                            with | :? DirectoryNotFoundException -> () // ignore not found (already gone)

                    let deleteSlnDir() =
                        try
                            deleteDirectory solutionSubdirectoryPath
                        with | ex ->
                            log (sprintf "Failed to delete Solution directory at the location: '%s'" solutionSubdirectoryPath) TraceLevel.Error

                    queueDelayedLongRunningAction deleteSlnDir (TimeSpan.FromSeconds(5.0))

                    ignore <| Task.Factory.StartNew(
                        deleteSlnDir, 
                        TaskCreationOptions.LongRunning)

                let thisDirectoryPath = Path.GetDirectoryName(me.GetType().Assembly.Location)
                let sourceNuGetFilePath = Path.Combine(thisDirectoryPath, "NuGet.exe")

                match File.Exists(sourceNuGetFilePath) with
                | false -> log (sprintf "Failed to find required NuGet.exe file at the location: '%s'" sourceNuGetFilePath) TraceLevel.Error
                | true -> 
                    let solutionDirectoryPath = Path.GetDirectoryName(solutionPath)
                    let nugetDirectoryPath = Path.Combine(solutionDirectoryPath, ".nuget")
                    let targetNuGetFilePath = Path.Combine(nugetDirectoryPath, "NuGet.exe")

//                    if not (Directory.Exists(nugetDirectoryPath)) then
//                        log (sprintf "Creating .nuget sub-directory at location: '%s'" nugetDirectoryPath) TraceLevel.Verbose
//                        ignore <| Directory.CreateDirectory(nugetDirectoryPath)
//
//                    if not (File.Exists(targetNuGetFilePath)) then
//                        log (sprintf "Copying NuGet.exe from '%s' to '%s'" sourceNuGetFilePath targetNuGetFilePath) TraceLevel.Verbose
//                        File.Copy(sourceNuGetFilePath, targetNuGetFilePath)

                    //log "Executing 'NuGet.exe update -self'" TraceLevel.Verbose
                    
                    let runNuGet exePath args workingDirectoryPath =
                        
                        log (sprintf "NuGet.exe %s" args) TraceLevel.Info

                        try
                            use nugetProcess = 
                                let nugetProcessInfo = new ProcessStartInfo(exePath, args)
                            
                                nugetProcessInfo.WorkingDirectory <- workingDirectoryPath

                                nugetProcessInfo.UseShellExecute <- false
                                nugetProcessInfo.RedirectStandardOutput <- true

                                Process.Start(nugetProcessInfo)

                            ignore <| nugetProcess.OutputDataReceived.Subscribe(fun e ->
                                log (sprintf "NuGet.exe output: %s" e.Data) TraceLevel.Info)

                            nugetProcess.BeginOutputReadLine()
                            nugetProcess.WaitForExit()

                            let exitCode = nugetProcess.ExitCode
                            
                            exitCode
                        with | ex ->
                            log (sprintf "Error NuGet.exe %s" ex.Message) TraceLevel.Error
                            -999
                    
                    let nuget args =
                        runNuGet sourceNuGetFilePath args thisDirectoryPath

                    let restoreNuGetPackages () =
                        ignore <| nuget (sprintf "restore \"%s\" -NonInteractive" solutionPath)
                    
                    queueDelayedLongRunningAction restoreNuGetPackages (TimeSpan.FromSeconds(5.0))