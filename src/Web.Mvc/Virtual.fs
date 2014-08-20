namespace Archient.Web.Mvc

open System

type IVirtualFile =

    abstract member VirtualPath : string with get

    abstract member Open : unit -> System.IO.Stream

type IVirtualFileProvider =
    
    abstract member FileExists : virtualPath:string -> bool
    abstract member GetFileHash : virtualPath:string * virtualPathDependencies:System.Collections.IEnumerable -> string
    abstract member GetFile : virtualPath:string -> IVirtualFile

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

    let replaceViewEngines virtualEngine =
        ViewEngines.Engines.Clear()
        ViewEngines.Engines.Add(virtualEngine)

    let addVirtualPathProvider provider =
        HostingEnvironment.RegisterVirtualPathProvider(provider)

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

    let virtualizeViews provider =
        replaceViewEngines(createVirtualRazorViewEngine provider)
        addVirtualPathProvider(provider)

[<System.Runtime.CompilerServices.Extension>]
module VirtualFileExtensions =
    [<System.Runtime.CompilerServices.Extension>]
    let ToWebVirtualFile(virtualFile:IVirtualFile) =
        Virtual.toWebVirtualFile virtualFile
        
    [<System.Runtime.CompilerServices.Extension>]
    let ToVirtualFile(virtualFile:System.Web.Hosting.VirtualFile) =
        Virtual.toVirtualFile virtualFile

    [<System.Runtime.CompilerServices.Extension>]
    let ToWebVirtualFileProvider(virtualFileProvider:IVirtualFileProvider) =
        Virtual.toWebVirtualFileProvider virtualFileProvider