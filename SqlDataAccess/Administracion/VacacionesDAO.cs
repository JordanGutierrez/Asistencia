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
    public class VacacionesDAO : IVacacionesDAO
    {

        ConsultasSQL sql = new ConsultasSQL();

        public List<Vacaciones> getAllVacaciones(ref string mensaje)
        {
            List<Vacaciones> vacaciones = new List<Vacaciones>();
            sql = new ConsultasSQL();
            sql.Comando.CommandText = "SELECT	PER.*"
                                    + " ,concat(USU.Apellidos, ' ', USU.Nombres) AS NombreUsuario"
                                    + " FROM tbVacaciones     AS PER"
                                    + " INNER JOIN tbusuario  AS USU"
                                    + " ON      PER.UsuarioID = USU.UsuarioID"
                                    + " WHERE USU.Estado      = 'A'";

            try
            {
                IDataReader reader = sql.EjecutaReader(ref mensaje);
                while (reader.Read())
                {
                    vacaciones.Add(Vacaciones.CreateVacacionesFromDataRecord(reader));
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return vacaciones;
        }

        public List<Vacaciones> getAllVacacionesByEstado(char estado, ref string mensaje)
        {
            List<Vacaciones> vacaciones = new List<Vacaciones>();
            sql = new ConsultasSQL();
            sql.Comando.CommandText = "SELECT	PER.*"
                                    + " ,concat(USU.Apellidos, ' ', USU.Nombres) AS NombreUsuario"
                                    + " FROM tbVacaciones     AS PER"
                                    + " INNER JOIN tbusuario  AS USU"
                                    + " ON      PER.UsuarioID = USU.UsuarioID"
                                    + " WHERE USU.Estado      = 'A'"
                                    + " AND PER.Estado = '" + estado + "'";

            try
            {
                IDataReader reader = sql.EjecutaReader(ref mensaje);
                while (reader.Read())
                {
                    vacaciones.Add(Vacaciones.CreateVacacionesFromDataRecord(reader));
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return vacaciones;
        }

        public Vacaciones getVacaciones(int id, ref string mensaje)
        {
            Vacaciones vacaciones = new Vacaciones();
            sql = new ConsultasSQL();
            sql.Comando.CommandText = "SELECT	PER.*"
                                    + " ,concat(USU.Apellidos, ' ', USU.Nombres) AS NombreUsuario"
                                    + " FROM tbVacaciones     AS PER"
                                    + " INNER JOIN tbusuario  AS USU"
                                    + " ON      PER.UsuarioID = USU.UsuarioID"
                                    + " WHERE USU.Estado = 'A'"
                                    + " AND PER.PermisoID = " + id;
            try
            {
                IDataReader reader = sql.EjecutaReader(ref mensaje);
                while (reader.Read())
                {
                    vacaciones = Vacaciones.CreateVacacionesFromDataRecord(reader);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return vacaciones;
        }

        public void insertVacaciones(Vacaciones vacaciones, string usuario, ref string mensaje)
        {
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_insertVacaciones";
            sql.Comando.Parameters.AddWithValue("P_UsuarioID", vacaciones.UsuarioID);
            sql.Comando.Parameters.AddWithValue("P_FechaInicio", vacaciones.FechaInicio);
            sql.Comando.Parameters.AddWithValue("P_FechaFin", vacaciones.FechaFin);
            sql.Comando.Parameters.AddWithValue("P_Motivo", vacaciones.Motivo);
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

        public void updateVacaciones(Vacaciones vacaciones, string usuario, ref string mensaje)
        {
            throw new NotImplementedException();
        }

        public void updateVacacionesEstado(int id, string usuario, ref string mensaje)
        {
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_updateVacacionesEstado";
            sql.Comando.Parameters.AddWithValue("P_VacacionesID", id);
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
    }
}
