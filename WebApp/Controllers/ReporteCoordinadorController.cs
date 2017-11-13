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
    public class ReporteCoordinadorController : BaseController
    {
        IReporteDAO reporteDAO = new ReporteDAO();
        IUsuarioDAO usuarioDAO = new UsuarioDAO();
        IFacultadDAO facultadDAO = new FacultadDAO();

        // GET: ReporteEmpleado/Print
        [AppAuthorize("00029")]
        public ActionResult PrintCoordinador()
        {
            ReporteCoordinador reportecoordinador = new ReporteCoordinador();
            reportecoordinador.FechaInicio = DateTime.Now;
            reportecoordinador.FechaFin = DateTime.Now;
            ViewBag.RolID = Utils.Utils.GetClaim("RolID");
            int usuarioID = int.Parse(Utils.Utils.GetClaim("UsuarioID"));
            return View(reportecoordinador);
        }

        // POST: ReporteEmpleado/Print
        [HttpPost]
        [AppAuthorize("00029")]
        public ActionResult PrintCoordinador(ReporteCoordinador reportecoordinador)
        {
            var cedula = reportecoordinador.Cedula;
            string mensaje = string.Empty;
            bool existe = false;

            if (!String.IsNullOrEmpty(cedula))
            {
                int facultad = int.Parse(Utils.Utils.GetClaim("FacultadID"));
                existe = usuarioDAO.getAllUsuario(ref mensaje).Where(x => x.FacultadID == facultad && x.Cedula == cedula).Any();
            }
            
            ViewBag.RolID = Utils.Utils.GetClaim("RolID"); //ojo
            try
            {
                if (existe)
                {  
                    if (reportecoordinador.FechaFin < reportecoordinador.FechaInicio)
                    {
                        Warning("La fecha hasta debe ser mayor a la fecha desde", "ReporteEmpleado", true);
                        return View(reportecoordinador);
                    }
                    var rol = Utils.Utils.GetClaim("RolID");
                    int usuarioID = int.Parse(Utils.Utils.GetClaim("UsuarioID"));


                    if (rol == "2")
                    {
                        reportecoordinador.Cedula = usuarioDAO.getUsuario(usuarioID, ref mensaje).Cedula;
                    }
                    ReportViewer reportviewer = new ReportViewer();
                    reportviewer.ProcessingMode = ProcessingMode.Local;
                    reportviewer.LocalReport.ReportPath = Server.MapPath("~\\Reportes\\Asistencia.rdlc");

                    reportecoordinador.FacultadID = int.Parse(Utils.Utils.GetClaim("FacultadID"));
                    DataSet ds = reporteDAO.getReporteCoordinador(reportecoordinador, ref mensaje);
                    ReportDataSource datasourceCabecera = new ReportDataSource("dtCabecera", ds.Tables[0]);
                    reportviewer.LocalReport.DataSources.Clear();
                    reportviewer.LocalReport.DataSources.Add(datasourceCabecera);
                    ReportDataSource datasourceDetalle = new ReportDataSource("dtDetalle", ds.Tables[1]);
                    reportviewer.LocalReport.DataSources.Add(datasourceDetalle);
                    ReportDataSource datasourceTotalFaltas = new ReportDataSource("dtTotalFaltas", ds.Tables[2]);
                    reportviewer.LocalReport.DataSources.Add(datasourceTotalFaltas);

                    reportviewer.LocalReport.SetParameters(new ReportParameter("FechaDesde", reportecoordinador.FechaInicio.Value.ToShortDateString()));
                    reportviewer.LocalReport.SetParameters(new ReportParameter("FechaHasta", reportecoordinador.FechaFin.Value.ToShortDateString()));

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
                    Warning("No se puede Generar reporte, el usuario que intenta filtrar no pertenece a la facultad del coordinador", "ReporteCoordinador", true);
                    return View(reportecoordinador);
                }
                
                
            }
            catch (Exception ex)
            {
                Warning(ex.Message, "ReporteCoordinador", true);
                return View(reportecoordinador);
            }
        }
    }
}
