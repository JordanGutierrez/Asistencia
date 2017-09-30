using DataAccess.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Administracion;
using SqlDataAccess.Utils;
using System.Data;

namespace SqlDataAccess.Administracion
{
    public class AppMenuDAO : IAppMenuDAO
    {
        ConsultasSQL sql = new ConsultasSQL();

        public List<AppMenu> getAllbyRol(int rolid, ref string mensaje)
        {
            List<AppMenu> menus = new List<AppMenu>();
            sql = new ConsultasSQL();
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_getMenu";
            sql.Comando.Parameters.AddWithValue("P_Rol", rolid);

            try
            {
                sql.AbrirConexion();
                IDataReader reader = sql.EjecutaReader(ref mensaje);
                while (reader.Read())
                {
                    menus.Add(AppMenu.CreateAppMenuFromDataRecord(reader));
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                sql.CerrarConexion();
            }

            return menus;
        }
    }
}
