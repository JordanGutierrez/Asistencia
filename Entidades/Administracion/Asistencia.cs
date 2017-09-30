using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Administracion
{
    public class Asistencia
    {
        [Required]
        [DisplayName("Código")]
        public int AsistenciaID { get; set; }

        [Required]
        [DisplayName("Código de Usuario")]
        public string CodigoUsuario { get; set; }

        public Nullable<DateTime> Fecha { get; set; }

        public Nullable<TimeSpan> HoraEntrada { get; set; }

        public Nullable<TimeSpan> HoraSalida { get; set; }

        public Nullable<TimeSpan> HoraEntradaLunh { get; set; }

        public Nullable<TimeSpan> HoraSalidaLunch { get; set; }

        public char Estado { get; set; }

        public static Asistencia CreateAsistenciaFromDataRecord(IDataRecord dr)
        {
            Asistencia asistencia = new Asistencia();

            //asistencia.AsistenciaID = int.Parse(dr["AsistenciaID"].ToString());
            //asistencia.CodigoUsuario = dr["CodigoUsuario"].ToString();
            //asistencia.HoraEntrada = TimeSpan.Parse(dr["HoraEntrada"]);
            //asistencia.HoraSalida = TimeSpan.Parse(dr["HoraSalida"]);
            //asistencia.HoraEntradaLunh = TimeSpan.Parse(dr["HoraEntradaLunch"]);
            //asistencia.HoraSalidaLunch = TimeSpan.Parse(dr["HoraSalidaLunch"]);
            //asistencia.Estado = char.Parse(dr["Estado"].ToString());

            return asistencia;
        }
    }
}
