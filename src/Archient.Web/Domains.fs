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

        if uhost.EndsWith("ERICIS.COM") || uhost.EndsWith("ISERIC.COM") then (host, PrimaryDomain.Eric)
        else if uhost.EndsWith("ARCHIENT.COM") then (host, PrimaryDomain.Archient)
        else (uhost, PrimaryDomain.Unknown)