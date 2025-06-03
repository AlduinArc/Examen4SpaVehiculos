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
    [RoutePrefix("api/empleados")]
    public class EmpleadosController : ApiController
    {
        clsEmpleado empleadoService = new clsEmpleado();

        [HttpPost]
        [Route("crear")]
        public string Crear([FromBody] Empleado e)
        {
            return empleadoService.Registrar(e);
        }

        [HttpGet]
        [Route("consultar")]
        public Empleado Consultar(int id)
        {
            return empleadoService.Consultar(id);
        }

        [HttpGet]
        [Route("listar")]
        public IHttpActionResult Listar()
        {
            try
            {
                var Empleado= empleadoService.ConsultarTodos();
                return Ok(Empleado); // Esto convierte la lista en JSON automáticamente
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("Error al listar clientes: " + ex.Message));
            }
        }

        [HttpPut]
        [Route("actualizar")]
        public string Actualizar([FromBody] Empleado e)
        {
            return empleadoService.Actualizar(e);
        }

        [HttpDelete]
        [Route("eliminar")]
        public string Eliminar(int id)
        {
            return empleadoService.Eliminar(id);
        }
    }
}