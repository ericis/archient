namespace Archient.Twitter

module Tweet =
    
    open System.Linq
    open LinqToTwitter

    let search criteria =
        
        let auth = 
            let key, secret = ("[key]","[secret]")
            let authorizer = ApplicationOnlyAuthorizer(CredentialStore = InMemoryCredentialStore(ConsumerKey = key, ConsumerSecret = secret))
            let task = authorizer.AuthorizeAsync()
            task.Wait()
            authorizer

        use twitter = new TwitterContext(auth)
        
        let query = 
            twitter.Search.Where(
                fun search -> 
                    search.Type = SearchType.Search && 
                    search.Query = criteria)

        let searchResult = query.FirstOrDefault()

        searchResult.Statuses
        |> Seq.map (fun tweet -> tweet.Text)
        |> Seq.toArray