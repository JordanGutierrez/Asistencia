using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Catalogos
{
    public class Pasantia
    {
        public int PasantiaID { get; set; }

        public string Nombre { get; set; }

        public static Pasantia CreatePasantiaFromDataRecord(IDataRecord dr)
        {
            Pasantia pasantia = new Pasantia();

            pasantia.PasantiaID = int.Parse(dr["PasantiaID"].ToString());
            pasantia.Nombre = dr["Nombre"].ToString();

            return pasantia;
        }
    }
}
