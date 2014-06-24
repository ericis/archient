namespace Archient.Web

open System.Web.Http

type PagesController() =
    inherit ApiController()

    member me.Get() =
        [|1..10|]