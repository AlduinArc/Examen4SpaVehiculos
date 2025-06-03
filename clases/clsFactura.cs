using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;
using SpaVehiculosProyecto.Models;

namespace SpaVehiculosProyecto.clases
{
    public class clsFactura
    {
        private SpaVehiculosEntities1 db = new SpaVehiculosEntities1();
        public Factura factura { get; set; }

        public string Registrar(Factura factura)
        {
            try
            {
                db.Facturas.Add(factura);
                db.SaveChanges();
                return "Factura registrada con éxito.";
            }
            catch (Exception ex)
            {
                return "Error al registrar la factura: " + ex.Message;
            }
        }

        public Factura Consultar(int idFactura)
        {
            return db.Facturas.FirstOrDefault(f => f.idFactura == idFactura);
        }

        public IEnumerable<Factura> ConsultarTodos()
        {
            try
            {
                return db.Facturas.ToList(); // Devuelve todos los clientes
            }
            catch (Exception ex)
            {
                // Podrías lanzar la excepción o manejarla según tus necesidades
                throw new Exception("Error al consultar todos los clientes: " + ex.Message);
            }
        }

        public string Actualizar(Factura factura)
        {
            try
            {
                var existente = Consultar(factura.idFactura);
                if (existente == null)
                {
                    return "La factura no existe.";
                }

                existente.idCliente = factura.idCliente;
                existente.idEmpleado = factura.idEmpleado;
                existente.Fecha = factura.Fecha;
                existente.Total = factura.Total;

                db.SaveChanges();
                return "Factura actualizada correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al actualizar la factura: " + ex.Message;
            }
        }

        public string Eliminar(int idFactura)
        {
            try
            {
                var factura = Consultar(idFactura);
                if (factura == null)
                {
                    return "La factura no existe.";
                }

                db.Facturas.Remove(factura);
                db.SaveChanges();
                return "Factura eliminada correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al eliminar la factura: " + ex.Message;
            }
        }
    }
}
