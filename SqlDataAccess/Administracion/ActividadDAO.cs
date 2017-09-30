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
    public class ActividadDAO : IActividadDAO
    {
        ConsultasSQL sql = new ConsultasSQL();

        public List<Actividad> getAllActividad(ref string mensaje)
        {
            List<Actividad> actividades = new List<Actividad>();
            sql = new ConsultasSQL();
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_getAllActividad";

            try
            {
                IDataReader reader = sql.EjecutaReader(ref mensaje);
                while (reader.Read())
                {
                    actividades.Add(Actividad.CreateActividadFromDataRecord(reader));
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return actividades;
        }

        public void insertActividad(Actividad actividad, string usuario, ref string mensaje)
        {
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_insertActividad";
            sql.Comando.Parameters.AddWithValue("@ProcesoID", actividad.ProcesoID);
            sql.Comando.Parameters.AddWithValue("@Descripcion", actividad.Descripcion);
            sql.Comando.Parameters.AddWithValue("@NumeroHoras", actividad.NumeroHoras);

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
