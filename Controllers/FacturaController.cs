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
    [RoutePrefix("api/facturas")]
    public class FacturasController : ApiController
    {
        clsFactura facturaService = new clsFactura();

        [HttpPost]
        [Route("crear")]
        public string Crear([FromBody] Factura f)
        {
            return facturaService.Registrar(f);
        }

        [HttpGet]
        [Route("consultar")]
        public Factura Consultar(int id)
        {
            return facturaService.Consultar(id);
        }

        [HttpPut]
        [Route("actualizar")]
        public string Actualizar([FromBody] Factura f)
        {
            return facturaService.Actualizar(f);
        }

        [HttpDelete]
        [Route("eliminar")]
        public string Eliminar(int id)
        {
            return facturaService.Eliminar(id);
        }
    }
}