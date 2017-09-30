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
    public class InstitucionController : BaseController
    {
        IInstitucionDAO institucionDAO = new InstitucionDAO();

        // GET: Institucion
        public ActionResult Index()
        {
            string mensaje = string.Empty;
            List<Institucion> instituciones = institucionDAO.getAllInstitucion(ref mensaje);
            if (mensaje != "OK")
                return View();
            else
                return View(instituciones);
        }

        // GET: Institucion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Institucion/Create
        [HttpPost]
        public ActionResult Create(Institucion institucion)
        {
            try
            {
                string mensaje = string.Empty;
                institucionDAO.insertInstitucion(institucion, GetApplicationUser(), ref mensaje);
                if (mensaje == "OK")
                    return RedirectToAction("Index");
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Institucion/Edit/5
        public ActionResult Edit(int id)
        {
            Institucion institucion = null;
            string mensaje = string.Empty;
            institucion = institucionDAO.getInstitucion(id, ref mensaje);
            return View(institucion);
        }

        // POST: Institucion/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Institucion institucion)
        {
            try
            {
                string mensaje = string.Empty;
                institucionDAO.updateInstitucion(institucion, GetApplicationUser(), ref mensaje);
                if (mensaje == "OK")
                    return RedirectToAction("Index");
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Instituciones()
        {
            string mensaje = string.Empty;
            List<Institucion> instituciones = institucionDAO.getAllInstitucion(ref mensaje);
            if (mensaje != "OK")
                return View();
            else
                return PartialView("_Instituciones", instituciones.Where(p => p.Estado && p.Estado));
        }

        [HttpPost]
        public JsonResult ConsultarInstitucion(int id)
        {
            try
            {
                string mensaje = string.Empty;
                var institucion = institucionDAO.getInstitucion(id, ref mensaje);
                return Json(new { institucion = institucion, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { mensaje = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
