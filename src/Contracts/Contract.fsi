namespace Archient.Contracts

/// <summary>F# Contract utility library</summary>
module Contract =
    
    /// <summary>Creates a value provider strategy</summary>
    /// <typeparam name="ctx">The type of context to evaluate when providing a value</typeparam>
    /// <typeparam name="t">The type of value being provided</typeparam>
    /// <param name="canProvide">Function to determine if a value can be provided</param>
    /// <param name="getValue">Function retrieve a value</param>
    /// <returns>A new instance of a value provider strategy</returns>
    val createValueProviderStrategy<'ctx,'t> : canProvide:('ctx->bool) -> getValue:('ctx->'t) -> IValueProviderStrategy<'ctx,'t>