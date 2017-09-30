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
    public class CarreraController : BaseController
    {
        ICarreraDAO carreraDAO = new CarreraDAO();
        IFacultadDAO facultadDAO = new FacultadDAO();

        // GET: Carrera
        [AppAuthorize("00003")]
        public ActionResult Index()
        {
            string mensaje = string.Empty;
            List<Carrera> carreras = carreraDAO.getAllCarrera(ref mensaje);
            if (mensaje != "OK")
                return View();
            else
                return View(carreras);
        }

        // GET: Carrera/Create
        [AppAuthorize("00007")]
        public ActionResult Create()
        {
            string mensaje = string.Empty;
            List<Facultad> facultades = facultadDAO.getAllFacultad(ref mensaje);
            if (mensaje == "OK")
            {
                ViewBag.Facultades = facultades;
                return View();
            }
            else
                return RedirectToAction("Index");
        }

        // POST: Carrera/Create
        [HttpPost]
        [AppAuthorize("00007")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Carrera carrera)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Datos Enviados Incorrectos");
                    return View(carrera);
                }
                string mensaje = string.Empty;
                carreraDAO.insertCarrera(carrera, GetApplicationUser(), ref mensaje);
                if (mensaje == "OK")
                {
                    Success("Carrera registrada con éxito", "Carrera", true);
                    return RedirectToAction("Index");
                }
                else
                    return View(carrera);
            }
            catch (Exception ex)
            {
                return View(carrera);
            }
        }

        // GET: Carrera/Edit/5
        [AppAuthorize("00008")]
        public ActionResult Edit(int id)
        {
            string mensaje = string.Empty;
            Carrera carrera = carreraDAO.getCarrera(id, ref mensaje);
            List<Facultad> facultades = facultadDAO.getAllFacultad(ref mensaje);
            ViewBag.Facultades = facultades;
            return View(carrera);
        }

        // POST: Carrera/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AppAuthorize("00008")]
        public ActionResult Edit(Carrera carrera)
        {
            string mensaje = string.Empty;
            List<Facultad> facultades = facultadDAO.getAllFacultad(ref mensaje);
            ViewBag.Facultades = facultades;
            try
            {
                carreraDAO.updateCarrera(carrera, GetApplicationUser(), ref mensaje);
                if (mensaje == "OK")
                {
                    Success("Carrera registrada exitósamente", "Carrera", true);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            Warning(mensaje, "Carrera", true);
            return View(carrera);
        }
    }
}
