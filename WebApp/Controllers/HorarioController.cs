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
        // GET: Horario
        public ActionResult Index()
        {
            String mensaje = string.Empty;
            return View(horarioDAO.getAllHorario(ref mensaje));
        }

        // GET: Horario/Details/5
        public ActionResult Details(int id)
        {
     
            return View();
        }

        // GET: Horario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Horario/Create
        [HttpPost]
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
        public ActionResult Edit(int id)
        {

            string mensaje = string.Empty;
            Horario horario = horarioDAO.getHorario(id, ref mensaje);
            return View(horario);
        }

        // POST: Horario/Edit/5
        [HttpPost]
        public ActionResult Edit(Horario horario)
        {
            string mensaje = string.Empty;
            try
            {
                horarioDAO.updateHorario(horario, GetApplicationUser(), ref mensaje);
                if (mensaje == "OK")
                {
                    Success("Usuario registrado con éxito", "Usuario", true);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return View(horario);
        }

        // GET: Horario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Horario/Delete/5
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
