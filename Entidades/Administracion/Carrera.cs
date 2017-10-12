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
    public class Carrera
    {
        [Required]
        [DisplayName("Código")]
        public int CarreraID { get; set; }

        [Required(ErrorMessage = "La facultad es requerida")]
        [DisplayName("Facultad")]
        public int FacultadID { get; set; }

        [Required(ErrorMessage = "La descripción es requerida")]
        [DisplayName("Descripción")]
        [RegularExpression(@"^[A-Za-zÑñáéíóúÁÉÍÓÚ ]*$", ErrorMessage = "La descripción solo acepta caracteres alfabéticos")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [DisplayName("Código")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "La facultad solo acepta caracteres numéricos")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "El estado es requerido")]
        [DisplayName("Estado")]
        public char Estado { get; set; }

        public static Carrera CreateCarreraFromDataRecord(IDataRecord dr)
        {
            Carrera carrera = new Carrera();

            carrera.CarreraID = int.Parse(dr["CarreraID"].ToString());
            carrera.FacultadID = int.Parse(dr["FacultadID"].ToString());
            carrera.Codigo = dr["Codigo"].ToString();
            carrera.Descripcion  = dr["Descripcion"].ToString();
            carrera.Estado = char.Parse(dr["Estado"].ToString());

            return carrera;
        }

    }
}
