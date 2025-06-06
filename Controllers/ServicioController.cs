﻿using SpaVehiculosProyecto.Models;
using SpaVehiculosProyecto.clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SpaVehiculosProyecto.Controllers
{
    [RoutePrefix("api/servicios")]
    public class ServiciosController : ApiController
    {
        clsServicio servicioService = new clsServicio();

        [HttpPost]
        [Route("crear")]
        public string Crear([FromBody] Servicio s)
        {
            return servicioService.Registrar(s);
        }

        [HttpGet]
        [Route("consultar")]
        public Servicio Consultar(int id)
        {
            return servicioService.Consultar(id);
        }

        [HttpGet]
        [Route("listar")]
        public IHttpActionResult Listar()
        {
            try
            {
                var Servicio = servicioService.ConsultarTodos();
                return Ok(Servicio); // Esto convierte la lista en JSON automáticamente
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("Error al listar clientes: " + ex.Message));
            }
        }

        [HttpPut]
        [Route("actualizar")]
        public string Actualizar([FromBody] Servicio s)
        {
            return servicioService.Actualizar(s);
        }

        [HttpDelete]
        [Route("eliminar")]
        public string Eliminar(int id)
        {
            return servicioService.Eliminar(id);
        }
    }
}
