﻿namespace Archient.Contracts

// Documentation is provided in Signature File (.fsi)

type IPing =
    abstract member Ping : unit -> string

type IHealthCheck<'TResult> =
    abstract member HealthCheck : key:string -> 'TResult

type IHealthCheck =
    inherit IHealthCheck<string>