using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SpaVehiculosProyecto.Models;

namespace SpaVehiculosProyecto.clases
{
    public class clsAuto
    {
        private SpaVehiculosEntities1 db = new SpaVehiculosEntities1();
        public Auto auto { get; set; }

        public string Registrar(Auto auto)
        {
            try
            {
                db.Autoes.Add(auto);
                db.SaveChanges();
                return "Auto registrado con éxito.";
            }
            catch (Exception ex)
            {
                return "Error al registrar el auto: " + ex.Message;
            }
        }

        public Auto Consultar(int idAuto)
        {
            return db.Autoes.FirstOrDefault(a => a.idAuto == idAuto);
        }

        public string Actualizar(Auto auto)
        {
            try
            {
                var existente = Consultar(auto.idAuto);
                if (existente == null)
                {
                    return "El auto no existe.";
                }

                // Actualizar propiedades (ajustar según el modelo real)
                existente.idCliente = auto.idCliente;
                existente.Marca = auto.Marca;
                existente.Modelo = auto.Modelo;
                existente.Placa = auto.Placa;
                

                db.SaveChanges();
                return "Auto actualizado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al actualizar el auto: " + ex.Message;
            }
        }

        public string Eliminar(int idAuto)
        {
            try
            {
                var auto = Consultar(idAuto);
                if (auto == null)
                {
                    return "El auto no existe.";
                }

                db.Autoes.Remove(auto);
                db.SaveChanges();
                return "Auto eliminado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al eliminar el auto: " + ex.Message;
            }
        }
    }
}