using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json;
using System.Web.Http.Cors; // Agregado para CORS

namespace SpaVehiculosProyecto
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Habilitar CORS
            var cors = new EnableCorsAttribute(
                origins: "*", // Permitir todos los orígenes (ajústalo según necesites)
                headers: "*",
                methods: "*");
            config.EnableCors(cors);

            // Configuración para ignorar referencias circulares en JSON
            var jsonFormatter = config.Formatters.JsonFormatter;
            jsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;

            // Configuración para ignorar valores nulos
            jsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

            // Configurar el formato de fecha
            jsonFormatter.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;

            // Habilitar rutas basadas en atributos (necesario para rutas como api/autos/consultar)
            config.MapHttpAttributeRoutes();

            // Configuración de rutas por defecto
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}", // Agregado {action} para soportar rutas como consultar, crear, etc.
                defaults: new { id = RouteParameter.Optional }
            );

            // Opcional: Eliminar el formatter de XML para usar solo JSON
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}