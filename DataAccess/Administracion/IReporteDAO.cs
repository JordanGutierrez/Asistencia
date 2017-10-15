using Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Administracion
{
    public interface IReporteDAO
    {
        DataSet getReporte(Reporte reporte, ref string mensaje);

        DataTable getReporteGenearl(Reporte reporte, ref string mensaje);

        DataTable getReportePermiso(Reporte reporte, ref string mensaje);

        DataTable getReporteVacaciones(Reporte reporte, ref string mensaje);

    }
}
