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
        public int AsistenciaID { get; set; }

        [Required(ErrorMessage = "El comentario es requerido")]
        [DisplayName("Comentario")]
        public string Comentario { get; set; }

        [Required(ErrorMessage = "El archivo es requerido")]
        [DisplayName("Archivo")]
        public byte[] Archivo { get; set; }

        public static Justificacion CreateJustificacionFromDataRecord(IDataRecord dr)
        {
            Justificacion justificacion = new Justificacion();

            justificacion.JustificacionID = int.Parse(dr["JustificacionID"].ToString());
            justificacion.AsistenciaID = int.Parse(dr["AsistenciaID"].ToString());
            justificacion.Comentario = dr["Comentario"].ToString();
            justificacion.Archivo = (byte[])dr["Archivo"];

            return justificacion;
        }
    }
}
