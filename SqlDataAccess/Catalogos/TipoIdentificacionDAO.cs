using DataAccess.Catalogos;
using Entidades.Catalogos;
using SqlDataAccess.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlDataAccess.Catalogos
{
    public class TipoIdentificacionDAO:  ITipoIdentificacionDAO
    {
        ConsultasSQL sql = new ConsultasSQL();

        public List<TipoIdentificacion> getAllTipoIdentificacion(ref string mensaje)
        {
            List<TipoIdentificacion> tiposidentificacion = new List<TipoIdentificacion>();
            sql = new ConsultasSQL();
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_getAllTipoIdentificacion";

            try
            {
                IDataReader reader = sql.EjecutaReader(ref mensaje);
                while (reader.Read())
                {
                    tiposidentificacion.Add(TipoIdentificacion.CreateTipoIdentificacionFromDataRecord(reader));
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return tiposidentificacion;
        }
    }
}
