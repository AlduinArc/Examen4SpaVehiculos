using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using SpaVehiculosProyecto.Models;

namespace SpaVehiculosProyecto.clases
{
    public class clsFactura
    {
        private SpaVehiculosEntities db = new SpaVehiculosEntities();
        public Factura factura { get; set; }

        public string Registrar(Factura factura)
        {
            try
            {
                factura.Fecha = DateTime.Now;
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
