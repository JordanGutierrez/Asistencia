using DataAccess.Administracion;
using DataAccess.Catalogos;
using Entidades.Administracion;
using SqlDataAccess.Administracion;
using SqlDataAccess.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class ProcesoController : BaseController
    {
        IProcesoDAO procesoDAO = new ProcesoDAO();
        IPasantiaDAO pasantiaDAO = new PasantiaDAO();

        // GET: Proceso
        public ActionResult Index()
        {
            string mensaje = string.Empty;
            List<Proceso> procesos = procesoDAO.getAllProceso(ref mensaje);
            if (mensaje != "OK")
                return View();
            else
                return View(procesos);
        }

        // GET: Proceso/Create
        public ActionResult Create()
        {
            string mensaje = string.Empty;
            ViewBag.Pasantias = pasantiaDAO.getAllPasantia(ref mensaje);
            return View();
        }

        // POST: Proceso/Create
        [HttpPost]
        public ActionResult Create(Proceso proceso)
        {
            string mensaje = string.Empty;
            ViewBag.Pasantias = pasantiaDAO.getAllPasantia(ref mensaje);

            try
            {
                mensaje = string.Empty;
                procesoDAO.insertProceso(proceso, GetApplicationUser(), ref mensaje);
                if (mensaje == "OK")
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError("", mensaje);
                    return View(proceso);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(proceso);
            }
        }

        // GET: Proceso/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Proceso/Edit/5
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
    }
}
