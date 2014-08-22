namespace Archient.Contracts

module ContractTests =
    open Xunit
    open Archient.Testing
    
    open Contract

    let [<Fact; UnitTest>] ``Contract.createValueProviderStrategy``() =
        
        let expectedValue = "expected value"

        let provider =
            createValueProviderStrategy<bool,string>
                (fun is -> is)
                (fun _ -> expectedValue)

        provider.CanProvideValue(true)
        |> Assert.isTrue

        provider.CanProvideValue(false)
        |> Assert.isFalse

        provider.GetValue(true)
        |> Assert.areEqual expectedValue