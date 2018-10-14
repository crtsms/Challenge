using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
<<<<<<< HEAD
=======
using Newtonsoft.Json.Serialization;
>>>>>>> api

namespace WebApplication
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
<<<<<<< HEAD
=======

            // Web API configuration and services

            // use camel case for JSON data
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();

            // Web API routes
>>>>>>> api
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
