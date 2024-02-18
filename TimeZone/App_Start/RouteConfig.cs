using System;
using System.Collections.Generic;
/*using System.Linq;*/
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace TimeZone
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
        }
    }

    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Vendor/assets/css/bootstrap.min.css",
                "~/Vendor/assets/css/owl.carousel.min.css",
                "~/Vendor/assets/css/flaticon.css",
                "~/Vendor/assets/css/slicknav.css",
                "~/Vendor/assets/css/animate.min.css",
                "~/Vendor/assets/css/magnific-popup.css",
                "~/Vendor/assets/css/fontawesome-all.min.css",
                "~/Vendor/assets/css/themify-icons.css",
                "~/Vendor/assets/css/slick.css",
                "~/Vendor/assets/css/nice-select.css",
                "~/Vendor/assets/css/style.css"));

            bundles.Add(new ScriptBundle("~/bundles/vendor").Include(
                       "~/Vendor/assets/js/vendor/modernizr-3.5.0.min.js",
                       "~/Vendor/assets/js/vendor/jquery-1.12.4.min.js",
                       "~/Vendor/assets/js/popper.min.js",
                       "~/Vendor/assets/js/bootstrap.min.js",
                       "~/Vendor/assets/js/jquery.slicknav.min.js",
                       "~/Vendor/assets/js/owl.carousel.min.js",
                       "~/Vendor/assets/js/slick.min.js",
                       "~/Vendor/assets/js/wow.min.js",
                       "~/Vendor/assets/js/animated.headline.js",
                       "~/Vendor/assets/js/jquery.magnific-popup.js",
                       "~/Vendor/assets/js/jquery.scrollUp.min.js",
                       "~/Vendor/assets/js/jquery.nice-select.min.js",
                       "~/Vendor/assets/js/jquery.sticky.js",
                       "~/Vendor/assets/js/contact.js",
                       "~/Vendor/assets/js/jquery.form.js",
                       "~/Vendor/assets/js/jquery.validate.min.js",
                       "~/Vendor/assets/js/mail-script.js",
                       "~/Vendor/assets/js/jquery.ajaxchimp.min.js",
                       "~/Vendor/assets/js/plugins.js",
                       "~/Vendor/assets/js/main.js"));
        }
    }

}
