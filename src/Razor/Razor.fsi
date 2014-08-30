namespace Archient.Razor

/// F# Razor library
module Razor =
    open System.CodeDom.Compiler
    open Microsoft.AspNet.Razor

    /// <summary>Maps template content to Razor output including C# code as a string</summary>
    /// <param name="baseClass">The base class that the template's code should implement</param>
    /// <param name="namespaces">A collection of namespaces that should be imported into the implementation as C# <c>using</c> statements</param>
    /// <param name="content">The content of the template</param>
    /// <returns>The Razor output including error information in the case of failure and C# code as a string for success</returns>
    val mapTemplateToCode : baseClass:string -> namespaces:seq<string> -> content:string -> GeneratorResults
    
    /// <summary>Compiles C# code from a string using the referenced assembly locations as assembly dependencies</summary>
    /// <param name="referencedAssemblyLocations">The file locations of the assembly references</param>
    /// <param name="code">A collection of C# code sources</param>
    /// <returns>The C# compiler output including error information in the case of failure and a prepared .NET assembly for success</returns>
    val compile : referencedAssemblyLocations:seq<string> -> code:seq<string> -> CompilerResults

    /// <summary>Creates and executes a razor template specified by the type parameter that is contained in the C# compiler output</summary>
    /// <param name="results">The C# compiler output</param>
    /// <returns>An instance of the razor template</returns>
    val createAndExecute<'t when 't :> IRazorTemplate and 't : null> : results:CompilerResults -> 't
    
    /// <summary>Creates and executes a Razor template, processing and compiling the string into C# code and then executing the template to convert it back to a string</summary>
    /// <param name="baseClass">The base class that the template's code should implement</param>
    /// <param name="namespaces">A collection of namespaces that should be imported into the implementation as C# <c>using</c> statements</param>
    /// <param name="referencedAssemblyLocations">The file locations of the assembly references</param>
    /// <param name="content">The content of the template</param>
    /// <returns>The processed template output</returns>
    val compileAndExecute<'t when 't :> IRazorTemplate and 't : null> : baseClass:string -> namespaces:seq<string> -> referencedAssemblyLocations:seq<string> -> content:string -> string