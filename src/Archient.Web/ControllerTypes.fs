namespace Archient.Web

open System.Web.Mvc

type HomeController() =
    inherit Controller()

    member me.Index() =
        me.View()