namespace Archient.Razor

[<AllowNullLiteral>]
type IRazorTemplate =
    abstract member ExecuteAsync : unit -> System.Threading.Tasks.Task
    abstract member Write : value:obj -> unit
    abstract member WriteLiteral : value:obj -> unit

[<AbstractClass; AllowNullLiteral>]
type TemplateBase =
    
    abstract member ExecuteAsync : unit -> System.Threading.Tasks.Task
    
    abstract member Write : value:obj -> unit
    abstract member WriteLiteral : value:obj -> unit

    interface IRazorTemplate

[<AbstractClass; AllowNullLiteral>]
type SimpleStringTemplate =
    inherit TemplateBase