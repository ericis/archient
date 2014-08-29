namespace Archient.Twitter

open System
open LinqToTwitter

type ITwitterService =
    inherit IDisposable

    abstract member Search : TwitterQueryable<Search> with get

module Tweet =
    
    open System.Linq
    open LinqToTwitter

    let getService (key:string,secret:string) =
        let auth =
            let authorizer = ApplicationOnlyAuthorizer(CredentialStore = InMemoryCredentialStore(ConsumerKey = key, ConsumerSecret = secret))
            let task = authorizer.AuthorizeAsync()
            task.Wait()
            authorizer
        
        let twitter = new TwitterContext(auth)

        {
            new ITwitterService with
                override me.Search with get() = twitter.Search

                override me.Dispose() = 
                    twitter.Dispose()
        }

    let search criteria (twitter:ITwitterService) =
        
        let query = 
            twitter.Search.Where(
                fun search -> 
                    search.Type = SearchType.Search && 
                    search.Query = criteria)

        let searchResult = query.FirstOrDefault()

        searchResult.Statuses
        |> Seq.map (fun tweet -> tweet.Text)
        |> Seq.toArray