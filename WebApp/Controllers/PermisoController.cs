﻿using DataAccess.Administracion;
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
    public class PermisoController : BaseController
    {
        IUsuarioDAO usuarioDAO = new UsuarioDAO();
        IPermisoDAO permisoDAO = new PermisoDAO();

        // GET: Permiso
        [AppAuthorize("00018")]
        public ActionResult Index()
        {
            String mensaje = string.Empty;
            ViewBag.RolID = Utils.Utils.GetClaim("RolID");
            int facultad = int.Parse(Utils.Utils.GetClaim("FacultadID"));
            int usuarioID = int.Parse(Utils.Utils.GetClaim("UsuarioID"));
            List<Permiso> permisos = permisoDAO.getAllPermiso(ref mensaje);
            if(ViewBag.RolID == "3")
                return View(permisos.Where(y => y.FacultadID == facultad));
            else
                return View(permisos.Where(x => x.UsuarioID == usuarioID));
        }

        // GET: Permiso/Create
        [AppAuthorize("00019")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Permiso/Create
        [HttpPost]
        [AppAuthorize("00019")]
        public ActionResult Create(HttpPostedFileBase Archivo, DateTime Fecha, string Motivo)
        {
            string mensaje = string.Empty;
            Permiso permiso = new Permiso();
            permiso.Fecha = Fecha;
            permiso.Motivo = Motivo;
            try
            {

                if (Archivo != null)
                {
                    if (Archivo.ContentLength <= 4194304)
                    {
                        string extension = Path.GetExtension(Archivo.FileName);
                        if (extension.ToUpper() == ".PDF")
                        {
                            if (Fecha.Date > DateTime.Now.Date)
                            {
                                using (Stream inputStream = Archivo.InputStream)
                                {
                                    MemoryStream memoryStream = inputStream as MemoryStream;
                                    if (memoryStream == null)
                                    {
                                        memoryStream = new MemoryStream();
                                        inputStream.CopyTo(memoryStream);
                                    }
                                    permiso.Archivo = memoryStream.ToArray();
                                }
                                permiso.UsuarioID = int.Parse(Utils.Utils.GetClaim("UsuarioID"));
                                permisoDAO.insertPermiso(permiso, GetApplicationUser(), ref mensaje);
                                if (mensaje == "OK")
                                {
                                    Success("Permiso registrado con éxito", "Permiso", true);
                                    return RedirectToAction("Index");
                                }
                            }
                            else
                                mensaje = "La fecha del permiso debe ser mayor a la fecha actual";
                        }
                        else
                            mensaje = "La extensión del archivo deber ser PDF";
                    }
                    else
                        mensaje = "El archivo no debe ser superior a 4 MB";
                }
                else
                    mensaje = "El archivo es requerido";
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            Warning(mensaje, "Permiso", true);
            return View(permiso);
        }

        // GET: Permiso/Aprobar
        [AppAuthorize("00024")]
        public ActionResult Aprobar(int id)
        {
            string mensaje = string.Empty;
            try
            {
                permisoDAO.updatePermisoEstado(id, GetApplicationUser(), 'A',"Permiso Aprobado",ref mensaje);
                if (mensaje == "OK")
                {
                    Success("Permiso aprobado con éxito", "Permiso", true);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            Warning(mensaje, "Permiso", true);
            return RedirectToAction("Index");
        }

        // GET: Permiso/Rechazar
        [AppAuthorize("00035")]
        public ActionResult Rechazar(int id)
        {
            Permiso permiso = new Permiso();
            permiso.PermisoID = id;
            return View(permiso);
        }

        // POST: Permiso/Rechazar
        [HttpPost]
        [AppAuthorize("00035")]
        public ActionResult Rechazar(int id, string Comentario)
        {
            string mensaje = string.Empty;
            try
            {
                permisoDAO.updatePermisoEstado(id, GetApplicationUser(), 'R', Comentario, ref mensaje);
                if (mensaje == "OK")
                {
                    Success("Permiso rechazado con éxito", "Permiso", true);
                    return RedirectToAction("Index");
                }         
                else
                    mensaje = "El comentario es requerido";
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            Warning(mensaje, "Permiso", true);

            return RedirectToAction("Index");
        }

        // GET: Permiso/Descargar
        [AppAuthorize("00025")]
        public ActionResult Descargar(int id)
        {
            string mensaje = string.Empty;
            try
            {
                Permiso permiso = permisoDAO.getPermiso(id, ref mensaje);
                if (mensaje == "OK")
                {
                    return File(permiso.Archivo, System.Net.Mime.MediaTypeNames.Application.Octet, "Permiso_" + permiso.PermisoID + ".pdf");
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            Warning(mensaje, "Permiso", true);
            return RedirectToAction("Index");
        }
    }
}