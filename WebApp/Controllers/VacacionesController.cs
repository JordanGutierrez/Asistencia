using DataAccess.Administracion;
using Entidades.Administracion;
using SqlDataAccess.Administracion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class VacacionesController : BaseController
    {
        IUsuarioDAO usuarioDAO = new UsuarioDAO();
        IVacacionesDAO vacacionesDAO = new VacacionesDAO();

        // GET: Vacaciones
        [AppAuthorize("00020")]
        public ActionResult Index()
        {
            String mensaje = string.Empty;
            ViewBag.RolID = Utils.Utils.GetClaim("RolID");
            int usuarioID = int.Parse(Utils.Utils.GetClaim("UsuarioID"));
            List<Vacaciones> vacaciones = vacacionesDAO.getAllVacaciones(ref mensaje);
            if (ViewBag.RolID == "3")
                return View(vacaciones);
            else
                return View(vacaciones.Where(x => x.UsuarioID == usuarioID));
        }

        // GET: Vacaciones/Create
        [AppAuthorize("00021")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vacaciones/Create
        [HttpPost]
        [AppAuthorize("00021")]
        public ActionResult Create(HttpPostedFileBase Archivo, DateTime FechaInicio, DateTime FechaFin, string Motivo)
        {
            string mensaje = string.Empty;
            Vacaciones vacaciones = new Vacaciones();
            vacaciones.FechaInicio = FechaInicio;
            vacaciones.FechaFin = FechaFin;
            vacaciones.Motivo = Motivo;
            try
            {
                if (Archivo != null)
                {
                    string extension = Path.GetExtension(Archivo.FileName);
                    if (extension.ToUpper() == ".PDF")
                    {
                        if (FechaInicio > DateTime.Now.Date)
                        {
                            if (FechaFin > DateTime.Now)
                            {
                                if (FechaInicio.Date < FechaFin.Date)
                                {
                                    using (Stream inputStream = Archivo.InputStream)
                                    {
                                        MemoryStream memoryStream = inputStream as MemoryStream;
                                        if (memoryStream == null)
                                        {
                                            memoryStream = new MemoryStream();
                                            inputStream.CopyTo(memoryStream);
                                        }
                                        vacaciones.Archivo = memoryStream.ToArray();
                                        vacaciones.UsuarioID = int.Parse(Utils.Utils.GetClaim("UsuarioID"));
                                        vacacionesDAO.insertVacaciones(vacaciones, GetApplicationUser(), ref mensaje);
                                        if (mensaje == "OK")
                                        {
                                            Success("Vacaciones registradas con éxito", "Vacaciones", true);
                                            return RedirectToAction("Index");
                                        }
                                    }
                                }
                                else
                                    mensaje = "La fecha de fin debe ser mayor a la fecha de inicio de las vacaciones";
                            }
                            else
                                mensaje = "La fecha de fin debe ser mayor a la actual";
                        }
                        else
                            mensaje = "La fecha de inicio debe ser mayor a la actual";
                    }
                    else
                        mensaje = "La extensión del archivo debe ser PDF";
                }
                else
                    mensaje = "El archivo es requerido";

            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            Warning(mensaje, "Vacaciones", true);
            return View(vacaciones);
        }

        // GET: Vacaciones/Aprobar
        [AppAuthorize("00022")]
        public ActionResult Aprobar(int id)
        {
            string mensaje = string.Empty;
            try
            {
                vacacionesDAO.updateVacacionesEstado(id, GetApplicationUser(), ref mensaje);
                if (mensaje == "OK")
                {
                    Success("Vacaciones aprobadas con éxito", "Vacaciones", true);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            Warning(mensaje, "Vacaciones", true);
            return RedirectToAction("Index");
        }

        // GET: Vacaciones/Descargar
        [AppAuthorize("00023")]
        public ActionResult Descargar(int id)
        {
            string mensaje = string.Empty;
            Vacaciones vacaciones = vacacionesDAO.getVacaciones(id, ref mensaje);
            if (mensaje == "OK")
            {
                return File(vacaciones.Archivo, System.Net.Mime.MediaTypeNames.Application.Octet, "Vacaciones_" + vacaciones.VacacionesID + ".pdf");
            }
            else
            {
                Warning(mensaje, "Justificación", true);
                return View("Index");
            }
        }
    }
}
