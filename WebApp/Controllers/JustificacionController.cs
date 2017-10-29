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
    public class JustificacionController : BaseController
    {
        IJustificacionDAO justificacionDAO = new JustificacionDAO();
        IAsistenciaDAO asistenciaDAO = new AsistenciaDAO();

        // GET: Justificacion
        [AppAuthorize("00017")]
        public ActionResult Index()
        {
            string mensaje = string.Empty;
            ViewBag.Faltas = asistenciaDAO.getAllAsistencia(ref mensaje);
            ViewBag.Atraso = asistenciaDAO.getAllAsistencia(ref mensaje);
            return View();
        }

        // GET: Justificacion/Create
        [AppAuthorize("00026")]
        public ActionResult Justificar(int asistencia)
        {
            Justificacion justificacion = new Justificacion();
            justificacion.AsistenciaID = asistencia;
            return View(justificacion);
        }

        // POST: Justificacion/Create
        [HttpPost]
        [AppAuthorize("00026")]
        public ActionResult Justificar(HttpPostedFileBase Archivo, int AsistenciaID, string Comentario)
        {
            string mensaje = string.Empty;
            Justificacion justificacion = new Justificacion();

            try
            {
                if (Archivo != null)
                {
                    string extension = Path.GetExtension(Archivo.FileName);
                    if (extension.ToUpper() == ".PDF")
                    {
                        using (Stream inputStream = Archivo.InputStream)
                        {
                            MemoryStream memoryStream = inputStream as MemoryStream;
                            if (memoryStream == null)
                            {
                                memoryStream = new MemoryStream();
                                inputStream.CopyTo(memoryStream);
                            }
                            justificacion.Archivo = memoryStream.ToArray();
                        }
                        justificacion.AsistenciaID = AsistenciaID;
                        justificacion.Comentario = Comentario;
                        justificacionDAO.insertJustificacion(justificacion, GetApplicationUser(), ref mensaje);
                        if (mensaje == "OK")
                        {
                            Success("Justificación registrada con éxito", "Justificación", true);
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Warning(mensaje, "Justificación", true);
                        }
                    }
                    else
                    {
                        Warning("La extensión del archivo debe ser PDF", "Justificación", true);
                    }
                }

            }
            catch (Exception ex)
            {
                Warning(ex.Message, "Justificación", true);
            }
            return View(justificacion);
        }

        // GET: Justificacion/Descargar
        [AppAuthorize("00027")]
        public ActionResult Descargar(int asistencia)
        {
            string mensaje = string.Empty;
            Justificacion justificacion = justificacionDAO.getJustificacionByAsistencia(asistencia, ref mensaje);
            if (mensaje == "OK")
            {
                return File(justificacion.Archivo, System.Net.Mime.MediaTypeNames.Application.Octet, "Justificacion_" + justificacion.AsistenciaID + ".pdf");
            }
            else
            {
                Warning(mensaje, "Justificación", true);
                return View();
            }
        }


        // GET: JustificacionAtraso/Create
        [AppAuthorize("00036")]
        public ActionResult JustificarAtraso(int asistencia)
        {
            Justificacion justificacion = new Justificacion();
            justificacion.AsistenciaID = asistencia;
            return View(justificacion);
        }

        // POST: JustificacionAtraso/Create
        [HttpPost]
        [AppAuthorize("00036")]
        public ActionResult JustificarAtraso(int AsistenciaID, string Comentario)
        {
            string mensaje = string.Empty;
            Justificacion justificacion = new Justificacion();

            try
            {

                justificacion.AsistenciaID = AsistenciaID;
                justificacion.Comentario = Comentario;
                justificacionDAO.insertJustificacionAtraso(justificacion, GetApplicationUser(), ref mensaje);
                if (mensaje == "OK")
                {
                    Success("Justificación registrada con éxito", "Justificación", true);
                    return RedirectToAction("Index");
                }
                else
                {
                    Warning(mensaje, "Justificación de Atraso", true);
                }

            }
            catch (Exception ex)
            {
                Warning(ex.Message, "Justificación", true);
            }
            return View(justificacion);
        }
    }
}