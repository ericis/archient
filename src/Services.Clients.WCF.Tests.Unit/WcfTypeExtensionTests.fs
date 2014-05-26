namespace Archient.Services.Clients

module WcfTypeExtensionsTests =
    open Archient.Testing
    open System.ServiceModel
    open Xunit

    let private createSvc () =
        new TestServiceClient()

    let [<Fact>] ``IUseWcfService.Create``() =
        
        let svc = createSvc()

        let container = svc.Create("svc")
        
        container |> Assert.isNotNull