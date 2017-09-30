using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Entidades.Administracion
{
    public class Rol
    {
        [Required]
        [DisplayName("Código")]
        public int RolID { get; set; }

        [Required]
        [DisplayName("Nombre")]
        [RegularExpression("^[a-zA-Z áéíóúÁÉÍÓÚ]*$", ErrorMessage = "El nombre sólo acepta caracteres alfabéticos")]
        [MaxLength(50, ErrorMessage = "El nombre del rol debe tener máximo 50 caracteres")]
        public string Nombre { get; set; }

        [Required]
        [DisplayName("Descripción")]
        [RegularExpression("^[a-zA-Z áéíóúÁÉÍÓÚ]*$", ErrorMessage = "La descripción sólo acepta caracteres alfabéticos")]
        [MaxLength(150, ErrorMessage = "La descripción del rol debe tener máximo 150 caracteres")]
        public string Descripcion { get; set; }

        [Required]
        [DisplayName("Estado")]
        public char Estado { get; set; }

        public static Rol CreateRolFromDataRecord(IDataRecord dr)
        {
            Rol rol = new Rol();

            rol.RolID = int.Parse(dr["RolID"].ToString());
            rol.Nombre = dr["Nombre"].ToString();
            rol.Descripcion = dr["Descripcion"].ToString();
            rol.Estado = char.Parse(dr["Estado"].ToString());

            return rol;
        }
    }
}
