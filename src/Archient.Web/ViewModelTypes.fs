namespace Archient.Web

type PageHeaderViewModel(title:string) =
    member me.Title = title

type PageViewModel<'THead,'TBody>(head:'THead, body:'TBody) =
    member me.Head = head
    member me.Body = body