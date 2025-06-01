using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json;

namespace SpaVehiculosProyecto
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración para ignorar referencias circulares en JSON
            var jsonFormatter = config.Formatters.JsonFormatter;
            jsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;

            // Configuración para ignorar valores nulos
            jsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

            // Configurar el formato de fecha
            jsonFormatter.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;

            // Configuración y servicios de Web API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Opcional: Eliminar el formatter de XML para usar solo JSON
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}