namespace Archient.Web.Mvc

open System.IO
open System.Collections

type IVirtualFile =

    abstract member VirtualPath : string with get

    abstract member Open : unit -> Stream

type IVirtualFileProvider =
    
    abstract member FileExists : virtualPath:string -> bool
    abstract member GetFileHash : virtualPath:string * virtualPathDependencies:IEnumerable -> string
    abstract member GetFile : virtualPath:string -> IVirtualFile