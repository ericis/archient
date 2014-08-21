namespace Archient.Web.Mvc

module Virtual =
    
    open System
    open System.IO
    open System.Collections
    open System.Web.Hosting
    open System.Web.Mvc
    
    /// Creates a virtual file given the specified function and path
    val createFile : onOpen:(unit->Stream) -> virtualPath:string -> IVirtualFile

    /// Creats a virtual file from the specified content and path
    val createFileFromString : content:string -> virtualPath:string -> IVirtualFile

    /// Converts a virtual file to a System.Web virtual file
    val toWebVirtualFile : virtualFile:IVirtualFile -> VirtualFile
    
    /// Converts a System.Web virtual file to a virtual file
    val toVirtualFile : virtualFile:VirtualFile -> IVirtualFile

    /// Creates a virtual file provider given the functions
    val createVirtualFileProvider : onFileExists:(string->bool) -> onGetFileHash:(string->IEnumerable->string) -> onGetFile:(string->IVirtualFile) -> IVirtualFileProvider

    /// Replaces the MVC View Engines with the specifed engine
    val replaceViewEngines : virtualEngine:IViewEngine -> viewEngines:ViewEngineCollection -> ViewEngineCollection

    /// Adds a virtual path provider to the global System.Web.Hosting environment
    val addVirtualPathProvider<'t when 't :> VirtualPathProvider> : provider:'t -> 't

    /// Converts a virtual file provider to a System.Web virtual path provider
    val toWebVirtualFileProvider : virtualProvider:IVirtualFileProvider -> VirtualPathProvider

    /// Creates a virtual Razor View Engine given the virtual file provider
    val createVirtualRazorViewEngine : virtualFileProvider:IVirtualFileProvider -> IViewEngine
    
    /// Virtualizes MVC views given the virtual file provider
    val virtualizeViewsWithProvider<'t when 't :> IVirtualFileProvider and 't :> VirtualPathProvider> : provider:'t -> viewEngines:ViewEngineCollection -> 't
    
    /// Virtualizes MVC views given the functions
    val virtualizeViews : onVirtualFileExists:(string->bool) -> onGetVirtualFile:(string->IVirtualFile) -> viewEngines:ViewEngineCollection -> IVirtualFileProvider