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
    [RoutePrefix("api/clientes")]
    public class ClientesController : ApiController
    {
        clsCliente clienteService = new clsCliente();

        [HttpPost]
        [Route("crear")]
        public string Crear([FromBody] Cliente c)
        {
            return clienteService.Registrar(c);
        }

        [HttpGet]
        [Route("consultar")]
        public Cliente Consultar(int id)
        {
            return clienteService.Consultar(id);
        }

        [HttpPut]
        [Route("actualizar")]
        public string Actualizar([FromBody] Cliente c)
        {
            return clienteService.Actualizar(c);
        }

        [HttpDelete]
        [Route("eliminar")]
        public string Eliminar(int id)
        {
            return clienteService.Eliminar(id);
        }
    }
}