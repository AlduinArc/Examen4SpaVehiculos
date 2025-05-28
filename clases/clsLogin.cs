using SpaVehiculosProyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace SpaVehiculosProyecto.clases
{
    public class clsLogin
    {
        public clsLogin()
        {
            loginRespuesta = new LoginRespuesta();
        }

        public SpaVehiculosEntities db = new SpaVehiculosEntities();
        public Login login { get; set; }
        public LoginRespuesta loginRespuesta { get; set; }

        public bool ValidarUsuario()
        {
            try
            {
                Empleado usuario = db.Empleadoes.FirstOrDefault(u => u.Usuario == login.Usuario);
                if (usuario == null)
                {
                    loginRespuesta.Autenticado = false;
                    loginRespuesta.Mensaje = "Usuario no existe";
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                loginRespuesta.Autenticado = false;
                loginRespuesta.Mensaje = ex.Message;
                return false;
            }
        }

        private bool ValidarClave()
        {
            try
            {
                Empleado usuario = db.Empleadoes.FirstOrDefault(u => u.Usuario == login.Usuario && u.Clave == login.Clave);
                if (usuario == null)
                {
                    loginRespuesta.Autenticado = false;
                    loginRespuesta.Mensaje = "La clave no coincide";
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                loginRespuesta.Autenticado = false;
                loginRespuesta.Mensaje = ex.Message;
                return false;
            }
        }

        public IQueryable<LoginRespuesta> Ingresar()
        {
            if (ValidarUsuario() && ValidarClave())
            {
                string token = TokenGenerator.GenerateTokenJwt(login.Usuario);
                Empleado usuario = db.Empleadoes
                    .FirstOrDefault(u => u.Usuario == login.Usuario && u.Clave == login.Clave);

                if (usuario != null)
                {
                    loginRespuesta.Usuario = usuario.Usuario;
                    loginRespuesta.Nombre = usuario.Nombre;
                    loginRespuesta.idEmpleado = usuario.idEmpleado;
                    loginRespuesta.Autenticado = true;
                    loginRespuesta.Token = token;
                    loginRespuesta.Mensaje = "Login exitoso";
                }
                else
                {
                    loginRespuesta.Autenticado = false;
                    loginRespuesta.Mensaje = "Usuario no encontrado tras validación";
                }
            }

            return new List<LoginRespuesta> { loginRespuesta }.AsQueryable();
        }
    }

}