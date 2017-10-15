using DataAccess.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Administracion;
using System.Data;
using SqlDataAccess.Utils;

namespace SqlDataAccess.Administracion
{
    public class ReporteDAO : IReporteDAO
    {
        ConsultasSQL sql = new ConsultasSQL();

        public DataSet getReporte(Reporte reporte, ref string mensaje)
        {
            DataSet ds = null;

            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_ReporteAsistencia";
            sql.Comando.Parameters.AddWithValue("P_Cedula", reporte.Cedula);
            sql.Comando.Parameters.AddWithValue("P_FechaInicio", reporte.FechaInicio);
            sql.Comando.Parameters.AddWithValue("P_FechaFin", reporte.FechaFin);
            sql.Comando.Parameters.AddWithValue("P_Estado", reporte.Estado);
            try
            {
                ds = sql.EjecutaDataSet(ref mensaje);
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return ds;
        }

        public DataTable getReporteGenearl(Reporte reporte, ref string mensaje)
        {
            DataTable dt = null;

            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_ReporteAsistenciaGeneral";
            sql.Comando.Parameters.AddWithValue("P_FechaInicio", reporte.FechaInicio);
            sql.Comando.Parameters.AddWithValue("P_FechaFin", reporte.FechaFin);
            sql.Comando.Parameters.AddWithValue("P_Estado", reporte.Estado);
            try
            {
                dt = sql.EjecutaDataTable(ref mensaje);
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return dt;
        }

    }
}
