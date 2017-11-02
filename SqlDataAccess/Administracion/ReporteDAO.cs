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
            sql.Comando.Parameters.AddWithValue("P_FacultadID", reporte.FacultadID);

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

        public DataSet getReporteCoordinador(ReporteCoordinador reportecoordinador, ref string mensaje)
        {
            DataSet ds = null;

            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_ReporteAsistencia";
            sql.Comando.Parameters.AddWithValue("P_Cedula", reportecoordinador.Cedula);
            sql.Comando.Parameters.AddWithValue("P_FechaInicio", reportecoordinador.FechaInicio);
            sql.Comando.Parameters.AddWithValue("P_FechaFin", reportecoordinador.FechaFin);
            sql.Comando.Parameters.AddWithValue("P_Estado", reportecoordinador.Estado);
            sql.Comando.Parameters.AddWithValue("P_FacultadID", reportecoordinador.FacultadID);

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

        public DataTable getReporteEstadistico(Reporte reporte, ref string mensaje)
        {
            DataTable dt = null;

            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_ReporteEstadistico";
            sql.Comando.Parameters.AddWithValue("P_FechaInicio", reporte.FechaInicio);
            sql.Comando.Parameters.AddWithValue("P_FechaFin", reporte.FechaFin);
            sql.Comando.Parameters.AddWithValue("P_FacultadID", reporte.FacultadID);

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

        public DataTable getReportePermiso(Reporte reporte, ref string mensaje)
        {
            DataTable dt = null;

            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_ReportePermisoGeneral";
            sql.Comando.Parameters.AddWithValue("P_FechaInicio", reporte.FechaInicio);
            sql.Comando.Parameters.AddWithValue("P_FechaFin", reporte.FechaFin);
            sql.Comando.Parameters.AddWithValue("P_Estado", reporte.Estado);
            sql.Comando.Parameters.AddWithValue("P_Cedula", reporte.Cedula);
            sql.Comando.Parameters.AddWithValue("P_FacultadID", reporte.FacultadID);

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

        public DataTable getReporteVacaciones(Reporte reporte, ref string mensaje)
        {
            DataTable dt = null;

            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_ReporteVacacionesGeneral";
            sql.Comando.Parameters.AddWithValue("P_FechaInicio", reporte.FechaInicio);
            sql.Comando.Parameters.AddWithValue("P_FechaFin", reporte.FechaFin);
            sql.Comando.Parameters.AddWithValue("P_Estado", reporte.Estado);
            sql.Comando.Parameters.AddWithValue("P_Cedula", reporte.Cedula);
            sql.Comando.Parameters.AddWithValue("P_FacultadID", reporte.FacultadID);

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
