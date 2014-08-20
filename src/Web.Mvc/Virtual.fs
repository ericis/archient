namespace Archient.Web.Mvc

type IVirtualFile =

    abstract member VirtualPath : string with get

    abstract member Open : unit -> System.IO.Stream

module Virtual =
    
    open System
    open System.IO
    open System.Web.Hosting
    
    type private MyVirtualFile(virtualPath, onOpen) =
        inherit VirtualFile(virtualPath)

        override me.Open() = onOpen()

        interface IVirtualFile with
            override me.VirtualPath with get() = virtualPath
            override me.Open() = me.Open()

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

[<System.Runtime.CompilerServices.Extension>]
module VirtualFileExtensions =
    [<System.Runtime.CompilerServices.Extension>]
    let ToWebVirtualFile(virtualFile:IVirtualFile) =
        Virtual.toWebVirtualFile virtualFile