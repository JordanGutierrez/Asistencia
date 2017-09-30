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

        [Required(ErrorMessage = "El nombre es requerido")]
        [DisplayName("Nombre")]
        [RegularExpression(@"^[A-Za-zÑñáéíóúÁÉÍÓÚ ]*$", ErrorMessage = "El nombre solo acepta caracteres alfabéticos")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La carrera es requerida")]
        [DisplayName("Carrera")]
        public int CarreraID { get; set; }

        public static Biometrico CreateBiometricoFromDataRecord(IDataRecord dr)
        {
            Biometrico biometrico = new Biometrico();

            biometrico.BiometricoID = int.Parse(dr["BiometricoID"].ToString());
            biometrico.Descripcion = dr["Descripcion"].ToString();
            biometrico.CarreraID = int.Parse(dr["CarreraID"].ToString());
            //facultad.Estado = char.Parse(dr["Estado"].ToString());

            return biometrico;
        }

    }
}
