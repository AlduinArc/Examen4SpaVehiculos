using SpaVehiculosProyecto.Models;
using SpaVehiculosProyecto.clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SpaVehiculosProyecto.Controllers
{
    [RoutePrefix("api/autos")]
    public class AutosController : ApiController
    {
        clsAuto autoService = new clsAuto();

        [HttpPost]
        [Route("crear")]
        public string Crear([FromBody] Auto a)
        {
            return autoService.Registrar(a);
        }

        [HttpGet]
        [Route("consultar")]
        public Auto Consultar(int id)
        {
            return autoService.Consultar(id);
        }
        [HttpGet]
        [Route("listar")]
        public IHttpActionResult Listar()
        {
            try
            {
                var autos = autoService.ConsultarTodos();
                return Ok(autos); // Esto convierte la lista en JSON automáticamente
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("Error al listar clientes: " + ex.Message));
            }
        }

        [HttpPut]
        [Route("actualizar")]
        public string Actualizar([FromBody] Auto a)
        {
            return autoService.Actualizar(a);
        }

        [HttpDelete]
        [Route("eliminar")]
        public string Eliminar(int id)
        {
            return autoService.Eliminar(id);
        }
    }
}