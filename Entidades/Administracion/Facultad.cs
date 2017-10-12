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
    public class Facultad
    {
        [Required]
        [DisplayName("Código")]
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

        public static Facultad CreateFacultadFromDataRecord(IDataRecord dr)
        {
            Facultad facultad = new Facultad();

            facultad.FacultadID = int.Parse(dr["FacultadID"].ToString());
            facultad.Descripcion = dr["Descripcion"].ToString();
            facultad.Codigo = dr["Codigo"].ToString();
            facultad.Estado = char.Parse(dr["Estado"].ToString());

            return facultad;
        }
    }
}
