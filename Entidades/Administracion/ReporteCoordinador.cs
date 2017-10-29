using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Administracion
{
    public class ReporteCoordinador
    {
        [Required(ErrorMessage = "La cédula es requerida")]
        [DisplayName("Cédula")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "La fecha desde es requerida")]
        [DisplayName("Fecha desde")]
        public Nullable<DateTime> FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha hasta es requerida")]
        [DisplayName("Fecha hasta")]
        public Nullable<DateTime> FechaFin { get; set; }

        [DisplayName("Estado")]
        public char Estado{ get; set; }

    }
}
