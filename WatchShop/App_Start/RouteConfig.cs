using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WatchShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              name: "LienHe",
              url: "lien-he",
              defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional });
            
            routes.MapRoute(
              name: "GioiThieu",
              url: "gioi-thieu",
              defaults: new { controller = "GioiThieu", action = "Index", id = UrlParameter.Optional });


            routes.MapRoute(
               name: "SlugSite",
               url: "{slug}",
               defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional });


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
