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
    [RoutePrefix("api/detallefactura")]
    public class DetalleFacturaController : ApiController
    {
        clsDetalleFactura detalleService = new clsDetalleFactura();

        [HttpPost]
        [Route("crear")]
        public string Crear([FromBody] DetalleFactura d)
        {
            return detalleService.Registrar(d);
        }

        [HttpGet]
        [Route("consultar")]
        public DetalleFactura Consultar(int id)
        {
            return detalleService.Consultar(id);
        }

        [HttpPut]
        [Route("actualizar")]
        public string Actualizar([FromBody] DetalleFactura d)
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
