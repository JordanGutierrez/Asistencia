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
    public class VacacionesController : BaseController
    {
        IUsuarioDAO usuarioDAO = new UsuarioDAO();
        IVacacionesDAO vacacionesDAO = new VacacionesDAO();

        // GET: Vacaciones
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vacaciones/Create
        [HttpPost]
        public ActionResult Create(Vacaciones vacaciones)
        {
            string mensaje = string.Empty;

            try
            {
                vacaciones.UsuarioID = int.Parse(Utils.Utils.GetClaim("UsuarioID"));
                vacacionesDAO.insertVacaciones(vacaciones, GetApplicationUser(), ref mensaje);
                if (mensaje == "OK")
                {
                    Success("Vacaciones registradas con éxito", "Vacaciones", true);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            Warning(mensaje, "Vacaciones", true);
            return View(vacaciones);
        }
        // GET: Vacaciones/Aprobar
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
    }
}
