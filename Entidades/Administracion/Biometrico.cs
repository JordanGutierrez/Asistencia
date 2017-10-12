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
    public class Biometrico
    {
        [Required]
        [DisplayName("Código")]
        public int BiometricoID { get; set; }

        [Required(ErrorMessage = "El código de biométrico es requerido")]
        [DisplayName("Código")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "El código de biométrico solo acepta caracteres numéricos")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [DisplayName("Descripción")]
        [RegularExpression(@"^[A-Za-zÑñáéíóúÁÉÍÓÚ ]*$", ErrorMessage = "El nombre solo acepta caracteres alfabéticos")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La facultad es requerida")]
        [DisplayName("Facultad")]
        public int FacultadID { get; set; }

        [Required(ErrorMessage = "El estado es requerido")]
        [DisplayName("Estado")]
        public char Estado { get; set; }

        public static Biometrico CreateBiometricoFromDataRecord(IDataRecord dr)
        {
            Biometrico biometrico = new Biometrico();

            biometrico.BiometricoID = int.Parse(dr["BiometricoID"].ToString());
            biometrico.Descripcion = dr["Descripcion"].ToString();
            biometrico.FacultadID = int.Parse(dr["FacultadID"].ToString());
            biometrico.Codigo = dr["Codigo"].ToString();
            biometrico.Estado = char.Parse(dr["Estado"].ToString());

            return biometrico;
        }

    }
}
