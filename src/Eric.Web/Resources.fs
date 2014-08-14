namespace Eric.Web

module Resources =
    open System.Resources

    type private IResx = interface end

    let private thisAssembly = typeof<IResx>.Assembly

    let private resx = 
        ResourceManager("Resources", thisAssembly)

    let internal getString name =
        resx.GetString(name)

    module Eric =
        module Home =
            let Title = getString "Eric.Home.Title"
        module Twitter =
            let HandleUrlFormat = getString "Eric.Twitter.HandleUrlFormat"
            let Handle = getString "Eric.Twitter.Handle"
            let Title = getString "Eric.Twitter.Title"
        module LinkedIn =
            let Url = getString "Eric.LinkedIn.Url"
            let Title = getString "Eric.LinkedIn.Title"