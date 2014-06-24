namespace Archient.Web

open System.Web.Mvc

type HomeController() =
    inherit Controller()

    member me.Index() =
        let model = ViewModels.create (PageHeaderViewModel(Resources.Archient.Home.Title)) (box Resources.Archient.Home.Title)
        
        me.View(Views.Shared.Archient, model)