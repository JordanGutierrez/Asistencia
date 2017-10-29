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
    public class PermisoDAO : IPermisoDAO
    {

        ConsultasSQL sql = new ConsultasSQL();

        public List<Permiso> getAllPermiso(ref string mensaje)
        {
            List<Permiso> permisos = new List<Permiso>();
            sql = new ConsultasSQL();
            sql.Comando.CommandText = "SELECT	PER.*"
                                    + " ,concat(USU.Apellidos, ' ', USU.Nombres) AS NombreUsuario"
                                    + " FROM tbpermiso           AS PER"
                                    + " INNER JOIN tbusuario AS USU"
                                    + " ON      PER.UsuarioID = USU.UsuarioID"
                                    + " WHERE USU.Estado = 'A'";

            try
            {
                IDataReader reader = sql.EjecutaReader(ref mensaje);
                while (reader.Read())
                {
                    permisos.Add(Permiso.CreatePermisoFromDataRecord(reader));
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return permisos;
        }

        public List<Permiso> getAllPermisoByEstado(char estado, ref string mensaje)
        {
            List<Permiso> permisos = new List<Permiso>();
            sql = new ConsultasSQL();
            sql.Comando.CommandText = "SELECT	PER.*"
                                    + " ,concat(USU.Apellidos, ' ', USU.Nombres) AS NombreUsuario"
                                    + " FROM tbpermiso           AS PER"
                                    + " INNER JOIN tbusuario AS USU"
                                    + " ON      PER.UsuarioID = USU.UsuarioID"
                                    + " WHERE USU.Estado = 'A'"
                                    + " AND PER.Estado = '" + estado + "'";

            try
            {
                IDataReader reader = sql.EjecutaReader(ref mensaje);
                while (reader.Read())
                {
                    permisos.Add(Permiso.CreatePermisoFromDataRecord(reader));
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return permisos;
        }

        public Permiso getPermiso(int id, ref string mensaje)
        {
            Permiso permiso = new Permiso();
            sql = new ConsultasSQL();
            sql.Comando.CommandText = "SELECT	PER.*"
                                    + " ,concat(USU.Apellidos, ' ', USU.Nombres) AS NombreUsuario"
                                    + " FROM tbpermiso           AS PER"
                                    + " INNER JOIN tbusuario AS USU"
                                    + " ON      PER.UsuarioID = USU.UsuarioID"
                                    + " WHERE USU.Estado = 'A'"
                                    + " AND PER.PermisoID = " + id;
            try
            {
                IDataReader reader = sql.EjecutaReader(ref mensaje);
                while (reader.Read())
                {
                    permiso = Permiso.CreatePermisoFromDataRecord(reader);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return permiso;
        }

        public void insertPermiso(Permiso permiso, string usuario, ref string mensaje)
        {
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_insertPermiso";
            sql.Comando.Parameters.AddWithValue("P_UsuarioID", permiso.UsuarioID);
            sql.Comando.Parameters.AddWithValue("P_Fecha", permiso.Fecha);
            sql.Comando.Parameters.AddWithValue("P_Motivo", permiso.Motivo);
            sql.Comando.Parameters.AddWithValue("P_Archivo", permiso.Archivo);
            sql.Comando.Parameters.AddWithValue("P_User", usuario);
            try
            {
                sql.EjecutaQuery(ref mensaje);
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
        }

        public void updatePermiso(Permiso permiso, string usuario, ref string mensaje)
        {
            throw new NotImplementedException();
        }

        public void updatePermisoEstado(int id, string usuario, char estado, string comentario, ref string mensaje)
        {
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_updatePermisoEstado";
            sql.Comando.Parameters.AddWithValue("P_PermisoID", id);
            sql.Comando.Parameters.AddWithValue("P_User", usuario);
            sql.Comando.Parameters.AddWithValue("P_Estado", estado);
            sql.Comando.Parameters.AddWithValue("P_Comentario", comentario);

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
