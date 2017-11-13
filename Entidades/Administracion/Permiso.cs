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
    public class Permiso
    {
        public int PermisoID { get; set; }

        public int FacultadID { get; set; }

        [DisplayName("Usuario")]
        public int UsuarioID { get; set; }

        [DisplayName("Usuario")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "La fecha es requerida")]
        [DisplayName("Fecha")]
        public Nullable<DateTime> Fecha { get; set; }

        [Required(ErrorMessage = "El motivo es requerido")]
        [DisplayName("Motivo")]
        public string Motivo { get; set; }

        public char Estado { get; set; }

        [Required(ErrorMessage = "El archivo es requerido")]
        [DisplayName("Archivo")]
        public byte[] Archivo { get; set; }

        
        [DisplayName("Comentario")]
        public string Comentario { get; set; }

        public static Permiso CreatePermisoFromDataRecord(IDataRecord dr)
        {
            Permiso permiso = new Permiso();

            permiso.UsuarioID = int.Parse(dr["UsuarioID"].ToString());
            permiso.PermisoID = int.Parse(dr["PermisoID"].ToString());
            permiso.FacultadID = int.Parse(dr["FacultadID"].ToString());
            permiso.Estado = char.Parse(dr["Estado"].ToString());
            permiso.Motivo = dr["Motivo"].ToString();
            permiso.NombreUsuario = dr["NombreUsuario"].ToString();
            permiso.Fecha = DateTime.Parse(dr["Fecha"].ToString());
            permiso.Archivo = (byte[])dr["Archivo"];
            permiso.Comentario = dr["Comentario"].ToString();

            return permiso;
        }
    }
}
