namespace Eric.Web.ViewModels

type TopNavigationItem(title:string, url:string) =
    member me.Title = title
    member me.Url = url

type TopNavigation(items:TopNavigationItem[]) =
    member me.Items = items