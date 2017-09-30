using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Administracion
{
    public class AppMenu
    {
        public string VistaID { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public string Controlador { get; set; }

        public string Vista { get; set; }

        public char Estado { get; set; }

        public string PadreID { get; set; }

        public int Orden { get; set; }

        public static AppMenu CreateAppMenuFromDataRecord(IDataReader reader)
        {
            var appMenu = new AppMenu();

            appMenu.VistaID = reader["VistaID"].ToString();
            appMenu.Titulo = reader["Titulo"].ToString();
            appMenu.Descripcion = reader["Descripcion"].ToString();
            appMenu.Controlador = reader["Controlador"].ToString();
            appMenu.Vista = reader["Vista"].ToString();
            appMenu.PadreID = reader["PadreID"].ToString();
            appMenu.Orden = int.Parse(reader["Orden"].ToString());
            appMenu.Estado = char.Parse(reader["Estado"].ToString());

            return appMenu;
        }
    }
}
