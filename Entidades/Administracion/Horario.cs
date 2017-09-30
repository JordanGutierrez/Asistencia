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
    public class Horario
    {
        [Required]
        [DisplayName("Código")]
        public int HorarioID { get; set; }

        [Required]
        [DisplayName("Descripción")]
        [RegularExpression("^[a-zA-Z áéíóúÁÉÍÓÚ]*$", ErrorMessage = "La descripción sólo acepta caracteres alfabéticos")]
        [MaxLength(150, ErrorMessage = "La descripción del rol debe tener máximo 150 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La hora de entrada es requerida")]
        [DisplayName("Hora de Entrada")]
        public string HoraEntrada { get; set; }
        
        [Required(ErrorMessage = "La hora de salida es requerida")]
        [DisplayName("Hora de Salida")]
        public string HoraSalida { get; set; }

        public static Horario  CreateHorarioFromDataRecord(IDataRecord dr)
        {
            Horario horario = new Horario();

            horario.HorarioID = int.Parse(dr["HorarioID"].ToString());
            horario.Descripcion = dr["Descripcion"].ToString();
            horario.HoraEntrada = dr["HoraEntrada"].ToString();
            horario.HoraSalida = dr["HoraSalida"].ToString();

            return horario;
        }
    }
}
