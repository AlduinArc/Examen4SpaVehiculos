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
    [RoutePrefix("api/detalleservicio")]
    public class DetalleServicioController : ApiController
    {
        clsDetalleServicio detalleService = new clsDetalleServicio();

        [HttpPost]
        [Route("crear")]
        public string Crear([FromBody] DetalleServicio d)
        {
            return detalleService.Registrar(d);
        }

        [HttpGet]
        [Route("consultar")]
        public DetalleServicio Consultar(int id)
        {
            return detalleService.Consultar(id);
        }

        [HttpGet]
        [Route("listar")]
        public IHttpActionResult Listar()
        {
            try
            {
                var detalleServicios = detalleService.ConsultarTodos();
                return Ok(detalleServicios); // Esto convierte la lista en JSON automáticamente
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("Error al listar clientes: " + ex.Message));
            }
        }

        [HttpPut]
        [Route("actualizar")]
        public string Actualizar([FromBody] DetalleServicio d)
        {
            return detalleService.Actualizar(d);
        }

        [HttpDelete]
        [Route("eliminar")]
        public string Eliminar(int id)
        {
            return detalleService.Eliminar(id);
        }
    }
}
