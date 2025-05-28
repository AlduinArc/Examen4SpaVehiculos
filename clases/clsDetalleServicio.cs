using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using SpaVehiculosProyecto.Models;

namespace SpaVehiculosProyecto.clases
{
    public class clsDetalleServicio
    {
        private SpaVehiculosEntities db = new SpaVehiculosEntities();
        public DetalleServicio detalle { get; set; }

        public string Registrar(DetalleServicio detalle)
        {
            try
            {
                db.DetalleServicios.Add(detalle);
                db.SaveChanges();
                return "Detalle de servicio registrado con éxito.";
            }
            catch (Exception ex)
            {
                return "Error al registrar el detalle de servicio: " + ex.Message;
            }
        }

        public DetalleServicio Consultar(int idDetalle)
        {
            return db.DetalleServicios.FirstOrDefault(d => d.idDetalleServicio == idDetalle);
        }

        public string Actualizar(DetalleServicio detalle)
        {
            try
            {
                var existente = Consultar(detalle.idDetalleServicio);
                if (existente == null)
                {
                    return "El detalle no existe.";
                }

                existente.idServicio = detalle.idServicio;
                existente.idAuto = detalle.idAuto;
                existente.Fecha = detalle.Fecha;

                db.SaveChanges();
                return "Detalle actualizado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al actualizar el detalle: " + ex.Message;
            }
        }

        public string Eliminar(int idDetalle)
        {
            try
            {
                var detalle = Consultar(idDetalle);
                if (detalle == null)
                {
                    return "El detalle no existe.";
                }

                db.DetalleServicios.Remove(detalle);
                db.SaveChanges();
                return "Detalle eliminado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al eliminar el detalle: " + ex.Message;
            }
        }
    }
}