using DataAccess.Administracion;
using Entidades.Administracion;
using SqlDataAccess.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlDataAccess.Administracion
{
    public class RolDAO: IRolDAO
    {
        ConsultasSQL sql = new ConsultasSQL();

        public List<Rol> getAllRol(ref string mensaje)
        {
            List<Rol> roles = new List<Rol>();
            sql = new ConsultasSQL();
            //sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "SELECT * FROM tbRol";

            try
            {
                sql.AbrirConexion();
                IDataReader reader = sql.EjecutaReader(ref mensaje);
                while (reader.Read())
                {
                    roles.Add(Rol.CreateRolFromDataRecord(reader));
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

            return roles;
        }

        public Rol getRol(int id)
        {
            throw new NotImplementedException();
        }
    }
}
