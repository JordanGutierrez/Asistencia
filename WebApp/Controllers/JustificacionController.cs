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
        public ActionResult Index()
        {
            string mensaje = string.Empty;
            ViewBag.Faltas = asistenciaDAO.getAllAsistenciasByEstado('F', ref mensaje);
            return View();
        }

        // GET: Justificacion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Justificacion/Create
        public ActionResult Justificar(int asistencia)
        {
            Justificacion justificacion = new Justificacion();
            justificacion.AsistenciaID = asistencia;
            return View(justificacion);
        }

        // POST: Justificacion/Create
        [HttpPost]
        public ActionResult Justificar(HttpPostedFileBase Archivo, int AsistenciaID, string Comentario)
        {
            string mensaje = string.Empty;
            Justificacion justificacion = new Justificacion();

            try
            {
                if (Archivo != null)
                {
                    string extension = Path.GetExtension(Archivo.FileName);
                    if(extension == ".pdf")
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
                        if(mensaje == "OK")
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
            catch(Exception ex)
            {
                Warning(ex.Message, "Justificación", true);
            }
            return View(justificacion);
        }
    }
}
