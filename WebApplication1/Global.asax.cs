namespace WebApplication1
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Archient.Web.Mvc;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ViewEngines.Engines.Virtualize(VirtualViewExists, GetVirtualView);
        }

        private static bool VirtualViewExists(string virtualPath)
        {
            // may get called with an optional "~/" prefix, so use EndsWith when possible
            return virtualPath.EndsWith("/tada/tada.cshtml", StringComparison.InvariantCultureIgnoreCase);
        }

        private static IVirtualFile GetVirtualView(string virtualPath)
        {
            const string Content = "@inherits System.Web.Mvc.WebViewPage<dynamic>\n<strong>Virtual @Model.Message!</strong>";
            
            return Virtual.createFileFromString(Content, virtualPath);
        }
    }
}