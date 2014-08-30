namespace Archient.Razor

[<AllowNullLiteral>]
type IRazorTemplate =
    abstract member ExecuteAsync : unit -> System.Threading.Tasks.Task
    abstract member Write : value:obj -> unit
    abstract member WriteLiteral : value:obj -> unit

[<AbstractClass; AllowNullLiteral>]
type TemplateBase() =
    
    abstract member ExecuteAsync : unit -> System.Threading.Tasks.Task
    
    abstract member Write : value:obj -> unit
    default me.Write(_) = ()

    abstract member WriteLiteral : value:obj -> unit
    default me.WriteLiteral(_) = ()

    interface IRazorTemplate with
        override me.ExecuteAsync() = me.ExecuteAsync()
        override me.Write(value) = me.Write(value)
        override me.WriteLiteral(value) = me.WriteLiteral(value)

[<AbstractClass; AllowNullLiteral>]
type SimpleStringTemplate() =
    inherit TemplateBase()

    let content = new System.Text.StringBuilder()

    override me.Write(value) = ignore <| content.Append(string value)
    override me.WriteLiteral(value) = ignore <| content.Append(string value)
    override me.ToString() = content.ToString()