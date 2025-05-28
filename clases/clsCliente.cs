using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using SpaVehiculosProyecto.Models;

namespace SpaVehiculosProyecto.clases
{
	public class clsCliente
	{
		private SpaVehiculosEntities db = new SpaVehiculosEntities();
		public Cliente cliente { get; set; }

        public string Registrar(Cliente cliente)
        {
            try
            {
                db.Clientes.Add(cliente);
                db.SaveChanges();
                return "Cliente registrado con éxito.";
            }
            catch (Exception ex)
            {
                return "Error al registrar el cliente: " + ex.Message;
            }
        }

        public Cliente Consultar(int idCliente)
        {
            return db.Clientes.FirstOrDefault(c => c.idCliente == idCliente);
        }

        public string Actualizar(Cliente cliente)
        {
            try
            {
                var existente = Consultar(cliente.idCliente);
                if (existente == null)
                {
                    return "El cliente no existe.";
                }

                // Se actualizan campos
                existente.Nombre = cliente.Nombre;
                existente.Apellido = cliente.Apellido;
                existente.Telefono = cliente.Telefono;
                

                db.SaveChanges();
                return "Cliente actualizado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al actualizar el cliente: " + ex.Message;
            }
        }

        public string Eliminar(int idCliente)
        {
            try
            {
                var cliente = Consultar(idCliente);
                if (cliente == null)
                {
                    return "El cliente no existe.";
                }

                db.Clientes.Remove(cliente);
                db.SaveChanges();
                return "Cliente eliminado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al eliminar el cliente: " + ex.Message;
            }
        }

    }
}