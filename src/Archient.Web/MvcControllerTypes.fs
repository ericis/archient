namespace Archient.Web

module Domains =
    open System

    type PrimaryDomain =
        | Archient
        | Eric
        | Unknown

    let getPrimary (uri:Uri) =
        let host = 
            let hostWithPort = uri.Host
            match hostWithPort.IndexOf(':') with
            | -1 -> hostWithPort
            | portIndex -> hostWithPort.Substring(0, portIndex)

        let uhost = host.ToUpperInvariant()

        if uhost.EndsWith("ERICIS.COM") || uhost.EndsWith("ISERIC.COM") then (host,PrimaryDomain.Eric)
        else if uhost.EndsWith("ARCHIENT.COM") then (host,PrimaryDomain.Archient)
        else (uhost,PrimaryDomain.Unknown)

open System
open System.Web.Mvc
open Eric.Web

type IViewModelProviderStrategy =
    abstract member CanProvide : host:string * primary:Domains.PrimaryDomain -> bool
    abstract member GetViewModel : host:string * primary:Domains.PrimaryDomain -> string*obj

type HomeController() =
    inherit Controller()

    let getArchientStrategy() =
        {
            new IViewModelProviderStrategy with
                override me.CanProvide(host, primary) =
                    primary = Domains.Archient
                
                override me.GetViewModel(host, primary) =
                    let model =
                        let header = PageHeaderViewModel(Resources.Archient.Home.Title)
                        let body = box Resources.Archient.Home.Title
                        let footer = PageFooterViewModel()
                        ViewModels.create header body footer
                    (Views.Shared.Archient, box model)
        }

    let getEricStrategy() =
        {
            new IViewModelProviderStrategy with
                override me.CanProvide(host, primary) =
                    primary = Domains.Eric

                override me.GetViewModel(host, primary) =
                    let model =
                        let topNav = 
                            let twitter = Eric.Web.ViewModels.TopNavigationItem(Resources.Eric.Twitter.Title, String.Format(Resources.Eric.Twitter.HandleUrlFormat, Resources.Eric.Twitter.Handle.Substring(1)))
                            let linkedin = Eric.Web.ViewModels.TopNavigationItem(Resources.Eric.LinkedIn.Title, Resources.Eric.LinkedIn.Url)
                            let topNavItems = [|twitter;linkedin|]
                            Eric.Web.ViewModels.TopNavigation(topNavItems)
                        let header = 
                            let title = Resources.Eric.Home.Title
                            PageHeaderViewModel(title)
                        let body = box topNav
                        let footer = PageFooterViewModel()
                        ViewModels.create header body footer
                    (Views.Shared.Archient, box model)
        }

    let strategies =
        [|getArchientStrategy(); getEricStrategy()|]

    member me.Index() =
        
        let host, primary = Domains.getPrimary(me.Request.Url)

        let viewModelStrategy =
            strategies
            |> Seq.tryFind (fun strategy -> strategy.CanProvide(host, primary))
        
        let view, model = 
            match viewModelStrategy with
            | Some(strategy) ->
                strategy.GetViewModel(host, primary)
            | None ->
                let title = String.Format(Resources.Unknown.Home.Title, host)
                let model = ViewModels.create (PageHeaderViewModel(title)) (box title) (PageFooterViewModel())
                (Views.Shared.Unknown, box model)

        me.View(view, model)