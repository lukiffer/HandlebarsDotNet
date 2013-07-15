using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HandlebarsDotNet.Example
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);

            BundleTable.Bundles.Add(new Bundle("~/bundle/js/app/templates/", 
                new HandlebarsBundleTransform("templates", true, "/assets/js/app/templates/"))
                    .IncludeDirectory("~/assets/js/app/templates/", "*.hbs")
                    .IncludeDirectory("~/assets/js/app/templates/sub/", "*.hbs"));

            BundleTable.EnableOptimizations = true;
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("Default", "{controller}/{action}/{id}", 
                new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}