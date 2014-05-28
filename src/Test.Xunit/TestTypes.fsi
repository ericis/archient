namespace Archient.Testing

open Xunit

/// Associates the code with unit testing.
[<Class>]
type UnitTestAttribute =
    inherit TraitAttribute
    
    /// Default constructor
    new : unit -> UnitTestAttribute
    
/// Associates the code with integration testing.
[<Class>]
type IntegrationTestAttribute =
    inherit TraitAttribute
    
    /// Default constructor
    new : unit -> IntegrationTestAttribute
    
/// Associates the code with functional testing.
[<Class>]
type FunctionalTestAttribute =
    inherit TraitAttribute
    
    /// Default constructor
    new : unit -> FunctionalTestAttribute
    
/// Associates the code with acceptance testing.
[<Class>]
type AcceptanceTestAttribute =
    inherit TraitAttribute
    
    /// Default constructor
    new : unit -> AcceptanceTestAttribute
    
/// Associates the code with system testing.
[<Class>]
type SystemTestAttribute =
    inherit TraitAttribute
    
    /// Default constructor
    new : unit -> SystemTestAttribute
    
/// Associates the code with tests as examples.
[<Class>]
type ExampleTestAttribute =
    inherit TraitAttribute
    
    /// Default constructor
    new : unit -> ExampleTestAttribute