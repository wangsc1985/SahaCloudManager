using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SahaCloudManager
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" }
            );
            routes.MapRoute(
                name: "token",
                url: "{controller}/{action}/{appid}/{secret}",
                defaults: new { controller = "Home", action = "Token", appid = UrlParameter.Optional,secret= UrlParameter.Optional }
            );
        }
    }
}
