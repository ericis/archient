namespace WebApplication1
{
    using System;
    using System.Collections;
    using System.Diagnostics;
    using System.Web.Caching;
    using System.Web.Hosting;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Archient.Web.Mvc;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Virtual.virtualizeViews(new MyVFP());
        }
    }

    public class MyVFP : VirtualPathProvider, IVirtualFileProvider
    {
        public override bool FileExists(string virtualPath)
        {
            // #2
            var exists =
                virtualPath.EndsWith("/tada/tada.cshtml", StringComparison.InvariantCultureIgnoreCase)
                ? true
                : base.FileExists(virtualPath);
            //exists = exists || virtualPath == "~/v/teedee";
            Debug.WriteLine("VPP.FileExists(" + virtualPath + ") = " + exists);
            return exists;
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            return this.GetVirtualFile(virtualPath).ToWebVirtualFile();
        }

        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            // #4
            Debug.WriteLine("VPP.GetCacheDependency(" + virtualPath + ")");
            return string.Compare(virtualPath, "/tada/tada.cshtml", StringComparison.InvariantCultureIgnoreCase) == 0
                ? null
                : base.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }

        public override string GetFileHash(string virtualPath, IEnumerable virtualPathDependencies)
        {
            Debug.WriteLine("VPP.GetFileHash(" + virtualPath + ")");
            return base.GetFileHash(virtualPath, virtualPathDependencies);
        }

        IVirtualFile IVirtualFileProvider.GetFile(string virtualPath)
        {
            return this.GetVirtualFile(virtualPath);
        }

        private IVirtualFile GetVirtualFile(string virtualPath)
        {
            var content = "@inherits System.Web.Mvc.WebViewPage<dynamic>\n<strong>Virtual @Model.Message!</strong>";

            Debug.WriteLine("VPP.GetFile(" + virtualPath + ")");

            return
                string.Compare(virtualPath, "/tada/tada.cshtml", StringComparison.InvariantCultureIgnoreCase) == 0
                ? Virtual.createFileFromString(content, virtualPath)
                : base.GetFile(virtualPath).ToVirtualFile();
        }
    }
}