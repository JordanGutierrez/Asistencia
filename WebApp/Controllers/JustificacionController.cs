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
    public class JustificacionController : BaseController
    {
        IJustificacionDAO justificacionDAO = new JustificacionDAO();
        
        // GET: Justificacion
        public ActionResult Index()
        {
            string rol = Utils.Utils.GetClaim("RolID");
            string usuarioID = Utils.Utils.GetClaim("UsuarioID");
            string mensaje = string.Empty;

            var justificaciones = justificacionDAO.getAllJustificacion(ref mensaje);

            //if(rol != "1")

            return View(justificaciones);
        }

        // GET: Justificacion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Justificacion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Justificacion/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Justificacion/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Justificacion/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Justificacion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Justificacion/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
