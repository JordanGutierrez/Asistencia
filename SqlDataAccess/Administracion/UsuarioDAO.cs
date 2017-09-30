using DataAccess.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Administracion;
using SqlDataAccess.Utils;
using System.Data;

namespace SqlDataAccess.Administracion
{
    public class UsuarioDAO : IUsuarioDAO
    {
        ConsultasSQL sql = new ConsultasSQL();

        public List<Usuario> getAllUsuario(ref string mensaje)
        {
            List<Usuario> usuarios = new List<Usuario>();
            sql = new ConsultasSQL();
            //sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "SELECT * FROM TbUsuario";

            try
            {
                IDataReader reader = sql.EjecutaReader(ref mensaje);
                while (reader.Read())
                {
                    usuarios.Add(Usuario.CreateUsuarioFromDataRecord(reader));
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return usuarios;
        }

        public Usuario getUsuario(int id, ref string mensaje)
        {
            Usuario usuario = new Usuario();
            sql = new ConsultasSQL();
            //sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "SELECT * FROM tbUsuario WHERE UsuarioID = " + id.ToString();

            try
            {
                IDataReader reader = sql.EjecutaReader(ref mensaje);
                while (reader.Read())
                {
                    usuario = Usuario.CreateUsuarioFromDataRecord(reader);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return usuario;
        }

        public void insertUsuario(Usuario usuario, string user, string clave, ref string mensaje)
        {
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_insertUsuario";
            sql.Comando.Parameters.AddWithValue("P_UserName", usuario.UserName);
            sql.Comando.Parameters.AddWithValue("P_BiometricoID", usuario.BiometricoID);
            sql.Comando.Parameters.AddWithValue("P_Clave", clave.ToString());
            sql.Comando.Parameters.AddWithValue("P_Cedula", usuario.Cedula);
            sql.Comando.Parameters.AddWithValue("P_Nombres", usuario.Nombres);
            sql.Comando.Parameters.AddWithValue("P_Apellidos", usuario.Apellidos);
            sql.Comando.Parameters.AddWithValue("P_Correo", usuario.Correo);
            sql.Comando.Parameters.AddWithValue("P_Telefono", usuario.Telefono);
            sql.Comando.Parameters.AddWithValue("P_Celular", usuario.Celular);
            sql.Comando.Parameters.AddWithValue("P_HorarioID", usuario.HorarioID);
            sql.Comando.Parameters.AddWithValue("P_CarreraID", usuario.CarreraID);
            sql.Comando.Parameters.AddWithValue("P_RolID", usuario.RolID);
            sql.Comando.Parameters.AddWithValue("P_UsuarioCreacion", user);
            sql.Comando.Parameters.AddWithValue("P_FechaCreacion", DateTime.Now); 
            try
            {
                sql.EjecutaQuery(ref mensaje);
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
        }

        public void updateUsuario(Usuario usuario, string user, ref string mensaje)
        {
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_updateUsuario";
            sql.Comando.Parameters.AddWithValue("P_UsuarioID", usuario.UsuarioID);
            sql.Comando.Parameters.AddWithValue("P_BiometricoID", usuario.BiometricoID);
            sql.Comando.Parameters.AddWithValue("P_UserName", usuario.UserName);
            sql.Comando.Parameters.AddWithValue("P_Cedula", usuario.Cedula);
            sql.Comando.Parameters.AddWithValue("P_Nombres", usuario.Nombres);
            sql.Comando.Parameters.AddWithValue("P_Apellidos", usuario.Apellidos);
            sql.Comando.Parameters.AddWithValue("P_Correo", usuario.Correo);
            sql.Comando.Parameters.AddWithValue("P_Telefono", usuario.Telefono);
            sql.Comando.Parameters.AddWithValue("P_Celular", usuario.Celular);
            sql.Comando.Parameters.AddWithValue("P_HorarioID", usuario.HorarioID);
            sql.Comando.Parameters.AddWithValue("P_CarreraID", usuario.CarreraID);
            sql.Comando.Parameters.AddWithValue("P_RolID", usuario.RolID);
            sql.Comando.Parameters.AddWithValue("P_UsuarioModificacion", user);
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
