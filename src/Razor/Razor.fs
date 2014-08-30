namespace Archient.Razor

module Razor =
    open System.CodeDom.Compiler

    open Microsoft.AspNet.Razor
    open Microsoft.AspNet.Razor.Text

    let private templateNamespace = "Razor.Templates"
    let private templateClassName = "RazorTemplate"

    let private getStrBuffer (content:string) =
        
        let buffer = ref content
        let position = ref 0

        {
            new ITextBuffer with
                
                override me.Length
                    with get() = buffer.Value.Length
                
                override me.Position
                    with get() = position.Value
                    and set(value) = position := value
                
                override me.Read() =

                    if position.Value >= buffer.Value.Length then
                        -1
                    else
                        let pos = position.Value
                        let value = buffer.Value.[pos]
                        position := pos + 1
                        int value
                
                override me.Peek() =

                    if position.Value >= buffer.Value.Length then
                        -1
                    else
                        int buffer.Value.[position.Value]
        }

    let mapTemplateToCode baseClass namespaces content =
        let engine = 
            let host = 
                RazorEngineHost(
                    CSharpRazorCodeLanguage(), 
                    DesignTimeMode = false,
                    DefaultNamespace = templateNamespace,
                    DefaultBaseClass = baseClass,
                    DefaultClassName = templateClassName,
                    StaticHelpers = false)

            namespaces
            |> Seq.iter (fun ns -> ignore <| host.NamespaceImports.Add(ns))

            RazorTemplateEngine(host)
        
        let buffer = getStrBuffer content

        engine.GenerateCode(buffer)

    let compile referencedAssemblyLocations code =
        use csc = CodeDomProvider.CreateProvider("CSharp")
        let options = CompilerParameters(GenerateInMemory = true, TreatWarningsAsErrors = false)
        
        referencedAssemblyLocations
        |> Seq.iter (fun loc -> ignore <| options.ReferencedAssemblies.Add(loc))

        csc.CompileAssemblyFromSource(options, code |> Seq.toArray)

    let createAndExecute<'t when 't :> IRazorTemplate and 't : null> (results:CompilerResults) =
        match results.CompiledAssembly.CreateInstance(sprintf "%s.%s" templateNamespace templateClassName) with
        | :? 't as instance ->
            instance.ExecuteAsync().Wait()
            instance
        | _ ->
            null
        
    let compileAndExecute<'t when 't :> IRazorTemplate and 't : null> baseClass namespaces referencedAssemblyLocations content =
        let code = mapTemplateToCode baseClass namespaces content
        let compiled = compile referencedAssemblyLocations [|code.GeneratedCode|]
        let template = createAndExecute<'t> compiled
        template.ToString()