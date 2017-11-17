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

        public DataTable getAllAsistencia(ref string mensaje)
        {
            DataTable dt = null;
            sql.Comando.CommandText = "SELECT ASI.AsistenciaID"
                                    + " ,ASI.Fecha"
                                    + " ,ASI.Estado"
                                    + " ,FAC.FacultadID"
                                    + " ,CONCAT(USU.Nombres, ' ', USU.Apellidos)    AS Usuario"
                                    + " FROM tbasistencia        AS ASI"
                                    + " INNER JOIN tbusuario AS USU"                                   
                                    + " ON      ASI.CodigoUsuario = USU.CodigoBiometrico"
                                    + " INNER JOIN tbcarrera AS CAR"
                                    + " ON      CAR.CarreraID = USU.CarreraID"
                                    + " INNER JOIN tbfacultad AS  FAC"
                                    + " ON      FAC.FacultadID = CAR.FacultadID";


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

        public DataTable getAllAsistenciaEstados(ref string mensaje)
        {
            DataTable dtEstados = null;
            sql.Comando.CommandText = "SELECT ASI.AsistenciaID"
                                    + " ,ASI.Fecha"
                                    + " ,ASI.Estado"
                                    + " ,CONCAT(USU.Nombres, ' ', USU.Apellidos)    AS Usuario"
                                    + " ,JUS.Comentario     AS Comentario"
                                    + " ,FAC.FacultadID"
                                    + " FROM tbasistencia        AS ASI"
                                    + " INNER JOIN tbusuario AS USU"
                                    + " ON      ASI.CodigoUsuario = USU.CodigoBiometrico"
                                    + " INNER JOIN tbjustificacion AS JUS"
                                    + " ON ASI.AsistenciaID = JUS.AsistenciaID"
                                    + " INNER JOIN tbcarrera AS CAR"
                                    + " ON      CAR.CarreraID = USU.CarreraID"
                                    + " INNER JOIN tbfacultad AS  FAC"
                                    + " ON      FAC.FacultadID = CAR.FacultadID";

            try
            {
                dtEstados = sql.EjecutaDataTable(ref mensaje);
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                sql.CerrarConexion();
            }

            return dtEstados;
        }
    }
}
