using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using Archient.Web.Mvc;

namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new MyVE());
            //ViewEngines.Engines.Add(new RazorViewEngine());

            HostingEnvironment.RegisterVirtualPathProvider(new MyV());
        }
    }

    public class MyVF : VirtualFile
    {
        public MyVF(string virtualPath)
            : base(virtualPath)
        {
            Debug.WriteLine("MyVF(" + virtualPath + ")");
        }

        public override Stream Open()
        {
            // #3
            Debug.WriteLine("MyVF.Open()");
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write("@inherits System.Web.Mvc.WebViewPage<dynamic>\n<strong>Virtual @Model.Message!</strong>");
            writer.Flush();
            ms.Position = 0;
            return ms;
        }
    }

    public class MyV : VirtualPathProvider
    {
        public override bool FileExists(string virtualPath)
        {
            // #2
            var exists =
                string.Compare(virtualPath, "/tada/tada.cshtml", StringComparison.InvariantCultureIgnoreCase) == 0
                ? true
                : base.FileExists(virtualPath);
            //exists = exists || virtualPath == "~/v/teedee";
            Debug.WriteLine("VPP.FileExists(" + virtualPath + ") = " + exists);
            return exists;
        }

        public override bool DirectoryExists(string virtualDir)
        {
            var exists = base.DirectoryExists(virtualDir);
            Debug.WriteLine("VPP.DirectoryExists(" + virtualDir + ") = " + exists);
            return exists;
        }

        public override string CombineVirtualPaths(string basePath, string relativePath)
        {
            Debug.WriteLine("VPP.CombineVirtualPaths(" + basePath + ", " + relativePath + ")");
            return base.CombineVirtualPaths(basePath, relativePath);
        }

        public override string GetFileHash(string virtualPath, IEnumerable virtualPathDependencies)
        {
            Debug.WriteLine("VPP.GetFileHash(" + virtualPath + ")");
            return base.GetFileHash(virtualPath, virtualPathDependencies);
        }

        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            // #4
            Debug.WriteLine("VPP.GetCacheDependency(" + virtualPath + ")");
            return string.Compare(virtualPath, "/tada/tada.cshtml", StringComparison.InvariantCultureIgnoreCase) == 0
                ? null
                : base.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }

        public override string GetCacheKey(string virtualPath)
        {
            Debug.WriteLine("VPP.GetCacheKey(" + virtualPath + ")");
            return base.GetCacheKey(virtualPath);
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            var content = "@inherits System.Web.Mvc.WebViewPage<dynamic>\n<strong>Virtual @Model.Message!</strong>";

            Debug.WriteLine("VPP.GetFile(" + virtualPath + ")");
            
            return
                string.Compare(virtualPath, "/tada/tada.cshtml", StringComparison.InvariantCultureIgnoreCase) == 0
                ? Virtual.createFileFromString(content, virtualPath).ToWebVirtualFile()
                : base.GetFile(virtualPath);
        }

        public override VirtualDirectory GetDirectory(string virtualDir)
        {
            Debug.WriteLine("VPP.GetDirectory(" + virtualDir + ")");
            return base.GetDirectory(virtualDir);
        }
    }

    public class MyVE : VirtualPathProviderViewEngine
    {
        public MyVE()
        {
            this.AreaMasterLocationFormats = new string[0];
            this.AreaViewLocationFormats = new string[0];
            this.AreaPartialViewLocationFormats = new string[0];

            foreach (var s in this.MasterLocationFormats ?? new string[0])
                Debug.WriteLine("MyVE.MasterLocationFormats = " + s);
            foreach (var s in this.ViewLocationFormats ?? new string[0])
                Debug.WriteLine("MyVE.ViewLocationFormats = " + s);
            foreach (var s in this.PartialViewLocationFormats ?? new string[0])
                Debug.WriteLine("MyVE.PartialViewLocationFormats = " + s);

            this.MasterLocationFormats = new[] { "~/Shared/{1}/{0}.cshtml" };
            this.ViewLocationFormats = new[] { "~/Views/{1}/{0}.cshtml", "~/Shared/{1}/{0}.cshtml" };
            this.PartialViewLocationFormats = new[] { "~/Views/{1}/{0}.cshtml", "~/Shared/{1}/{0}.cshtml" };
        }

        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            // #1
            var exists = 
                string.Compare(virtualPath, "~/tada/tada.cshtml", StringComparison.InvariantCultureIgnoreCase) == 0
                ? true
                : base.FileExists(controllerContext, virtualPath);
            Debug.WriteLine("VPPVE.FileExists(" + virtualPath + ") = " + exists);
            return exists;
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            Debug.WriteLine("VPPVE.CreatePartialView(" + partialPath + ")");
            return new RazorView(controllerContext, partialPath, null, true, null);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            var view = new RazorView(controllerContext, viewPath, masterPath, true, null);
            Debug.WriteLine("VPPVE.CreateView(" + viewPath + ", " + masterPath + ") = " + view.ViewPath);
            return view;
        }
    }
}