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
            return View(usuarioDAO.getAllUsuario(ref mensaje));
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            string mensaje = string.Empty;

            ViewBag.Roles = rolDAO.getAllRol(ref mensaje);
            ViewBag.Carreras = carreraDAO.getAllCarrera(ref mensaje);
            ViewBag.Horarios = horarioDAO.getAllHorario(ref mensaje);
            ViewBag.biometricos = biometricoDAO.getAllBiometrico(ref mensaje);

            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        public ActionResult Create(Usuario usuario)
        {
            string mensaje = string.Empty;
            ViewBag.Roles = rolDAO.getAllRol(ref mensaje);
            ViewBag.Carreras = carreraDAO.getAllCarrera(ref mensaje);
            ViewBag.Horarios = horarioDAO.getAllHorario(ref mensaje);

            try
            {
                //if (!ModelState.IsValid)
                //{
                //    Warning("Información incorrecta", "Usuario", true);
                //    return View(usuario);
                //}
                if (!Utils.Utils.esCedulaValida(usuario.Cedula))
                {
                    Warning("El número de cédula es inválido", "Usuario", true);
                    return View(usuario);
                }
                string clave = Utils.Utils.GetStringSha256Hash(usuario.Clave);
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
                //if (!ModelState.IsValid)
                //{
                //    Warning("Información incorrecta", "Usuario", true);
                //    return View(usuario);
                //}
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
    }
}
