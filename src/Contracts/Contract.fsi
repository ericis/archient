namespace Archient.Contracts

/// F# Contract utility library
module Contract =
    
    /// <summary>Creates a value provider strategy</summary>
    /// <typeparam name="ctx">The type of context to evaluate when providing a value</param>
    /// <typeparam name="t">The type of value being provided</param>
    /// <param name="canProvide">Function to determine if a value can be provided</param>
    /// <param name="getValue">Function retrieve a value</param>
    /// <returns>A new instance of a value provider strategy</returns>
    val createValueProviderStrategy<'ctx,'t> : canProvide:('ctx->bool) -> getValue:('ctx->'t) -> IValueProviderStrategy<'ctx,'t>