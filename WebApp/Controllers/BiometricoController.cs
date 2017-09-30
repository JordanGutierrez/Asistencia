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
    public class BiometricoController : BaseController
    {
        IFacultadDAO facultadDAO = new FacultadDAO();
        IBiometricoDAO biometricoDAO = new BiometricoDAO();

        // GET: Biometrico
        public ActionResult Index()
        {
            String mensaje = string.Empty;
            return View(biometricoDAO.getAllBiometrico(ref mensaje));
        }

        // GET: Biometrico/Create
        public ActionResult Create()
        {
            string mensaje = string.Empty;

            ViewBag.Facultades = facultadDAO.getAllFacultad(ref mensaje);

            return View();
        }

        // POST: Biometrico/Create
        [HttpPost]
        public ActionResult Create(Biometrico biometrico)
        {
            string mensaje = string.Empty;

            ViewBag.Facultades = facultadDAO.getAllFacultad(ref mensaje);

            try
            {
                biometricoDAO.insertBiometrico(biometrico, GetApplicationUser(), ref mensaje);
                if (mensaje == "OK")
                {
                    Success("Biométrico registrado con éxito", "Biométrico", true);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            Warning(mensaje, "Biométrico", true);
            return View(biometrico);
        }

        // GET: Biometrico/Edit/5
        public ActionResult Edit(int id)
        {
            string mensaje = string.Empty;
            ViewBag.Facultades = facultadDAO.getAllFacultad(ref mensaje);

            Biometrico biometrico = biometricoDAO.getBiometrico(id, ref mensaje);
            return View(biometrico);
        }

        // POST: Biometrico/Edit/5
        [HttpPost]
        public ActionResult Edit(Biometrico biometrico)
        {
            string mensaje = string.Empty;
            ViewBag.Facultades = facultadDAO.getAllFacultad(ref mensaje);

            try
            {
                biometricoDAO.updateBiometrico(biometrico, GetApplicationUser(), ref mensaje);
                if (mensaje == "OK")
                {
                    Success("Biométrico actualizado con éxito", "Biométrico", true);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            Warning(mensaje, "Biométrico", true);
            return View(biometrico);
        }
    }
}
