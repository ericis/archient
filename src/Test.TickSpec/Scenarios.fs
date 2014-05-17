namespace Archient.Testing

module Scenarios =
    
    open System.IO
    open System.Reflection

    open TickSpec

    /// Generates the scenarios defined in the file path using the step definitions contained in the assembly of the given type
    let generate (scenarioFilePath:string) (assembly:Assembly) =
        (StepDefinitions(assembly)).GenerateScenarios(scenarioFilePath)

    /// Generates the scenarios defined in the file path using the step definitions contained in the assembly of the given type
    let generateWithStepsOfType<'T> (scenarioFilePath:string) =
        generate scenarioFilePath typeof<'T>.Assembly