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
    [RoutePrefix("api/citas")]
    public class CitasController : ApiController
    {
        clsCita citaService = new clsCita();

        [HttpPost]
        [Route("crear")]
        public string Crear([FromBody] Cita c)
        {
            return citaService.Registrar(c);
        }

        [HttpGet]
        [Route("consultar")]
        public Cita Consultar(int id)
        {
            return citaService.Consultar(id);
        }

        [HttpGet]
        [Route("listar")]
        public IHttpActionResult Listar()
        {
            try
            {
                var Cita = citaService.ConsultarTodos();
                return Ok(Cita); // Esto convierte la lista en JSON automáticamente
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("Error al listar clientes: " + ex.Message));
            }
        }

        [HttpPut]
        [Route("actualizar")]
        public string Actualizar([FromBody] Cita c)
        {
            return citaService.Actualizar(c);
        }

        [HttpDelete]
        [Route("eliminar")]
        public string Eliminar(int id)
        {
            return citaService.Eliminar(id);
        }
    }
}
