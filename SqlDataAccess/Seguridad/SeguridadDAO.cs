using DataAccess.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Entidades.Administracion;
using SqlDataAccess.Utils;
using System.Data;
using DataAccess.Administracion;
using SqlDataAccess.Administracion;

namespace SqlDataAccess.Seguridad
{
    public class SeguridadDAO : ISeguridadDAO
    {
        string remoteAddress { get; set; }

        ConsultasSQL sql = new ConsultasSQL();
        IAppMenuDAO appmenuDAO = new AppMenuDAO();
        IUsuarioDAO usuarioDAO = new UsuarioDAO();

        public SeguridadDAO(string remoteAddress)
        {
            this.remoteAddress = remoteAddress;
        }

        public string authenticateUser(string userName, string password, out List<string> transacciones, out Usuario usuario)
        {
            usuario = null;
            string mensaje = string.Empty;
            var retValue = string.Empty;
            var loginHelper = new Usuario();
            loginHelper.Clave = password;
            loginHelper.Correo = userName;

            var host = Dns.GetHostEntry(Dns.GetHostName());

            string roles = string.Empty;
            transacciones = null;
            try
            {
                usuario = getUsuario(userName, ref mensaje);
                if (mensaje == "OK")
                {
                    if (usuario != null)
                    {
                        if (usuario.Estado == 'A')
                        {
                            if (usuario.Clave == GetStringSha256Hash(password))
                            {
                                List<AppMenu> menus = appmenuDAO.getAllbyRol(usuario.RolID, ref mensaje);
                                if(mensaje == "OK")
                                {
                                    transacciones = new List<string>();
                                    foreach (AppMenu menu in menus)
                                    {
                                        transacciones.Add(menu.VistaID);
                                    }
                                }
                            }
                            else
                                mensaje = "La clave o contraseña es incorrecta";
                        }
                        else
                        {
                            if(usuario.Estado == 'C')
                                mensaje = "Cambiar Contraseña";
                            else
                                mensaje = "El usuario se encuentra inactivo";
                        }
                    }
                    else
                        mensaje = "El usuario no existe";
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return mensaje;
        }

        internal static string GetStringSha256Hash(string text)
        {
            if (String.IsNullOrEmpty(text))
                return String.Empty;

            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }

        public Usuario getUsuario(string alias, ref string mensaje)
        {
            Usuario usuario = null;
            sql = new ConsultasSQL();
            sql.Comando.CommandText = "SELECT * FROM tbUsuario WHERE Correo = '" + alias + "'";

            try
            {
                IDataReader reader = sql.EjecutaReader(ref mensaje);
                if (reader.Read())
                {
                    usuario = new Usuario();
                    usuario =  Usuario.CreateUsuarioFromDataRecord(reader);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                sql.CerrarConexion();
            }

            return usuario;
        }

        private List<string> leerTransacciones(List<AppMenu> menus)
        {
            List<string> result = new List<string>();

            foreach (var menu in menus)
            {
                result.Add(menu.VistaID.ToString());
            }

            return result;
        }

        public void inserUsuario(Usuario usuario1, string usuario2, ref string mensaje)
        {
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_insertUsuario";
            sql.Comando.Parameters.AddWithValue("P_CodigoBiometrico", usuario1.CodigoBiometrico);
            sql.Comando.Parameters.AddWithValue("P_BiometricoID", usuario1.BiometricoID);
            sql.Comando.Parameters.AddWithValue("P_Clave", GetStringSha256Hash(usuario1.Cedula));
            sql.Comando.Parameters.AddWithValue("P_Cedula", usuario1.Cedula);
            sql.Comando.Parameters.AddWithValue("P_Nombres", usuario1.Nombres);
            sql.Comando.Parameters.AddWithValue("P_Apellidos", usuario1.Apellidos);
            sql.Comando.Parameters.AddWithValue("P_Correo", usuario1.Correo);
            sql.Comando.Parameters.AddWithValue("P_Telefono", usuario1.Telefono);
            sql.Comando.Parameters.AddWithValue("P_Celular", usuario1.Celular);
            sql.Comando.Parameters.AddWithValue("P_RolID", usuario1.RolID);
            sql.Comando.Parameters.AddWithValue("P_HorarioID", usuario1.HorarioID);
            sql.Comando.Parameters.AddWithValue("P_CarreraID", usuario1.CarreraID);
            sql.Comando.Parameters.AddWithValue("P_UsuarioCreacion", usuario2);

            try
            {
                sql.EjecutaQuery(ref mensaje);
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
        }
    }
}
