using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CardiologicalResearch
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "ControllerAndId",
                routeTemplate: "api/v1/{controller}/{id}",
                //                defaults: new { id = RouteParameter.Optional }
                defaults: null,
                constraints: new { id = @"^\d+$" } // Only integers
            );

            config.Routes.MapHttpRoute(
                name: "ControllerForSearches",
                routeTemplate: "api/v1/{action}/{controller}",
                defaults: null,
                constraints: new { controller = @"^search$" }
            );

            config.Routes.MapHttpRoute(
                name: "ControllerOnly",
                routeTemplate: "api/v1/{controller}"
            );
        }
    }
}