namespace Archient.Web.Mvc

module Virtual =
    
    open System
    open System.IO
    open System.Web.Hosting
    open System.Web.Mvc
    
    type private MyVirtualFile(virtualPath, onOpen) =
        inherit VirtualFile(virtualPath)

        override me.Open() = onOpen()

        interface IVirtualFile with
            override me.VirtualPath with get() = virtualPath
            override me.Open() = me.Open()

    type private MyVirtualFilePathProvider
        (
            onFileExists,
            onGetFile,
            onGetCacheDependency) =

        inherit VirtualPathProvider()

        override me.FileExists(virtualPath) =
            onFileExists virtualPath

        override me.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart) =
            onGetCacheDependency virtualPath virtualPathDependencies utcStart

        override me.GetFile(virtualPath) =
            onGetFile virtualPath

    type private VirtualRazorViewEngine(virtualFileProvider:IVirtualFileProvider) as me =
        inherit VirtualPathProviderViewEngine()
        
        do
            me.AreaMasterLocationFormats <- Array.empty
            me.AreaViewLocationFormats <- Array.empty
            me.AreaPartialViewLocationFormats <- Array.empty
            me.MasterLocationFormats <- Array.empty
            me.ViewLocationFormats <- [|""|] // empty results in MVC exception
            me.PartialViewLocationFormats <- Array.empty

        override me.FileExists(controllerContext, virtualPath) =
            match virtualFileProvider.FileExists(virtualPath) with
            | true -> true
            | false -> base.FileExists(controllerContext, virtualPath)

        override me.CreatePartialView(controllerContext, partialPath) = 
            RazorView(controllerContext, partialPath, null, true, null) :> IView

        override me.CreateView(controllerContext, viewPath, masterPath) =
            RazorView(controllerContext, viewPath, masterPath, true, null) :> IView

    let createFile onOpen virtualPath =
        {
            new IVirtualFile with
                override me.VirtualPath with get() = virtualPath
                override me.Open() = onOpen()
        }

    let createFileFromString (content:string) virtualPath =
        let onOpen () =
            let stream = new MemoryStream()
            let writer = new StreamWriter(stream)
            
            writer.Write(content)
            writer.Flush()
            
            stream.Position <- int64 0

            stream :> Stream

        createFile onOpen virtualPath

    let toWebVirtualFile (virtualFile:IVirtualFile) =
        new MyVirtualFile(virtualFile.VirtualPath, fun _ -> virtualFile.Open()) :> VirtualFile

    let toVirtualFile (virtualFile:VirtualFile) =
        new MyVirtualFile(virtualFile.VirtualPath, fun _ -> virtualFile.Open()) :> IVirtualFile

    let createVirtualFileProvider onFileExists onGetFileHash onGetFile =
        {
            new IVirtualFileProvider with
                override me.FileExists(virtualPath) = onFileExists virtualPath
                override me.GetFileHash(virtualPath, virtualPathDependencies) = onGetFileHash virtualPath virtualPathDependencies
                override me.GetFile(virtualPath) = onGetFile virtualPath
        }

    let replaceViewEngines virtualEngine (viewEngines:ViewEngineCollection) =
        viewEngines.Clear()
        viewEngines.Add(virtualEngine)
        viewEngines

    let addVirtualPathProvider<'t when 't :> VirtualPathProvider> (provider:'t) =
        HostingEnvironment.RegisterVirtualPathProvider(provider)
        provider

    let toWebVirtualFileProvider (virtualProvider:IVirtualFileProvider) =
        let onFileExists path = virtualProvider.FileExists(path)
        let onGetFile path = virtualProvider.GetFile(path) :?> VirtualFile
        let onGetCache _ _ _ = null
        new MyVirtualFilePathProvider(
            onFileExists, 
            onGetFile, 
            onGetCache) :> VirtualPathProvider
    
    let createVirtualRazorViewEngine virtualFileProvider =
        VirtualRazorViewEngine(virtualFileProvider) :> IViewEngine

    let virtualizeViews<'t when 't :> IVirtualFileProvider and 't :> VirtualPathProvider> (provider:'t) viewEngines =
        ignore <| replaceViewEngines (createVirtualRazorViewEngine provider) viewEngines
        addVirtualPathProvider(provider)