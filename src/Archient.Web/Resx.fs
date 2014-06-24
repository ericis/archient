namespace Archient.Web

module Resx =
    open System.Resources

    type private IResx = interface end

    let private thisAssembly = typeof<IResx>.Assembly

    let private resx = 
        ResourceManager("Resources", thisAssembly)

    let internal getString name =
        resx.GetString(name)

module Resources =
    
    open Resx

    module Archient =
        module Home =
            let Title = getString "Archient.Home.Title"