namespace Archient.Web.Mvc

open System.Runtime.CompilerServices

[<Extension>]
module VirtualFileExtensions =
    open System
    open System.Web.Hosting
    open System.Web.Mvc
    
    type private MyVirtualPathProvider(onVirtualFileExists, onGetVirtualFile:string->IVirtualFile) =
        inherit VirtualPathProvider()

        override me.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart) =
            match onVirtualFileExists virtualPath with
            | true -> null
            | false -> base.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart)
            
        override me.FileExists(virtualPath) = 
            match onVirtualFileExists virtualPath with
            | true -> true
            | false -> base.FileExists(virtualPath)
            
        override me.GetFile(virtualPath) = 
            match onVirtualFileExists virtualPath with
            | true -> onGetVirtualFile virtualPath |> Virtual.toWebVirtualFile
            | false -> base.GetFile(virtualPath)

        interface IVirtualFileProvider with
            
            override me.FileExists(virtualPath) = 
                me.FileExists(virtualPath)

            override me.GetFile(virtualPath) = 
                me.GetFile(virtualPath) |> Virtual.toVirtualFile

            override me.GetFileHash(virtualPath, virtualPathDependencies) = 
                me.GetFileHash(virtualPath, virtualPathDependencies)

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

        let virtualProvider = MyVirtualPathProvider(onVirtualFileExists, onGetVirtualFile)

        Virtual.virtualizeViews virtualProvider viewEngines
        :> VirtualPathProvider