using DataAccess.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SqlDataAccess.Utils;

namespace SqlDataAccess.Administracion
{
    public class AsistenciaDAO : IAsistenciaDAO
    {
        ConsultasSQL sql = new ConsultasSQL();

        public DataTable getAllAsistenciasByEstado(char estado, ref string mensaje)
        {
            DataTable dt = null;
            sql.Comando.CommandText = "SELECT ASI.AsistenciaID"
		                            + " ,ASI.Fecha"
		                            + " ,CONCAT(USU.Nombres, ' ', USU.Apellidos)    AS Usuario"
                                    + " FROM tbasistencia        AS ASI"
                                    + " INNER JOIN tbusuario AS USU"
                                    + " ON      ASI.CodigoUsuario = USU.CodigoBiometrico"
                                    + " WHERE ASI.Estado = '" + estado + "'";

            try
            {
                dt = sql.EjecutaDataTable(ref mensaje);
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                sql.CerrarConexion();
            }

            return dt;
        }
    }
}
