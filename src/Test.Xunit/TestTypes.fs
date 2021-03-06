﻿namespace Archient.Testing

// Documentation is provided in Signature File (.fsi)

open Xunit

type UnitTestAttribute() =
    inherit TraitAttribute("Test Type", "Unit")

type IntegrationTestAttribute() =
    inherit TraitAttribute("Test Type", "Integration")

type FunctionalTestAttribute() =
    inherit TraitAttribute("Test Type", "Functional")

type AcceptanceTestAttribute() =
    inherit TraitAttribute("Test Type", "Acceptance")

type SystemTestAttribute() =
    inherit TraitAttribute("Test Type", "System")

type ExampleTestAttribute() =
    inherit TraitAttribute("Test Type", "Example")