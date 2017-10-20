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
    public class HorarioController : BaseController
    {
        IHorarioDAO  horarioDAO = new HorarioDAO();

        [AppAuthorize("00009")]
        // GET: Horario
        public ActionResult Index()
        {
            String mensaje = string.Empty;
            return View(horarioDAO.getAllHorario(ref mensaje));
        }

        // GET: Horario/Create
        [AppAuthorize("00010")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Horario/Create
        [HttpPost]
        [AppAuthorize("00010")]
        public ActionResult Create(Horario horario)
        {
            string mensaje = string.Empty;
            try
            {
                
                horarioDAO.insertHorario(horario, GetApplicationUser(), ref mensaje);
                if (mensaje == "OK")
                {
                    Success("Horario registrado con éxito", "Horario", true);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            Warning(mensaje, "Horario", true);
            return View(horario);
        }

        // GET: Horario/Edit/5
        [AppAuthorize("00011")]
        public ActionResult Edit(int id)
        {

            string mensaje = string.Empty;
            Horario horario = horarioDAO.getHorario(id, ref mensaje);
            return View(horario);
        }

        // POST: Horario/Edit/5
        [HttpPost]
        [AppAuthorize("00011")]
        public ActionResult Edit(Horario horario)
        {
            string mensaje = string.Empty;
            try
            {
                horarioDAO.updateHorario(horario, GetApplicationUser(), ref mensaje);
                if (mensaje == "OK")
                {
                    Success("Horario registrado con éxito", "Usuario", true);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            Warning(mensaje, "Usuario", true);
            return View(horario);
        }
    }
}
