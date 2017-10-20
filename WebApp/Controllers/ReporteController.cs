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

        // GET: Reporte/Print
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

        // POST: Reporte/Print
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
                MemoryStream ms = new MemoryStream(mybytes, 0, 0, true, true);
                Response.AddHeader("content-disposition", "attachment;filename= Reporte.pdf");
                Response.Buffer = true;
                Response.Clear();
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.End();
                return new FileStreamResult(Response.OutputStream, "application/pdf");
            }
            catch(Exception ex)
            {
                Warning(ex.Message, "Reporte", true);
                return View(reporte);
            }
        }

        // GET: Reporte/ReportGeneral
        [AppAuthorize("00030")]
        public ActionResult ReportGeneral()
        {
            Reporte reporte = new Reporte();
            reporte.FechaInicio = DateTime.Now;
            reporte.FechaFin = DateTime.Now;
            return View(reporte);
        }

        // POST: Reporte/ReportGeneral
        [HttpPost]
        [AppAuthorize("00030")]
        public ActionResult ReportGeneral(Reporte reporte)
        {
            try
            {
                if (reporte.FechaFin < reporte.FechaInicio)
                {
                    Warning("La fecha hasta debe ser mayor a la fecha desde", "Reporte", true);
                    return View(reporte);
                }
                string mensaje = string.Empty;

                ReportViewer reportviewer = new ReportViewer();
                reportviewer.ProcessingMode = ProcessingMode.Local;
                reportviewer.LocalReport.ReportPath = Server.MapPath("~\\Reportes\\AsistenciaGeneral.rdlc");

                DataTable dt = reporteDAO.getReporteGenearl(reporte, ref mensaje);
                ReportDataSource datasourceCabecera = new ReportDataSource("dtDetalle", dt);
                reportviewer.LocalReport.DataSources.Clear();
                reportviewer.LocalReport.DataSources.Add(datasourceCabecera);

                reportviewer.LocalReport.SetParameters(new ReportParameter("FechaDesde", reporte.FechaInicio.Value.ToShortDateString()));
                reportviewer.LocalReport.SetParameters(new ReportParameter("FechaHasta", reporte.FechaFin.Value.ToShortDateString()));

                Byte[] mybytes = reportviewer.LocalReport.Render("PDF");

                MemoryStream ms = new MemoryStream(mybytes, 0, 0, true, true);
                Response.AddHeader("content-disposition", "attachment;filename= Reporte.pdf");
                Response.Buffer = true;
                Response.Clear();
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.End();
                return new FileStreamResult(Response.OutputStream, "application/pdf");
            }
            catch (Exception ex)
            {
                Warning(ex.Message, "Reporte", true);
                return View(reporte);
            }
        }

        // GET: Reporte/ReportPermiso
        [AppAuthorize("00032")]
        public ActionResult ReportPermiso()
        {
            Reporte reporte = new Reporte();
            reporte.FechaInicio = DateTime.Now;
            reporte.FechaFin = DateTime.Now;
            return View(reporte);
        }

        // POST: Reporte/ReportPermiso
        [HttpPost]
        [AppAuthorize("00032")]
        public ActionResult ReportPermiso(Reporte reporte)
        {
            try
            {
                if (reporte.FechaFin < reporte.FechaInicio)
                {
                    Warning("La fecha hasta debe ser mayor a la fecha desde", "Reporte", true);
                    return View(reporte);
                }

                if (string.IsNullOrEmpty(reporte.Cedula) || string.IsNullOrWhiteSpace(reporte.Cedula))
                    reporte.Cedula = "O"; 

                string mensaje = string.Empty;

                ReportViewer reportviewer = new ReportViewer();
                reportviewer.ProcessingMode = ProcessingMode.Local;
                reportviewer.LocalReport.ReportPath = Server.MapPath("~\\Reportes\\PermisoGeneral.rdlc");

                DataTable dt = reporteDAO.getReportePermiso(reporte, ref mensaje);
                ReportDataSource datasourceCabecera = new ReportDataSource("dtPermiso", dt);
                reportviewer.LocalReport.DataSources.Clear();
                reportviewer.LocalReport.DataSources.Add(datasourceCabecera);

                reportviewer.LocalReport.SetParameters(new ReportParameter("FechaDesde", reporte.FechaInicio.Value.ToShortDateString()));
                reportviewer.LocalReport.SetParameters(new ReportParameter("FechaHasta", reporte.FechaFin.Value.ToShortDateString()));

                Byte[] mybytes = reportviewer.LocalReport.Render("PDF");

                MemoryStream ms = new MemoryStream(mybytes, 0, 0, true, true);
                Response.AddHeader("content-disposition", "attachment;filename= Reporte.pdf");
                Response.Buffer = true;
                Response.Clear();
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.End();
                return new FileStreamResult(Response.OutputStream, "application/pdf");
            }
            catch (Exception ex)
            {
                Warning(ex.Message, "Reporte", true);
                return View(reporte);
            }
        }

        // GET: Reporte/ReportVacaciones
        [AppAuthorize("00033")]
        public ActionResult ReportVacaciones()
        {
            Reporte reporte = new Reporte();
            reporte.FechaInicio = DateTime.Now;
            reporte.FechaFin = DateTime.Now;
            return View(reporte);
        }

        // POST: Reporte/ReportVacaciones
        [HttpPost]
        [AppAuthorize("00033")]
        public ActionResult ReportVacaciones(Reporte reporte)
        {
            try
            {
                if (reporte.FechaFin < reporte.FechaInicio)
                {
                    Warning("La fecha hasta debe ser mayor a la fecha desde", "Reporte", true);
                    return View(reporte);
                }

                if (string.IsNullOrEmpty(reporte.Cedula) || string.IsNullOrWhiteSpace(reporte.Cedula))
                    reporte.Cedula = "O";

                string mensaje = string.Empty;

                ReportViewer reportviewer = new ReportViewer();
                reportviewer.ProcessingMode = ProcessingMode.Local;
                reportviewer.LocalReport.ReportPath = Server.MapPath("~\\Reportes\\VacacionesGeneral.rdlc");

                DataTable dt = reporteDAO.getReporteVacaciones(reporte, ref mensaje);
                ReportDataSource datasourceCabecera = new ReportDataSource("dtVacaciones", dt);
                reportviewer.LocalReport.DataSources.Clear();
                reportviewer.LocalReport.DataSources.Add(datasourceCabecera);

                reportviewer.LocalReport.SetParameters(new ReportParameter("FechaDesde", reporte.FechaInicio.Value.ToShortDateString()));
                reportviewer.LocalReport.SetParameters(new ReportParameter("FechaHasta", reporte.FechaFin.Value.ToShortDateString()));

                Byte[] mybytes = reportviewer.LocalReport.Render("PDF");

                MemoryStream ms = new MemoryStream(mybytes, 0, 0, true, true);
                Response.AddHeader("content-disposition", "attachment;filename= Reporte.pdf");
                Response.Buffer = true;
                Response.Clear();
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.End();
                return new FileStreamResult(Response.OutputStream, "application/pdf");
            }
            catch (Exception ex)
            {
                Warning(ex.Message, "Reporte", true);
                return View(reporte);
            }
        }

    }
}
