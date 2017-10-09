using DataAccess.Administracion;
using Entidades.Administracion;
using Microsoft.Reporting.WebForms;
using SqlDataAccess.Administracion;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class ReporteController : BaseController
    {
        IReporteDAO reporteDAO = new ReporteDAO();
        IUsuarioDAO usuarioDAO = new UsuarioDAO();

        // GET: Reporte/Create
        [AppAuthorize("00029")]
        public ActionResult Print()
        {
            Reporte reporte = new Reporte();
            reporte.FechaInicio = DateTime.Now;
            reporte.FechaFin = DateTime.Now;
            ViewBag.RolID = Utils.Utils.GetClaim("RolID");
            int usuarioID = int.Parse(Utils.Utils.GetClaim("UsuarioID"));
            return View(reporte);
        }

        // POST: Reporte/Create
        [HttpPost]
        [AppAuthorize("00029")]
        public ActionResult Print(Reporte reporte)
        {
            try
            {
                if(reporte.FechaFin < reporte.FechaInicio)
                {
                    Warning("La fecha hasta debe ser mayor a la fecha desde", "Reporte", true);
                    return View(reporte);
                }
                var rol = Utils.Utils.GetClaim("RolID");
                int usuarioID = int.Parse(Utils.Utils.GetClaim("UsuarioID"));
                string mensaje = string.Empty;

                if (rol == "2")
                {
                    reporte.Cedula = usuarioDAO.getUsuario(usuarioID, ref mensaje).Cedula;
                }
                ReportViewer reportviewer = new ReportViewer();
                reportviewer.ProcessingMode = ProcessingMode.Local;
                reportviewer.LocalReport.ReportPath = Server.MapPath("~\\Reportes\\Asistencia.rdlc");

                DataSet ds = reporteDAO.getReporte(reporte, ref mensaje);
                ReportDataSource datasourceCabecera = new ReportDataSource("dtCabecera", ds.Tables[0]);
                reportviewer.LocalReport.DataSources.Clear();
                reportviewer.LocalReport.DataSources.Add(datasourceCabecera);
                ReportDataSource datasourceDetalle = new ReportDataSource("dtDetalle", ds.Tables[1]);
                reportviewer.LocalReport.DataSources.Add(datasourceDetalle);
                ReportDataSource datasourceTotalFaltas = new ReportDataSource("dtTotalFaltas", ds.Tables[2]);
                reportviewer.LocalReport.DataSources.Add(datasourceTotalFaltas);

                reportviewer.LocalReport.SetParameters(new ReportParameter("FechaDesde", reporte.FechaInicio.Value.ToShortDateString()));
                reportviewer.LocalReport.SetParameters(new ReportParameter("FechaHasta", reporte.FechaFin.Value.ToShortDateString()));

                Byte[] mybytes = reportviewer.LocalReport.Render("PDF");
                using (FileStream fs = System.IO.File.Create(@"D:\Reporte.pdf"))
                {
                    fs.Write(mybytes, 0, mybytes.Length);
                }

                return File(mybytes, "application/pdf");
            }
            catch(Exception ex)
            {
                Warning(ex.Message, "Reporte", true);
                return View(reporte);
            }
        }
    }
}
