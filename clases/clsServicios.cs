using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using SpaVehiculosProyecto.Models;

namespace SpaVehiculosProyecto.clases
{
    public class clsServicio
    {
        private SpaVehiculosEntities db = new SpaVehiculosEntities();
        public Servicio servicio { get; set; }

        public string Registrar(Servicio servicio)
        {
            try
            {
                db.Servicios.Add(servicio);
                db.SaveChanges();
                return "Servicio registrado con éxito.";
            }
            catch (Exception ex)
            {
                return "Error al registrar el servicio: " + ex.Message;
            }
        }

        public Servicio Consultar(int idServicio)
        {
            return db.Servicios.FirstOrDefault(s => s.idServicio == idServicio);
        }

        public string Actualizar(Servicio servicio)
        {
            try
            {
                var existente = Consultar(servicio.idServicio);
                if (existente == null)
                {
                    return "El servicio no existe.";
                }

                existente.Nombre = servicio.Nombre;
                existente.Descripcion = servicio.Descripcion;
                existente.Precio = servicio.Precio;
                existente.idSede = servicio.idSede;

                db.SaveChanges();
                return "Servicio actualizado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al actualizar el servicio: " + ex.Message;
            }
        }

        public string Eliminar(int idServicio)
        {
            try
            {
                var servicio = Consultar(idServicio);
                if (servicio == null)
                {
                    return "El servicio no existe.";
                }

                db.Servicios.Remove(servicio);
                db.SaveChanges();
                return "Servicio eliminado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al eliminar el servicio: " + ex.Message;
            }
        }
    }
}
