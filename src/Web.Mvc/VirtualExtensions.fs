namespace Archient.Web.Mvc

open System.Runtime.CompilerServices

[<Extension>]
module VirtualFileExtensions =
    open System
    open System.Web.Hosting
    open System.Web.Mvc

    [<Extension>]
    let ToWebVirtualFile(virtualFile:IVirtualFile) =
        Virtual.toWebVirtualFile virtualFile
        
    [<Extension>]
    let ToVirtualFile(virtualFile:System.Web.Hosting.VirtualFile) =
        Virtual.toVirtualFile virtualFile

    [<Extension>]
    let ToWebVirtualFileProvider(virtualFileProvider:IVirtualFileProvider) =
        Virtual.toWebVirtualFileProvider virtualFileProvider
        
    [<Extension>]
    let Virtualize(viewEngines:ViewEngineCollection, onVirtualFileExists:Func<string,bool>, onGetVirtualFile:Func<string,IVirtualFile>) =
        
        let onVirtualFileExists path = onVirtualFileExists.Invoke(path)
        let onGetVirtualFile path = onGetVirtualFile.Invoke(path)

        viewEngines
        |> Virtual.virtualizeViews onVirtualFileExists onGetVirtualFile