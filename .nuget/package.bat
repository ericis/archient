cls
NuGet.exe pack ..\src\Config\Config.fsproj -OutputDirectory ..\.deploy\
NuGet.exe pack ..\src\Depend\Depend.fsproj -OutputDirectory ..\.deploy\
NuGet.exe pack ..\src\Depend.Unity\Depend.Unity.fsproj -OutputDirectory ..\.deploy\
NuGet.exe pack ..\src\Test.Xunit\Test.Xunit.fsproj -OutputDirectory ..\.deploy\
NuGet.exe pack ..\src\Test.Foq\Test.Foq.fsproj -OutputDirectory ..\.deploy\
NuGet.exe pack ..\src\Test.TickSpec\Test.TickSpec.fsproj -OutputDirectory ..\.deploy\
NuGet.exe pack ..\src\WPF\WPF.fsproj -OutputDirectory ..\.deploy\
NuGet.exe pack ..\src\MVC\MVC.fsproj -OutputDirectory ..\.deploy\
NuGet.exe pack ..\src\WebApi\WebApi.fsproj -OutputDirectory ..\.deploy\