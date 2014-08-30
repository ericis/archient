namespace Archient.Razor

module RazorTests =
    
    open System.Linq

    open Xunit
    open Archient.Testing
    open System.CodeDom.Compiler

    open Razor

    let getCode() =
        System.IO.File.ReadAllText("RazorTemplate.cs")

    let [<Fact; UnitTest>] ``Razor.map``() =

        let code = mapTemplateToCode "Archient.Razor.SimpleStringTemplate" [|"Archient.Razor"|] "Hello world! 1 + 1 = @(1+1)"

        code |> Assert.isNotNull

        code.ParserErrors.Count |> Assert.areEqual 0

        printfn "%s" code.GeneratedCode

        // eliminate all whitespace and confirm the generated code is expected
        let ws = System.Text.RegularExpressions.Regex("\s")
        let actualCode = ws.Replace(code.GeneratedCode, "")
        let expectedCode = ws.Replace(getCode(), "")

        actualCode |> Assert.areEqual expectedCode

    let [<Fact; UnitTest>] ``Razor.compile``() =
        
        let location = typeof<SimpleStringTemplate>.Assembly.Location
        printfn "Location: %s" location

        let compiled = compile [|location|] [|(getCode())|]

        compiled |> Assert.isNotNull

        compiled.Errors.Cast<CompilerError>()
        |> Seq.iter (fun err -> printfn "Error %s (%d, %d): %s" err.ErrorNumber err.Line err.Column err.ErrorText)

        compiled.Errors.Count |> Assert.areEqual 0

        let template = createAndExecute<SimpleStringTemplate> compiled

        template.ToString() |> Assert.areEqual "Hello world! 1 + 1 = 2"

    let [<Fact; UnitTest>] ``Razor.end-to-end``() =
        
        let runView view =
            compileAndExecute<SimpleStringTemplate> "Archient.Razor.SimpleStringTemplate" [|"Archient.Razor"|] [|typeof<SimpleStringTemplate>.Assembly.Location|] view
        
        runView "Hello world! 1 + 1 = @(1+1)"
        |> Assert.areEqual "Hello world! 1 + 1 = 2"

        runView "@(\"Hello\" + \" world\")! 14 * 3 / 2 = @(14*3/2)"
        |> Assert.areEqual "Hello world! 14 * 3 / 2 = 21"