using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using SpaVehiculosProyecto.Models;

namespace SpaVehiculosProyecto.clases
{
    public class clsCita
    {
        private SpaVehiculosEntities1 db = new SpaVehiculosEntities1();
        public Cita cita { get; set; }

        public string Registrar(Cita cita)
        {
            try
            {
                // No asignamos ninguna fecha aquí; se usará la que viene del frontend
                db.Citas.Add(cita);
                db.SaveChanges();
                return "Cita registrada con éxito.";
            }
            catch (Exception ex)
            {
                string mensajeError = ObtenerMensajesExcepcion(ex);
                return "Error al registrar la cita: " + mensajeError;
            }
        }

        private string ObtenerMensajesExcepcion(Exception ex)
        {
            var mensajes = new System.Text.StringBuilder();
            Exception currentEx = ex;
            while (currentEx != null)
            {
                mensajes.AppendLine(currentEx.Message);
                currentEx = currentEx.InnerException;
            }
            return mensajes.ToString();
        }
        public Cita Consultar(int idCita)
        {
            return db.Citas.FirstOrDefault(c => c.idCita == idCita);
        }
        public IEnumerable<Cita> ConsultarTodos()
        {
            try
            {
                return db.Citas.ToList(); // Devuelve todos los clientes
            }
            catch (Exception ex)
            {
                // Podrías lanzar la excepción o manejarla según tus necesidades
                throw new Exception("Error al consultar todos los clientes: " + ex.Message);
            }
        }

        public string Actualizar(Cita cita)
        {
            try
            {
                var existente = Consultar(cita.idCita);
                if (existente == null)
                {
                    return "La cita no existe.";
                }

                // Actualizamos campos (ajustar según modelo real)
                existente.idCliente = cita.idCliente;
                
                existente.Fecha = cita.Fecha;
                existente.Hora = cita.Hora;
                

                db.SaveChanges();
                return "Cita actualizada correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al actualizar la cita: " + ex.Message;
            }
        }

        public string Eliminar(int idCita)
        {
            try
            {
                var cita = Consultar(idCita);
                if (cita == null)
                {
                    return "La cita no existe.";
                }

                db.Citas.Remove(cita);
                db.SaveChanges();
                return "Cita eliminada correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al eliminar la cita: " + ex.Message;
            }
        }
    }
}
