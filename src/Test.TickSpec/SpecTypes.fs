namespace Archient.Testing

open System

open Xunit
open Xunit.Sdk
open TickSpec

/// Annotation for methods generating TickSpec scenarios
[<AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)>] 
type SpecAttribute() = 
    inherit FactAttribute()

    override this.EnumerateTestCommands (info:IMethodInfo) =
        let mi = info.MethodInfo
        let scenarios = mi.Invoke(null, null) :?> seq<Scenario> 
        scenarios
        |> Seq.map (fun scenario -> Command.createTestCommand scenario info)
        |> Seq.cast