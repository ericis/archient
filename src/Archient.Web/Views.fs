namespace Archient.Web

module Views =
    let private root = "~/Views"

    module Shared =
        let private root = root + "/Shared"
        
        let private Site = root + "/Site.cshtml"

        let Archient = Site
        let Eric = Site
        let Unknown = Site