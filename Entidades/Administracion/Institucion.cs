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
    public class Institucion
    {
        [Required]
        [DisplayName("Código")]
        public int InstitucionID { get; set; }

        [Required(ErrorMessage = "La razón social es requerida")]
        [DisplayName("Razón Social")]
        [RegularExpression(@"^[A-Za-zÑñáéíóúÁÉÍÓÚ ]*$", ErrorMessage = "La razón social solo acepta caracteres alfabéticos")]
        public string RazonSocial { get; set; }

        [Required(ErrorMessage = "El RUC es requerido")]
        [DisplayName("RUC")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "El RUC solo acepta caracteres numéricos")]
        public string Ruc { get; set; }

        [Required(ErrorMessage = "El representante legal es requerido")]
        [DisplayName("Representante Legal")]
        [RegularExpression(@"^[A-Za-zÑñáéíóúÁÉÍÓÚ ]*$", ErrorMessage = "El representante legal solo acepta caracteres alfabéticos")]
        public string RepresentanteLegal { get; set; }

        [Required(ErrorMessage = "La dirección es requerida")]
        [DisplayName("Dirección")]
        [RegularExpression(@"^[A-Za-zÑñáéíóúÁÉÍÓÚ ]*$", ErrorMessage = "La dirección solo acepta caracteres alfabéticos")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El teléfono es requerido")]
        [DisplayName("Teléfono")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "El teléfono solo acepta caracteres numéricos")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El correo es requerido")]
        [DisplayName("Correo")]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "Debe ingresar un correo válido")]
        public string Correo { get; set; }

        [DisplayName("Activo")]
        public bool Estado { get; set; }

        public static Institucion CreateInstitucionFromDataRecord(IDataRecord dr)
        {
            Institucion institucion = new Institucion();

            institucion.InstitucionID = int.Parse(dr["InstitucionID"].ToString());
            institucion.RazonSocial = dr["RazonSocial"].ToString();
            institucion.Ruc = dr["Ruc"].ToString();
            institucion.RepresentanteLegal = dr["RepresentanteLegal"].ToString();
            institucion.Direccion = dr["Direccion"].ToString();
            institucion.Telefono = dr["Telefono"].ToString();
            institucion.Correo = dr["Correo"].ToString();
            institucion.Estado = bool.Parse(dr["Estado"].ToString());

            return institucion;
        }

    }
}
