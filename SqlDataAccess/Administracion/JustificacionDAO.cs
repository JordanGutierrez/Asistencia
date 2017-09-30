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

        public Justificacion getJustificacion(int id, ref string mensaje)
        {
            throw new NotImplementedException();
        }

        public void insertJustificacion(Justificacion justificacion, string user, ref string mensaje)
        {
            throw new NotImplementedException();
        }

        public void updateJustificacion(Justificacion justificacion, string user, ref string mensaje)
        {
            throw new NotImplementedException();
        }
    }
}
