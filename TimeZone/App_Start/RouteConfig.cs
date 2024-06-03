using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Time_Zone
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Cart",
                url: "Cart/{action}/{id}",
                defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ProductDetails",
                url: "ProductDetails/{action}/{id}",
                defaults: new { controller = "Home", action = "ProductDetails", id = UrlParameter.Optional }
            );
        }
    }
}


