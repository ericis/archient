namespace Archient.Web

type PageHeaderViewModel(title:string) =
    member me.Title = title

type PageFooterViewModel() =
    class end

type PageViewModel<'THeader,'TBody,'TFooter>(header:'THeader, body:'TBody, footer:'TFooter) =
    member me.Header = header
    member me.Body = body
    member me.Footer = footer