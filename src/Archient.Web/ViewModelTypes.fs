namespace Archient.Web

type MyViewEngine() as me =
    inherit System.Web.Mvc.RazorViewEngine()

    //// http://weblogs.asp.net/imranbaloch/view-engine-with-dynamic-view-location

    do
        System.Diagnostics.Debug.WriteLine(sprintf "[Before] FileExtensions: %A" me.FileExtensions)

        System.Diagnostics.Debug.WriteLine(sprintf "[Before] AreaMasterLocationFormats: %A" me.AreaMasterLocationFormats)
        System.Diagnostics.Debug.WriteLine(sprintf "[Before] AreaViewLocationFormats: %A" me.AreaViewLocationFormats)
        System.Diagnostics.Debug.WriteLine(sprintf "[Before] AreaPartialViewLocationFormats: %A" me.AreaPartialViewLocationFormats)

        System.Diagnostics.Debug.WriteLine(sprintf "[Before] MasterLocationFormats: %A" me.MasterLocationFormats)
        System.Diagnostics.Debug.WriteLine(sprintf "[Before] ViewLocationFormats: %A" me.ViewLocationFormats)
        System.Diagnostics.Debug.WriteLine(sprintf "[Before] PartialViewLocationFormats: %A" me.PartialViewLocationFormats)

//        me.AreaMasterLocationFormats <- Array.empty
//        me.AreaViewLocationFormats <- Array.empty
//        me.AreaPartialViewLocationFormats <- Array.empty
//
//        me.PartialViewLocationFormats <-
//            me.PartialViewLocationFormats
//            |> Array.append [|"~/{0}.cshtml"|]
//
//        System.Diagnostics.Debug.WriteLine(sprintf "[After] FileExtensions: %A" me.FileExtensions)
//
//        System.Diagnostics.Debug.WriteLine(sprintf "[After] AreaMasterLocationFormats: %A" me.AreaMasterLocationFormats)
//        System.Diagnostics.Debug.WriteLine(sprintf "[After] AreaViewLocationFormats: %A" me.AreaViewLocationFormats)
//        System.Diagnostics.Debug.WriteLine(sprintf "[After] AreaPartialViewLocationFormats: %A" me.AreaPartialViewLocationFormats)
//
//        System.Diagnostics.Debug.WriteLine(sprintf "[After] MasterLocationFormats: %A" me.MasterLocationFormats)
//        System.Diagnostics.Debug.WriteLine(sprintf "[After] ViewLocationFormats: %A" me.ViewLocationFormats)
//        System.Diagnostics.Debug.WriteLine(sprintf "[After] PartialViewLocationFormats: %A" me.PartialViewLocationFormats)

type VirtualViewFile(virtualPath) =
    inherit System.Web.Hosting.VirtualFile(virtualPath)

    override me.IsDirectory with get() = false

    override me.Open() =
        let stream = new System.IO.MemoryStream()
        let bytes = System.Text.Encoding.ASCII.GetBytes("@inherits System.Web.Mvc.WebViewPage\n\n<strong>TADA</strong>")

        stream.Write(bytes, 0, bytes.Length)

        stream :> System.IO.Stream

type VirtualViewPathProvider() =
    inherit System.Web.Hosting.VirtualPathProvider()

    // System.Web.VirtualPathUtility

    override me.Initialize() =
        
        base.Initialize()

        System.Diagnostics.Debug.WriteLine("VirtualViewPathProvider.Initialize()")
        // ()

    override me.InitializeLifetimeService() =

        let svc = base.InitializeLifetimeService()

        System.Diagnostics.Debug.WriteLine(sprintf "VirtualViewPathProvider.InitializeLifetimeService() = %A" svc)

        svc
        // obj

    override me.CombineVirtualPaths(basePath, virtualPath) =
        
        let paths = base.CombineVirtualPaths(basePath, virtualPath)

        System.Diagnostics.Debug.WriteLine(sprintf "VirtualViewPathProvider.CombineVirtualPaths(\"%s\", \"%s\") = \"%s\"" basePath virtualPath paths)

        paths
        // string

    override me.DirectoryExists(virtualDir) =
        
        let exists = 
            match virtualDir with
            //| "~/tada/tada/" -> true
            | _ -> base.DirectoryExists(virtualDir)
        
        System.Diagnostics.Debug.WriteLine(sprintf "VirtualViewPathProvider.DirectoryExists(\"%s\") = %A" virtualDir exists)

        exists
        // bool

    override me.FileExists(virtualPath) =
        
        (*
            .aspx
            .ascx
            .vbhtml
        *)

        let exists = 
            match virtualPath with
            | "/tada/tada.cshtml"
            | "~/tada/tada.cshtml" -> true
            | _ -> base.FileExists(virtualPath)
        
        System.Diagnostics.Debug.WriteLine(sprintf "VirtualViewPathProvider.FileExists(\"%s\") = %A" virtualPath exists)
        
        exists
        // bool

    override me.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart) =
        
        let dep = base.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart)

        System.Diagnostics.Debug.WriteLine(sprintf "VirtualViewPathProvider.GetCacheDependency(\"%s\", %A, %A) = %A" virtualPath virtualPathDependencies utcStart dep)

        dep
        // CacheDependency

    override me.GetCacheKey(virtualPath) =
        
        let key = base.GetCacheKey(virtualPath)
        
        System.Diagnostics.Debug.WriteLine(sprintf "VirtualViewPathProvider.GetCacheKey(\"%s\") = \"%s\"" virtualPath key)
        
        key
        // string

    override me.GetDirectory(virtualDir) =
        
        let vdir = base.GetDirectory(virtualDir)

        System.Diagnostics.Debug.WriteLine(sprintf "VirtualViewPathProvider.GetDirectory(\"%s\") = %A" virtualDir vdir)
        
        vdir
        // VirtualDirectory

    override me.GetFile(virtualPath) =
        
        let vfile = 
            match virtualPath with
            | "/tada.cshtml" ->
                VirtualViewFile(virtualPath) :> System.Web.Hosting.VirtualFile
            | _ -> base.GetFile(virtualPath)
            
        System.Diagnostics.Debug.WriteLine(sprintf "VirtualViewPathProvider.GetFile(\"%s\") = %A" virtualPath vfile)

        vfile
        // VirtualFile

    override me.GetFileHash(virtualPath, virtualPathDependencies) =
        
        let hash = base.GetFileHash(virtualPath, virtualPathDependencies)

        System.Diagnostics.Debug.WriteLine(sprintf "VirtualViewPathProvider.GetFileHash(\"%s\", %A) = \"%s\"" virtualPath virtualPathDependencies hash)

        hash
        // string

type PageHeaderViewModel(title:string) =
    member me.Title = title

type PageFooterViewModel() =
    class end

type PageViewModel<'THeader,'TBody,'TFooter>(header:'THeader, body:'TBody, footer:'TFooter) =
    member me.Header = header
    member me.Body = body
    member me.Footer = footer

type IViewModelProviderStrategy =
    abstract member CanProvide : host:string * primary:Domains.PrimaryDomain -> bool
    abstract member GetViewModel : host:string * primary:Domains.PrimaryDomain -> string*obj