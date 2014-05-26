namespace Archient.Testing

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