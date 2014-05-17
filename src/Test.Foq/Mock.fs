namespace Archient.Testing

module Mock =
    
    open Microsoft.FSharp.Quotations

    open Foq

    let private expect (op:'t->Expr<'ret>) (times:Times) (value:'t) =
        let expectation = op value :> Expr

        Mock.Expect(expectation, times)
    
    let build<'t when 't : not struct> () =
        let mock = Mock<'t>(MockMode.Strict)
        
        mock, list<'t->unit>.Empty

    let call<'t,'ret when 't : not struct> (op:'t->Expr<'ret>) (result:'ret) (times:Times) (mockery:Mock<'t>*list<'t->unit>) =
        let mock, expectations = mockery

        let mock = mock.Setup(op).Returns(result)

        let expectation value = 
            expect op times value

        mock, (expectation :: expectations)

    let instance (mockery:Mock<'t>*seq<'t->unit>) =
        let mock, expectations = mockery
        let value = mock.Create()
        expectations |> Seq.iter (fun expect -> expect value)
        value