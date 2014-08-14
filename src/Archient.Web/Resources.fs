namespace Archient.Web

module Resources =
    open System.Resources

    type private IResx = interface end

    let private thisAssembly = typeof<IResx>.Assembly

    let private resx = 
        ResourceManager("Resources", thisAssembly)

    let private resxOther = 
        ResourceManager("UnknownWebsiteResources", thisAssembly)

    let internal getString name =
        resx.GetString(name)

    let internal getOtherString name =
        resxOther.GetString(name)

    module Archient =
        module Home =
            let Title = getString "Archient.Home.Title"

    module Unknown =
        module Home =
            let Title = getOtherString "Unknown.Home.Title"