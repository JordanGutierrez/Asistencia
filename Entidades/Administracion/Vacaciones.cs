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
    public class Vacaciones
    {
        public int VacacionesID { get; set; }

        [DisplayName("Usuario")]
        public int UsuarioID { get; set; }

        [DisplayName("Usuario")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es requerida")]
        [DisplayName("Fecha de Inicio")]
        public Nullable<DateTime> FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de fin es requerida")]
        [DisplayName("Fecha de Fin")]
        public Nullable<DateTime> FechaFin { get; set; }

        [Required(ErrorMessage = "El motivo es requerido")]
        [DisplayName("Motivo")]
        public string Motivo { get; set; }

        [Required(ErrorMessage = "El archivo es requerido")]
        [DisplayName("Archivo")]
        public byte[] Archivo { get; set; }

        public char Estado { get; set; }

        public static Vacaciones CreateVacacionesFromDataRecord(IDataRecord dr)
        {
            Vacaciones vacaciones = new Vacaciones();

            vacaciones.UsuarioID = int.Parse(dr["UsuarioID"].ToString());
            vacaciones.VacacionesID = int.Parse(dr["VacacionesID"].ToString());
            vacaciones.Estado = char.Parse(dr["Estado"].ToString());
            vacaciones.Motivo = dr["Motivo"].ToString();
            vacaciones.NombreUsuario = dr["NombreUsuario"].ToString();
            vacaciones.FechaInicio = DateTime.Parse(dr["FechaInicio"].ToString());
            vacaciones.FechaFin = DateTime.Parse(dr["FechaFin"].ToString());
            vacaciones.Archivo = (byte[])dr["Archivo"];

            return vacaciones;
        }
    }
}
