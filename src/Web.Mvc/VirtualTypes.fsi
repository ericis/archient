namespace Archient.Web.Mvc

open System.IO
open System.Collections

/// Contract for a virtual file
type IVirtualFile =

    /// Gets the virtual file's path
    abstract member VirtualPath : string with get

    /// Opens a stream to the virtual file's contents
    abstract member Open : unit -> Stream

/// Contract for a virtual file provider
type IVirtualFileProvider =
    
    /// Checks if the specified virtual file exists from the specified path
    abstract member FileExists : virtualPath:string -> bool
    
    /// Gets the virtual file's hash key from the specified path
    abstract member GetFileHash : virtualPath:string * virtualPathDependencies:IEnumerable -> string

    /// Gets the virtual file from the specified path
    abstract member GetFile : virtualPath:string -> IVirtualFile