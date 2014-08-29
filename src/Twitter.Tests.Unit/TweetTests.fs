namespace Archient.Twitter

module TweetTests =
    
    open System.Linq
    
    open Xunit
    open Archient.Testing

    let [<Fact>]``tada``() =
        
        Assert.isTrue true

// Integration test (requires connectivity and valid API keys)
//        let twitter = Tweet.getService ("[key]","[secret]")
//        let tweets = Tweet.search "@ericis" twitter
//
//        tweets.Length |> Assert.isGT 0
//        (tweets.First()) |> Assert.areEqual "tada"