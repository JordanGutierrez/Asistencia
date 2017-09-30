using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Administracion
{
    public class Actividad
    {
        public int ActividadID { get; set; }

        public int ProcesoID { get; set; }

        public string Descripcion { get; set; }

        public int NumeroHoras { get; set; }

        public static Actividad CreateActividadFromDataRecord(IDataRecord dr)
        {
            Actividad actividad = new Actividad();

            actividad.ActividadID = int.Parse(dr["ActividadID"].ToString());
            actividad.ProcesoID = int.Parse(dr["ProcesoID"].ToString());
            actividad.Descripcion = dr["Descripcion"].ToString();
            actividad.NumeroHoras = int.Parse(dr["NumeroHoras"].ToString());

            return actividad;
        }
    }
}
