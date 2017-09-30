using DataAccess.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Catalogos;
using SqlDataAccess.Utils;
using System.Data;

namespace SqlDataAccess.Catalogos
{
    public class PasantiaDAO : IPasantiaDAO
    {
        ConsultasSQL sql = new ConsultasSQL();

        public List<Pasantia> getAllPasantia(ref string mensaje)
        {
            List<Pasantia> pasantias = new List<Pasantia>();
            sql = new ConsultasSQL();
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_getAllPasantia";

            try
            {
                IDataReader reader = sql.EjecutaReader(ref mensaje);
                while (reader.Read())
                {
                    pasantias.Add(Pasantia.CreatePasantiaFromDataRecord(reader));
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return pasantias;
        }
    }
}
