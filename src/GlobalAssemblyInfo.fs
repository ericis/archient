﻿module GlobalAssemblyInfo

open System.Reflection
open System.Resources
open System.Runtime.InteropServices
    
#if DEBUG
[<assembly: AssemblyConfiguration("Debug")>]
#else
[<assembly: AssemblyConfiguration("Release")>]
#endif

[<assembly: AssemblyCompany("Internet Information Architects, LLC")>]
[<assembly: AssemblyCopyright("""Copyright 2014 Internet Information Architects, LLC

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

     http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.""")>]

[<assembly: ComVisible(false)>]
[<assembly: AssemblyCulture("")>]
[<assembly: NeutralResourcesLanguage("en-US")>]

()