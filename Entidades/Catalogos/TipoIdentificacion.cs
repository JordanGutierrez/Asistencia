using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Catalogos
{
    public class TipoIdentificacion
    {
        public int TipoIdentificacionID { get; set; }

        public string Descripcion { get; set; }

        public static TipoIdentificacion CreateTipoIdentificacionFromDataRecord(IDataRecord dr)
        {
            TipoIdentificacion tipoidentificacion = new TipoIdentificacion();

            tipoidentificacion.TipoIdentificacionID = int.Parse(dr["TipoIdentificacionID"].ToString());
            tipoidentificacion.Descripcion = dr["Descripcion"].ToString();

            return tipoidentificacion;
        }

    }
}
