using DataAccess.Administracion;
using Entidades.Administracion;
using SqlDataAccess.Administracion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidades.Catalogos;
using DataAccess.Catalogos;
using SqlDataAccess.Catalogos;

namespace WebApp.Controllers
{
    public class PasanteController : BaseController
    {
        IPersonaDAO personaDAO = new PersonaDAO();
        ICarreraDAO carreraDAO = new CarreraDAO();
        ITipoIdentificacionDAO tipoidentificacionDAO = new TipoIdentificacionDAO();

        public string Descrimage { get; set; }

        // GET: Pasante
        public ActionResult Index()
        {
            string mensaje = string.Empty;
            List<Persona> personas = personaDAO.getAllPersona(ref mensaje);
            if (mensaje != "OK")
                return View();
            else
                return View(personas.Where(p => p.RolID == 1));
        }

        // GET: Pasante/Create
        public ActionResult Create()
        {
            string mensaje = string.Empty;
            List<Carrera> carreras = carreraDAO.getAllCarrera(ref mensaje);
            Persona persona = new Persona();
            persona.RolID = 1;
            ViewBag.TiposIdentificacion = tipoidentificacionDAO.getAllTipoIdentificacion(ref mensaje);
            ViewBag.Carreras = carreras.Where(c => c.Estado == 'A');
            return View(persona);
        }

        // POST: Pasante/Create
        [HttpPost]
        public ActionResult Create(Persona persona)
        {
            string mensaje = string.Empty;
            List<Carrera> carreras = carreraDAO.getAllCarrera(ref mensaje);
            ViewBag.Carreras = carreras.Where(c => c.Estado == 'A');
            ViewBag.TiposIdentificacion = tipoidentificacionDAO.getAllTipoIdentificacion(ref mensaje);
            try
            {
                if (!Utils.Utils.esCedulaValida(persona.NumeroIdentificacion))
                {
                    Warning("Número de identificación inválido", "Pasante", true);
                    return View(persona);
                }
                personaDAO.insertPersona(persona, GetApplicationUser(), ref mensaje);
                if (mensaje == "OK")
                {
                    Success("Pasante registrado con éxito", "Pasante", true);
                    return RedirectToAction("Index");
                }
                else
                    return View(persona);
            }
            catch
            {
                return View();
            }
        }

        // GET: Pasante/Edit/5
        public ActionResult Edit(int id)
        {
            Persona persona = null;
            string mensaje = string.Empty;
            List<Carrera> carreras = carreraDAO.getAllCarrera(ref mensaje);
            persona = personaDAO.getPersona(id, ref mensaje);
            ViewBag.Carreras = carreras.Where(c => c.Estado);
            ViewBag.TiposIdentificacion = tipoidentificacionDAO.getAllTipoIdentificacion(ref mensaje);
            return View(persona);
        }

        // POST: Pasante/Edit/5
        [HttpPost]
        public ActionResult Edit(Persona persona)
        {
            string mensaje = string.Empty;

            List<Carrera> carreras = carreraDAO.getAllCarrera(ref mensaje);
            ViewBag.Carreras = carreras.Where(c => c.Estado);
            ViewBag.TiposIdentificacion = tipoidentificacionDAO.getAllTipoIdentificacion(ref mensaje);

            try
            {
                personaDAO.updatePersona(persona, GetApplicationUser(), ref mensaje);
                if (mensaje == "OK")
                {
                    Success("Pasante Registrado con éxito", "Pasante", true);
                    return RedirectToAction("Index");
                }
                else
                {
                    Warning(mensaje, "Pasante", true);
                    return View(persona);
                }
            }
            catch(Exception ex)
            {
                Danger(ex.Message, "Pasante", true);
                return View(persona);
            }
        }

        [HttpPost]
        public JsonResult Foto(string id)
        {
            var file = Request.Files[0];
            Descrimage = file.FileName;
            string mensaje = string.Empty;
            try
            {
                //if (Request.IsAuthenticated)
                //{
                    //if (GetApplicationUser().CodCandidato == id)
                    //{
                        if (Descrimage.Length <= 128)
                           personaDAO.updateFotoPersona(int.Parse(id), Utils.Utils.ConvertToBytes(file), GetApplicationUser(), ref mensaje);
                    //}

                //}
                return Json(new { Message = "ok"});
            }
            catch (Exception ex)
            {
                return Json(new { Message =  ex.Message });
            }
        }

        public ActionResult Image(string id, string random)
        {
            byte[] cover;
            string mensaje = string.Empty;
            try
            {
                var persona = personaDAO.getPersona(int.Parse(id), ref mensaje);
                cover = persona.Foto;
            }
            catch
            {
                cover = null;
            }

            if (cover == null || cover.Length == 0)
                cover = System.IO.File.ReadAllBytes(Server.MapPath(ConfigurationManager.AppSettings["GenericProfilePic"]));

            return File(cover, "image/png");
        }

        public ActionResult Pasantes()
        {
            string mensaje = string.Empty;
            List<Persona> personas = personaDAO.getAllPersona(ref mensaje);
            if (mensaje != "OK")
                return View();
            else
                return PartialView("_Pasantes", personas.Where(p => p.RolID == 1 && p.Estado));
        }

        public JsonResult ConsultarPasante(int id)
        {
            try
            {
                string mensaje = string.Empty;
                var persona = personaDAO.getPersona(id, ref mensaje);
                return Json(new { persona = persona, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { mensaje = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
