using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SpaVehiculosProyecto.Models;

namespace SpaVehiculosProyecto.clases
{
    public class clsEmpleado
    {
        private SpaVehiculosEntities db = new SpaVehiculosEntities();
        public Empleado empleado { get; set; }

        public string Registrar(Empleado empleado)
        {
            try
            {
                db.Empleadoes.Add(empleado);
                db.SaveChanges();
                return "Empleado registrado con éxito.";
            }
            catch (Exception ex)
            {
                return "Error al registrar el empleado: " + ex.Message;
            }
        }

        public Empleado Consultar(int idEmpleado)
        {
            return db.Empleadoes.FirstOrDefault(e => e.idEmpleado == idEmpleado);
        }

        public string Actualizar(Empleado empleado)
        {
            try
            {
                var existente = Consultar(empleado.idEmpleado);
                if (existente == null)
                {
                    return "El empleado no existe.";
                }

                // Actualizar campos según tu modelo
                existente.Nombre = empleado.Nombre;
                existente.Apellido = empleado.Apellido;
                
                

                db.SaveChanges();
                return "Empleado actualizado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al actualizar el empleado: " + ex.Message;
            }
        }

        public string Eliminar(int idEmpleado)
        {
            try
            {
                var empleado = Consultar(idEmpleado);
                if (empleado == null)
                {
                    return "El empleado no existe.";
                }

                db.Empleadoes.Remove(empleado);
                db.SaveChanges();
                return "Empleado eliminado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al eliminar el empleado: " + ex.Message;
            }
        }
    }
}