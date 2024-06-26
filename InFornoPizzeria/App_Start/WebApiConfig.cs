﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace InFornoPizzeria
{
    public static class WebApiConfig
    { 
        public static void Register(HttpConfiguration config)
        {
            // Servizi e configurazione dell'API Web
            var cors = new EnableCorsAttribute("http://127.0.0.1:5500", "*", "*"); // Puoi specificare le origini consentite qui
            config.EnableCors(cors);
            // Route dell'API Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
