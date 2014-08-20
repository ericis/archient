namespace Archient.Twitter

module TweetTests =
    
    open System.Linq
    
    open Xunit
    open Archient.Testing

    let [<Fact>]``tada``() =
        
        let tweets = Tweet.search "@ericis"

        tweets.Length |> Assert.isGT 0
        (tweets.First()) |> Assert.areEqual "tada"