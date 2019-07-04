using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FreelancerProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Home-Index",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "User-Login",
                url: "{controller}/{action}",
                defaults: new { controller = "User", action = "Login" }
            );

            routes.MapRoute(
                name: "User-Logout",
                url: "{controller}/{action}",
                defaults: new { controller = "User", action = "Logout" }
            );

            routes.MapRoute(
                name: "User-SignUP",
                url: "{controller}/{action}",
                defaults: new { controller = "User", action = "SignUp" }
            );

            routes.MapRoute(
                name: "Contest-Browse",
                url: "{controller}/{action}",
                defaults: new { controller = "Contest", action = "Browse" }
            );


            routes.MapRoute(
                name: "Contest-MyContests",
                url: "{controller}/{action}",
                defaults: new { controller = "Contest", action = "MyContests" }
            );

            routes.MapRoute(
                name: "Contest-Create",
                url: "{controller}/{action}",
                defaults: new { controller = "Contest", action = "Create" }
            );

            routes.MapRoute(
                name: "Contest-Show",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Contest", action = "Show", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Entry-Action",
                url: "{controller}/{action}/{id}/{perform}/{cid}",
                defaults: new { controller = "Contest", action = "Entry_Action", id = UrlParameter.Optional, perform = UrlParameter.Optional ,cid = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Entry-Upload",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Entry", action = "Upload", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Handover",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Contest", action = "Handover", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Payment",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Contest", action = "Payment", id = UrlParameter.Optional }
            );
        }
    }
}
