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
        IFacultadDAO facultadDAO = new FacultadDAO();

        
        // GET: ReporteEmpleado/PrintEmpleado
        [AppAuthorize("00037")]
        public ActionResult PrintEmpleado()
        {
            Reporte reporte = new Reporte();
            reporte.FechaInicio = DateTime.Now;
            reporte.FechaFin = DateTime.Now;
            ViewBag.RolID = Utils.Utils.GetClaim("RolID");
            int usuarioID = int.Parse(Utils.Utils.GetClaim("UsuarioID"));
            return View(reporte);
        }

        // POST: ReporteEmpleado/Print
        [HttpPost]
        [AppAuthorize("00037")]
        public ActionResult PrintEmpleado(Reporte reporte)
        {
            try
            {
                if (reporte.FechaFin < reporte.FechaInicio)
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

                reporte.FacultadID = int.Parse(Utils.Utils.GetClaim("FacultadID"));
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
            catch (Exception ex)
            {
                Warning(ex.Message, "ReporteEmpleado", true);
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
            string mensaje = string.Empty;
            try
            {
                if (reporte.FechaFin < reporte.FechaInicio)
                {
                    Warning("La fecha hasta debe ser mayor a la fecha desde", "Reporte", true);
                    return View(reporte);
                }

                ReportViewer reportviewer = new ReportViewer();
                reportviewer.ProcessingMode = ProcessingMode.Local;
                reportviewer.LocalReport.ReportPath = Server.MapPath("~\\Reportes\\AsistenciaGeneral.rdlc");

                reporte.FacultadID = int.Parse(Utils.Utils.GetClaim("FacultadID"));
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
            var cedula = reporte.Cedula;
            string mensaje = string.Empty;
            bool existe = false;

            if (!String.IsNullOrEmpty(cedula))
            {
                int facultad = int.Parse(Utils.Utils.GetClaim("FacultadID"));
                existe = usuarioDAO.getAllUsuario(ref mensaje).Where(x => x.FacultadID == facultad && x.Cedula == cedula).Any();
            
                ViewBag.RolID = Utils.Utils.GetClaim("RolID"); //ojo
                try
                {
                    if (existe)
                    {
                        if (reporte.FechaFin < reporte.FechaInicio)
                            {
                                Warning("La fecha hasta debe ser mayor a la fecha desde", "Reporte", true);
                                return View(reporte);
                            }

                            if (string.IsNullOrEmpty(reporte.Cedula) || string.IsNullOrWhiteSpace(reporte.Cedula))
                                reporte.Cedula = "O"; 

                            ReportViewer reportviewer = new ReportViewer();
                            reportviewer.ProcessingMode = ProcessingMode.Local;
                            reportviewer.LocalReport.ReportPath = Server.MapPath("~\\Reportes\\PermisoGeneral.rdlc");

                            reporte.FacultadID = int.Parse(Utils.Utils.GetClaim("FacultadID"));
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
                    else
                    {
                        Warning("No se puede Generar reporte, el usuario que intenta filtrar no pertenece a la facultad del coordinador", "Reporte", true);
                        return View(reporte);
                    }
                }
                catch (Exception ex)
                {
                    Warning(ex.Message, "Reporte", true);
                    return View(reporte);
                }
            }
            else
            {

                ViewBag.RolID = Utils.Utils.GetClaim("RolID"); //ojo
                try
                {
                    if (!existe)
                    {
                        if (reporte.FechaFin < reporte.FechaInicio)
                        {
                            Warning("La fecha hasta debe ser mayor a la fecha desde", "Reporte", true);
                            return View(reporte);
                        }

                        if (string.IsNullOrEmpty(reporte.Cedula) || string.IsNullOrWhiteSpace(reporte.Cedula))
                            reporte.Cedula = "O";

                        ReportViewer reportviewer = new ReportViewer();
                        reportviewer.ProcessingMode = ProcessingMode.Local;
                        reportviewer.LocalReport.ReportPath = Server.MapPath("~\\Reportes\\PermisoGeneral.rdlc");

                        reporte.FacultadID = int.Parse(Utils.Utils.GetClaim("FacultadID"));
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
                    else
                    {
                        Warning("No se puede Generar reporte, el usuario que intenta filtrar no pertenece a la facultad del coordinador", "Reporte", true);
                        return View(reporte);
                    }
                }
                catch (Exception ex)
                {
                    Warning(ex.Message, "Reporte", true);
                    return View(reporte);
                }
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
            var cedula = reporte.Cedula;
            string mensaje = string.Empty;
            bool existe = false;

            if (!String.IsNullOrEmpty(cedula))
            {
                int facultad = int.Parse(Utils.Utils.GetClaim("FacultadID"));
                existe = usuarioDAO.getAllUsuario(ref mensaje).Where(x => x.FacultadID == facultad && x.Cedula == cedula).Any();
            
                ViewBag.RolID = Utils.Utils.GetClaim("RolID"); //ojo
                try
                {
                    if (existe)
                    {
                        if (reporte.FechaFin < reporte.FechaInicio)
                        {
                            Warning("La fecha hasta debe ser mayor a la fecha desde", "Reporte", true);
                            return View(reporte);
                        }

                        if (string.IsNullOrEmpty(reporte.Cedula) || string.IsNullOrWhiteSpace(reporte.Cedula))
                           reporte.Cedula = "O";

                        ReportViewer reportviewer = new ReportViewer();
                        reportviewer.ProcessingMode = ProcessingMode.Local;
                        reportviewer.LocalReport.ReportPath = Server.MapPath("~\\Reportes\\VacacionesGeneral.rdlc");

                        reporte.FacultadID = int.Parse(Utils.Utils.GetClaim("FacultadID"));
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
                    else
                    {
                        Warning("No se puede Generar reporte, el usuario que intenta filtrar no pertenece a la facultad del coordinador", "Reporte", true);
                        return View(reporte);
                    }
                }
                catch (Exception ex)
                {
                    Warning(ex.Message, "Reporte", true);
                    return View(reporte);
                }
            }
            else
            {
                ViewBag.RolID = Utils.Utils.GetClaim("RolID"); //ojo
                try
                {
                    if (!existe)
                    {
                        if (reporte.FechaFin < reporte.FechaInicio)
                        {
                            Warning("La fecha hasta debe ser mayor a la fecha desde", "Reporte", true);
                            return View(reporte);
                        }

                        if (string.IsNullOrEmpty(reporte.Cedula) || string.IsNullOrWhiteSpace(reporte.Cedula))
                            reporte.Cedula = "O";

                        ReportViewer reportviewer = new ReportViewer();
                        reportviewer.ProcessingMode = ProcessingMode.Local;
                        reportviewer.LocalReport.ReportPath = Server.MapPath("~\\Reportes\\VacacionesGeneral.rdlc");

                        reporte.FacultadID = int.Parse(Utils.Utils.GetClaim("FacultadID"));
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
                    else
                    {
                        Warning("No se puede Generar reporte, el usuario que intenta filtrar no pertenece a la facultad del coordinador", "Reporte", true);
                        return View(reporte);
                    }
                }
                catch (Exception ex)
                {
                    Warning(ex.Message, "Reporte", true);
                    return View(reporte);
                }
            }
        }

        // GET: Reporte/ReporteEstadistico
        public ActionResult ReporteEstadistico()
        {
            string mensaje = string.Empty;
            Reporte reporte = new Reporte();
            reporte.FechaInicio = DateTime.Now;
            reporte.FechaFin = DateTime.Now;
            ViewBag.Facultades = facultadDAO.getAllFacultad(ref mensaje).Where(x => x.Estado == 'A');
            return View(reporte);
        }

        // POST: Reporte/ReporteEstadistico
        [HttpPost]
        public ActionResult ReporteEstadistico(Reporte reporte)
        {
            string mensaje = string.Empty;
            ViewBag.Facultades = facultadDAO.getAllFacultad(ref mensaje).Where(x => x.Estado == 'A');
            try
            {
                if (reporte.FechaFin < reporte.FechaInicio)
                {
                    Warning("La fecha hasta debe ser mayor a la fecha desde", "Reporte", true);
                    return View(reporte);
                }

                ReportViewer reportviewer = new ReportViewer();
                reportviewer.ProcessingMode = ProcessingMode.Local;
                reportviewer.LocalReport.ReportPath = Server.MapPath("~\\Reportes\\EstadisticasAsistenciaGeneral.rdlc");

                DataTable dt = reporteDAO.getReporteEstadistico(reporte, ref mensaje);
                ReportDataSource datasourceCabecera = new ReportDataSource("dtEstadistico", dt);
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
