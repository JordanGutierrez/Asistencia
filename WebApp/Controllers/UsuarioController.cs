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
    public class UsuarioController : BaseController
    {
        IUsuarioDAO usuarioDAO = new UsuarioDAO();
        IRolDAO rolDAO = new RolDAO();
        ICarreraDAO carreraDAO = new CarreraDAO();
        IHorarioDAO horarioDAO = new HorarioDAO();
        IBiometricoDAO biometricoDAO = new BiometricoDAO();

        // GET: Usuario
        public ActionResult Index()
        {
            String mensaje = string.Empty;
            ViewBag.RolID = Utils.Utils.GetClaim("RolID");
            if(ViewBag.RolID == "1")
                return View(usuarioDAO.getAllUsuario(ref mensaje).Where(x => x.RolID == 3));
            else
                return View(usuarioDAO.getAllUsuario(ref mensaje).Where(x => x.RolID == 2));
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            string mensaje = string.Empty;
            Usuario usuario = new Usuario();
            ViewBag.Carreras = carreraDAO.getAllCarrera(ref mensaje);
            ViewBag.Horarios = horarioDAO.getAllHorario(ref mensaje);
            ViewBag.biometricos = biometricoDAO.getAllBiometrico(ref mensaje);

            string rol = Utils.Utils.GetClaim("RolID");

            if (rol == "1")
                usuario.RolID = 3;
            else
                usuario.RolID = 2;

            return View(usuario);
        }

        // POST: Usuario/Create
        [HttpPost]
        public ActionResult Create(Usuario usuario)
        {
            string mensaje = string.Empty;
            ViewBag.Carreras = carreraDAO.getAllCarrera(ref mensaje);
            ViewBag.Horarios = horarioDAO.getAllHorario(ref mensaje);
            ViewBag.biometricos = biometricoDAO.getAllBiometrico(ref mensaje);

            try
            {
                if (!Utils.Utils.esCedulaValida(usuario.Cedula))
                {
                    Warning("El número de cédula es inválido", "Usuario", true);
                    return View(usuario);
                }
                string clave = Utils.Utils.GetStringSha256Hash(usuario.Cedula);
                usuarioDAO.insertUsuario(usuario, GetApplicationUser(), clave, ref mensaje);
                if(mensaje == "OK")
                {
                    Success("Usuario registrado con éxito", "Usuario", true);
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                mensaje = ex.Message;
            }
            Warning(mensaje, "Usuario", true);
            return View(usuario);
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            string mensaje = string.Empty;
            ViewBag.Roles = rolDAO.getAllRol(ref mensaje);
            ViewBag.Carreras = carreraDAO.getAllCarrera(ref mensaje);
            ViewBag.Horarios = horarioDAO.getAllHorario(ref mensaje);
            ViewBag.biometricos = biometricoDAO.getAllBiometrico(ref mensaje);

            Usuario usuario = usuarioDAO.getUsuario(id, ref mensaje);
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit(Usuario usuario)
        {
            string mensaje = string.Empty;
            ViewBag.Roles = rolDAO.getAllRol(ref mensaje);
            ViewBag.Carreras = carreraDAO.getAllCarrera(ref mensaje);
            ViewBag.Horarios = horarioDAO.getAllHorario(ref mensaje);
            ViewBag.biometricos = biometricoDAO.getAllBiometrico(ref mensaje);

            try
            {
                if (!Utils.Utils.esCedulaValida(usuario.Cedula))
                {
                    Warning("El número de cédula es inválido", "Usuario", true);
                    return View(usuario);
                }
                usuarioDAO.updateUsuario(usuario, GetApplicationUser(), ref mensaje);
                if (mensaje == "OK")
                {
                    Success("Usuario actualizado con éxito", "Usuario", true);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            Warning(mensaje, "Usuario", true);
            return View(usuario);
        }

        public ActionResult Restart(int id)
        {
            string mensaje = string.Empty;

            usuarioDAO.restartUsuario(id, ref mensaje);
            if (mensaje == "OK")
                Success("Contraseña reseteada con éxito", "Usuario", true);
            else
                Warning(mensaje, "Usuario", true);
            return RedirectToAction("Index");
        }
    }
}
