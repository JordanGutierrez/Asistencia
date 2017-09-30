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
    public class Proceso
    {
        [Required]
        [DisplayName("Código")]
        public int ProcesoID { get; set; }

        [Required(ErrorMessage = "El tipo de Pasantía es requerido")]
        [DisplayName("Pasantía")]
        public int PasantiaID { get; set; }

        [DisplayName("Pasantía")]
        public string Pasantia { get; set; }

        [Required(ErrorMessage = "El pasante es requerido")]
        [DisplayName("Pasante")]
        public int PasanteID { get; set; }

        [Required(ErrorMessage = "El pasante es requerido")]
        [DisplayName("Pasante")]
        public string Pasante { get; set; }

        [DisplayName("Tutor")]
        public int TutorID { get; set; }

        [DisplayName("Tutor")]
        public string Tutor { get; set; }

        [DisplayName("Institución")]
        public int InstitucionID { get; set; }

        [Required(ErrorMessage = "La institución es requerida")]
        [DisplayName("Institución")]
        public string Institucion { get; set; }

        [Required(ErrorMessage = "El departamento es requerido")]
        [DisplayName("Departamento")]
        [RegularExpression(@"^[A-Za-zÑñáéíóúÁÉÍÓÚ ]*$", ErrorMessage = "El departamento solo acepta caracteres alfabéticos")]
        public string Departamento { get; set; }

        [Required(ErrorMessage = "El nombre del supervisor es requerido")]
        [DisplayName("Nombre del Supervisor")]
        [RegularExpression(@"^[A-Za-zÑñáéíóúÁÉÍÓÚ ]*$", ErrorMessage = "El nmbre del supervisor solo acepta caracteres alfabéticos")]
        public string Supervisor { get; set; }

        [Required(ErrorMessage = "El correo del supervisor es requerido")]
        [DisplayName("Correo del Supervisor")]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "Debe ingresar un correo válido")]
        public string CorreoSupervisor { get; set; }

        [Required(ErrorMessage = "El cargo del supervisor es requerido")]
        [DisplayName("Cargo del Supervisor")]
        [RegularExpression(@"^[A-Za-zÑñáéíóúÁÉÍÓÚ ]*$", ErrorMessage = "El cargo del supervisor solo acepta caracteres alfabéticos")]
        public string CargoSupervisor { get; set; }

        [Required(ErrorMessage = "La fecha de inicio de pasantías es requerida")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Fecha de Inicio de Pasantías")]
        public Nullable<DateTime> FechaInicioPasantias { get; set; }

        [Required(ErrorMessage = "La fecha de fin de pasantías es requerida")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Fecha de Fin de Pasantías")]
        public Nullable<DateTime> FechaFinPasantias { get; set; }

        [Required(ErrorMessage = "El estado es requerido")]
        [DisplayName("Estado")]
        public char Estado { get; set; }

        public static Proceso CreateProcesoFromDataRecord(IDataRecord dr)
        {
            Proceso proceso = new Proceso();

            proceso.ProcesoID = int.Parse(dr["ProcesoID"].ToString());
            proceso.PasantiaID = int.Parse(dr["PasantiaID"].ToString());
            proceso.Pasantia = dr["Pasantia"].ToString();
            proceso.PasanteID = int.Parse(dr["PasanteID"].ToString());
            proceso.Pasante = dr["Pasante"].ToString();
            proceso.TutorID = int.Parse(dr["TutorID"].ToString());
            proceso.Tutor = dr["Tutor"].ToString();
            proceso.InstitucionID = int.Parse(dr["InstitucionID"].ToString());
            proceso.Institucion = dr["Institucion"].ToString();
            proceso.Departamento = dr["Departamento"].ToString();
            proceso.Supervisor = dr["Supervisor"].ToString();
            proceso.CorreoSupervisor = dr["CorreoSupervisor"].ToString();
            proceso.CargoSupervisor = dr["CargoSupervisor"].ToString();
            proceso.FechaInicioPasantias = DateTime.Parse(dr["FechaInicioPasantias"].ToString());
            proceso.FechaFinPasantias = DateTime.Parse(dr["FechaFinPasantias"].ToString());
            proceso.Estado = char.Parse(dr["Estado"].ToString());

            return proceso;
        }

    }
}
