namespace Archient.Web.Mvc

module VirtualFileExtensions =
    open System
    open System.Web.Hosting
    open System.Web.Mvc

    /// Converts a virtual file to a System.Web virtual file
    val ToWebVirtualFile : virtualFile:IVirtualFile -> VirtualFile
    
    /// Converts a System.Web virtual file to a virtual file
    val ToVirtualFile : virtualFile:VirtualFile -> IVirtualFile
    
    /// Converts a virtual file provider to a System.Web virtual path provider
    val ToWebVirtualFileProvider : virtualFileProvider:IVirtualFileProvider -> VirtualPathProvider

    /// Virtualizes MVC Views to come from a virtual location
    val Virtualize : viewEngines:ViewEngineCollection * onVirtualFileExists:Func<string,bool> * onGetVirtualFile:Func<string,IVirtualFile> -> VirtualPathProvider