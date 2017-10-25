using DataAccess.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Administracion;
using System.Data;
using SqlDataAccess.Utils;

namespace SqlDataAccess.Administracion
{
    public class JustificacionDAO : IJustificacionDAO
    {
        ConsultasSQL sql = new ConsultasSQL();

        public List<Justificacion> getAllJustificacion(ref string mensaje)
        {
            List<Justificacion> justificacion = new List<Justificacion>();
            sql = new ConsultasSQL();
            sql.Comando.CommandText = "SELECT * FROM tbJustificacion";

            try
            {
                IDataReader reader = sql.EjecutaReader(ref mensaje);
                while (reader.Read())
                {
                    justificacion.Add(Justificacion.CreateJustificacionFromDataRecord(reader));
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return justificacion;
        }

        public Justificacion getJustificacionByAsistencia(int asistenciaid, ref string mensaje)
        {
            Justificacion justificacion = new Justificacion();
            sql = new ConsultasSQL();
            sql.Comando.CommandText = "SELECT * FROM tbJustificacion WHERE AsistenciaID = " + asistenciaid;

            try
            {
                IDataReader reader = sql.EjecutaReader(ref mensaje);
                while (reader.Read())
                {
                    justificacion = Justificacion.CreateJustificacionFromDataRecord(reader);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return justificacion;
        }

        public void insertJustificacion(Justificacion justificacion, string user, ref string mensaje)
        {
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_insertJustificacion";
            sql.Comando.Parameters.AddWithValue("P_AsistenciaID", justificacion.AsistenciaID);
            sql.Comando.Parameters.AddWithValue("P_Archivo", justificacion.Archivo);
            sql.Comando.Parameters.AddWithValue("P_User", user);
            sql.Comando.Parameters.AddWithValue("P_Comentario", justificacion.Comentario);
            try
            {
                sql.EjecutaQuery(ref mensaje);
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
        }

        public void insertJustificacionAtraso(Justificacion justificacion, string user, ref string mensaje)
        {
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_insertJustificacionAtraso";
            sql.Comando.Parameters.AddWithValue("P_AsistenciaID", justificacion.AsistenciaID);
            sql.Comando.Parameters.AddWithValue("P_User", user);
            sql.Comando.Parameters.AddWithValue("P_Comentario", justificacion.Comentario);
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
