using DataAccess.Administracion;
using Entidades.Administracion;
using SqlDataAccess.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class FacultadController : BaseController
    {
        IFacultadDAO facultadDAO = new FacultadDAO();

        // GET: Facultad
        [AppAuthorize("00002")]
        public ActionResult Index()
        {
            string mensaje = string.Empty;
            List<Facultad> facultades = facultadDAO.getAllFacultad(ref mensaje);
            if (mensaje != "OK")
                return View();
            else
                return View(facultades);
        }

        // GET: Facultad/Create
        [AppAuthorize("00005")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Facultad/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AppAuthorize("00005")]
        public ActionResult Create(Facultad facultad)
        {
            try
            {
                string mensaje = string.Empty;
                facultadDAO.insertFacultad(facultad, GetApplicationUser(),ref mensaje);
                if (mensaje == "OK")
                {
                    Success("Facultad registrada exitósamente", "Facultad", true);
                    return RedirectToAction("Index");
                }
                else
                {
                    Warning(mensaje, "Facultad", true);
                    return View(facultad);
                }
            }
            catch(Exception ex)
            {
                Danger(ex.Message, "Facultad", true);
                return View(facultad);
            }
        }

        // GET: Facultad/Edit/5
        [AppAuthorize("00006")]
        public ActionResult Edit(int id)
        {
            string mensaje = string.Empty;
            Facultad facultad = facultadDAO.getFacultad(id, ref mensaje);
            return View(facultad);
        }

        // POST: Facultad/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AppAuthorize("00006")]
        public ActionResult Edit(int id, Facultad facultad)
        {
            string mensaje = string.Empty;
            try
            {
                facultadDAO.updateFacultad(facultad, GetApplicationUser(), ref mensaje);
                if (mensaje == "OK")
                {
                    Success("Facultad actualizada con éxito", "Facultad", true);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            Warning(mensaje, "Facultad", true);
            return View(facultad);
        }

        public ActionResult Activar(int id)
        {
            string mensaje = string.Empty;

            facultadDAO.updateFacultadEstado(id, 'A', GetApplicationUser(), ref mensaje);
            if (mensaje == "OK")
                Success("Facultad activada con éxito", "Facultad", true);
            else
                Warning(mensaje, "Facultad", true);
            return RedirectToAction("Index");
        }

        public ActionResult Inactivar(int id)
        {
            string mensaje = string.Empty;

            facultadDAO.updateFacultadEstado(id, 'I', GetApplicationUser(), ref mensaje);
            if (mensaje == "OK")
                Success("Facultad inactivada con éxito", "Facultad", true);
            else
                Warning(mensaje, "Facultad", true);
            return RedirectToAction("Index");
        }
    }
}
