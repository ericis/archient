namespace Archient.Web

module ViewModels =
    let create header body footer =
        PageViewModel(header, body, footer)