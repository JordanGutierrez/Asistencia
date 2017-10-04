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
    public class Usuario
    {
        public int UsuarioID { get; set; }

        [Required(ErrorMessage = "El biométrico es requerido")]
        [DisplayName("Biométrico")]
        public int BiometricoID { get; set; }

        [Required(ErrorMessage = "El código de biométrico es requerido")]
        [DisplayName("Código de Biométrico")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "El código de biométrico solo acepta caracteres numéricos")]
        public string CodigoBiometrico { get; set; }

        [Required(ErrorMessage = "La clave es requerida")]
        [DisplayName("Clave")]
        [DataType(DataType.Password)]
        public string Clave { get; set; }

        [Required(ErrorMessage = "La cédula es requerida")]
        [DisplayName("Cédula")]
        [MaxLength(10, ErrorMessage = "La cédula debe tener un máximo de 10 caracteres")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "La cédula solo acepta caracteres numéricos")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "Los nombres son requeridos")]
        [DisplayName("Nombres")]
        [RegularExpression(@"^[A-Za-zÑñáéíóúÁÉÍÓÚ ]*$", ErrorMessage = "El nombre solo acepta caracteres alfabéticos")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Los apellidos son requeridos")]
        [DisplayName("Apellidos")]
        [RegularExpression(@"^[A-Za-zÑñáéíóúÁÉÍÓÚ ]*$", ErrorMessage = "El apellido solo acepta caracteres alfabéticos")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El cargo es requerido")]
        [DisplayName("Cargo")]
        [RegularExpression(@"^[A-Za-zÑñáéíóúÁÉÍÓÚ ]*$", ErrorMessage = "El cargo solo acepta caracteres alfabéticos")]
        public string Cargo { get; set; }

        [Required(ErrorMessage = "El correo es requerido")]
        [DisplayName("Correo")]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "Debe ingresar un correo válido")]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El teléfono es requerido")]
        [DisplayName("Teléfono")]
        [MaxLength(9, ErrorMessage = "El teléfono debe tener un máximo de 9 caracteres")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "El teléfono solo acepta caracteres numéricos")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El celular es requerido")]
        [DisplayName("Celular")]
        [MaxLength(10, ErrorMessage = "El celular debe tener un máximo de 10 caracteres")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "El celular solo acepta caracteres numéricos")]
        public string Celular { get; set; }

        [Required(ErrorMessage = "El rol es requerido")]
        [DisplayName("Rol")]
        public int RolID { get; set; }

        [Required(ErrorMessage = "El horario es requerido")]
        [DisplayName("Horario")]
        public int HorarioID { get; set; }

        [Required(ErrorMessage = "La carrera es requerida")]
        [DisplayName("Carrera")]
        public int CarreraID { get; set; }

        [Required(ErrorMessage = "El estado es requerido")]
        [DisplayName("Estado")]
        public char Estado { get; set; }

        public static Usuario CreateUsuarioFromDataRecord(IDataRecord dr)
        {
            Usuario usuario = new Usuario();

            usuario.UsuarioID = int.Parse(dr["UsuarioID"].ToString());
            usuario.CodigoBiometrico = dr["CodigoBiometrico"].ToString();
            usuario.Clave = dr["Clave"].ToString();
            usuario.Cedula = dr["Cedula"].ToString();
            usuario.Nombres = dr["Nombres"].ToString();
            usuario.Apellidos = dr["Apellidos"].ToString();
            usuario.Cargo = dr["Cargo"].ToString();
            usuario.Correo = dr["Correo"].ToString();
            usuario.Telefono = dr["Telefono"].ToString();
            usuario.Celular = dr["Celular"].ToString();
            usuario.RolID = int.Parse(dr["RolID"].ToString());
            usuario.HorarioID = int.Parse(dr["HorarioID"].ToString());
            usuario.CarreraID = int.Parse(dr["CarreraID"].ToString());
            usuario.Estado = char.Parse(dr["Estado"].ToString());
            usuario.BiometricoID = int.Parse(dr["BiometricoID"].ToString());

            return usuario;
        }
    }
}
