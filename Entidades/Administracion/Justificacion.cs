using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Administracion
{
    public class Justificacion
    {
        [Required]
        [DisplayName("Código")]
        public int JustificacionID { get; set; }

        [Required]
        [DisplayName("Asistencia")]
        public string AsistenciaID { get; set; }

        [Required]
        [DisplayName("Archivo")]
        public byte[] Archivo { get; set; }

        [DisplayName("Estado")]
        public char Estado { get; set; }

        public static Justificacion CreateJustificacionFromDataRecord(IDataRecord dr)
        {
            Justificacion justificacion = new Justificacion();

            justificacion.JustificacionID = int.Parse(dr["JustificacionID"].ToString());
            justificacion.AsistenciaID = dr["AsistenciaID"].ToString();
            justificacion.Archivo = (byte[])dr["Archivo"];
            justificacion.Estado = char.Parse(dr["Estado"].ToString());

            return justificacion;
        }
    }
}
