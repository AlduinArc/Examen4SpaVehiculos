using System;
using System.Collections.Generic;
using System.Linq;
using SpaVehiculosProyecto.Models;

namespace SpaVehiculosProyecto.clases
{
    public class clsDetalleFactura
    {
        private SpaVehiculosEntities1 db = new SpaVehiculosEntities1();
        public DetalleFactura detalle { get; set; }

        public string Registrar(DetalleFactura detalle)
        {
            try
            {
                db.DetalleFacturas.Add(detalle);
                db.SaveChanges();
                return "Detalle de factura registrado con éxito.";
            }
            catch (Exception ex)
            {
                return "Error al registrar el detalle de factura: " + ex.Message;
            }
        }

        public DetalleFactura Consultar(int idDetalle)
        {
            return db.DetalleFacturas.FirstOrDefault(d => d.idDetalleFactura == idDetalle);
        }
        public IEnumerable<DetalleFactura> ConsultarTodos()
        {
            try
            {
                return db.DetalleFacturas.ToList(); // Devuelve todos los clientes
            }
            catch (Exception ex)
            {
                // Podrías lanzar la excepción o manejarla según tus necesidades
                throw new Exception("Error al consultar todos los clientes: " + ex.Message);
            }
        }

        public string Actualizar(DetalleFactura detalle)
        {
            try
            {
                var existente = Consultar(detalle.idDetalleFactura);
                if (existente == null)
                {
                    return "El detalle no existe.";
                }

                existente.idFactura = detalle.idFactura;
                existente.Descripcion = detalle.Descripcion;
                existente.Cantidad = detalle.Cantidad;
                existente.PrecioUnitario = detalle.PrecioUnitario;

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

                db.DetalleFacturas.Remove(detalle);
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
