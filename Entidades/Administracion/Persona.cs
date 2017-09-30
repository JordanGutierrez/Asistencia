using Foolproof;
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
    public class Persona
    {
        [Required]
        [DisplayName("Código")]
        public int PersonaID { get; set; }

        public int UsuarioID { get; set; }

        [Required]
        public int RolID { get; set; }

        [Required(ErrorMessage = "El tipo de identificación es requerido")]
        [DisplayName("Tipo de Identificación")]
        public int TipoIdentificacionID { get; set; }

        [Required(ErrorMessage = "El número de identificación es requerido")]
        [DisplayName("Número de identificación")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "El número de identificación solo acepta caracteres numéricos")]
        public string NumeroIdentificacion { get; set; }

        [Required(ErrorMessage = "El primer nombre es requerido")]
        [DisplayName("Primer Nombre")]
        [RegularExpression(@"^[A-Za-zÑñáéíóúÁÉÍÓÚ ]*$", ErrorMessage = "El primer nombre solo acepta caracteres alfabéticos")]
        [MaxLength(50, ErrorMessage ="Máximo 25 caracteres")]
        public string PrimerNombre { get; set; }

        [Required(ErrorMessage = "El segundo nombre es requerido")]
        [DisplayName("Segundo Nombre")]
        [RegularExpression(@"^[A-Za-zÑñáéíóúÁÉÍÓÚ ]*$", ErrorMessage = "El segundo nombre solo acepta caracteres alfabéticos")]
        [MaxLength(50, ErrorMessage = "Máximo 25 caracteres")]
        public string SegundoNombre { get; set; }

        [Required(ErrorMessage = "El apellido paterno es requerido")]
        [DisplayName("Apellido Paterno")]
        [RegularExpression(@"^[A-Za-zÑñáéíóúÁÉÍÓÚ ]*$", ErrorMessage = "El apellido paterno solo acepta caracteres alfabéticos")]
        [MaxLength(25, ErrorMessage = "Máximo 25 caracteres")]
        public string ApellidoPaterno { get; set; }

        [Required(ErrorMessage = "El apellido materno es requerido")]
        [DisplayName("Apellido Materno")]
        [RegularExpression(@"^[A-Za-zÑñáéíóúÁÉÍÓÚ ]*$", ErrorMessage = "El apellido materno solo acepta caracteres alfabéticos")]
        [MaxLength(50, ErrorMessage = "Máximo 25 caracteres")]
        public string ApellidoMaterno { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es requerida")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Fecha de Nacimiento")]
        public Nullable<DateTime> FechaNacimineto { get; set; }

        [Required(ErrorMessage = "El correo institucional es requerido")]
        [DisplayName("Correo Institucional")]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "Debe ingresar un correo válido")]
        public string CorreoInstitucional { get; set; }

        [RequiredIf("RolID", 1, ErrorMessage = "El correo personal es requerido")]
        [DisplayName("Correo Personal")]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "Debe ingresar un correo válido")]
        public string CorreoPersonal { get; set; }

        [Required(ErrorMessage = "El número de celular es requerido")]
        [DisplayName("Celular")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "El número celular solo acepta caracteres numéricos")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "El número de celular debe tener 10 dígitos")]
        public string Celular { get; set; }

        [Required(ErrorMessage = "La carrera es requerida")]
        [DisplayName("Carrera")]
        public int CarreraID { get; set; }

        //[Required(ErrorMessage = "La foto es requerida")]
        [DisplayName("Foto")]
        public byte[] Foto { get; set; }

        [DisplayName("Activo")]
        public bool Estado { get; set; }

        public static Persona CreatePersonaFromDataRecord(IDataRecord dr)
        {
            Persona persona = new Persona();

            persona.PersonaID = int.Parse(dr["PersonaID"].ToString());
            persona.UsuarioID = int.Parse(dr["UsuarioID"].ToString());
            persona.RolID = int.Parse(dr["RolID"].ToString());
            persona.TipoIdentificacionID = int.Parse(dr["TipoIdentificacionID"].ToString());
            persona.NumeroIdentificacion = dr["NumeroIdentificacion"].ToString();
            persona.PrimerNombre = dr["PrimerNombre"].ToString();
            persona.SegundoNombre = dr["SegundoNombre"].ToString();
            persona.ApellidoPaterno = dr["ApellidoPaterno"].ToString();
            persona.ApellidoMaterno = dr["ApellidoMaterno"].ToString();
            persona.FechaNacimineto = DateTime.Parse(dr["FechaNacimiento"].ToString());
            persona.Celular = dr["Celular"].ToString();
            persona.CorreoInstitucional = dr["CorreoInstitucional"].ToString();
            persona.CorreoPersonal = dr["CorreoPersonal"].ToString();
            persona.CarreraID = int.Parse(dr["CarreraID"].ToString());
            if(dr["Foto"] != DBNull.Value)
                persona.Foto = (byte[])(dr["Foto"]);
            
            persona.Estado = bool.Parse(dr["Estado"].ToString());

            return persona;
        }
    }
}
