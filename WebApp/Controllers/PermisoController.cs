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
    public class PermisoController : BaseController
    {
        IUsuarioDAO usuarioDAO = new UsuarioDAO();
        IPermisoDAO permisoDAO = new PermisoDAO();

        // GET: Permiso
        public ActionResult Index()
        {
            String mensaje = string.Empty;
            ViewBag.RolID = Utils.Utils.GetClaim("RolID");
            int usuarioID = int.Parse(Utils.Utils.GetClaim("UsuarioID"));
            List<Permiso> permisos = permisoDAO.getAllPermiso(ref mensaje);
            if(ViewBag.RolID == "3")
                return View(permisos);
            else
                return View(permisos.Where(x => x.UsuarioID == usuarioID));
        }

        // GET: Permiso/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        public ActionResult Create(Permiso permiso)
        {
            string mensaje = string.Empty;

            try
            {
                permiso.UsuarioID = int.Parse(Utils.Utils.GetClaim("UsuarioID"));
                permisoDAO.insertPermiso(permiso, GetApplicationUser(), ref mensaje);
                if (mensaje == "OK")
                {
                    Success("Permiso registrado con éxito", "Permiso", true);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            Warning(mensaje, "Permiso", true);
            return View(permiso);
        }

        // GET: Permiso/Aprobar
        public ActionResult Aprobar(int id)
        {
            string mensaje = string.Empty;
            try
            {
                permisoDAO.updatePermisoEstado(id, GetApplicationUser(), ref mensaje);
                if (mensaje == "OK")
                {
                    Success("Permiso aprobado con éxito", "Permiso", true);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            Warning(mensaje, "Permiso", true);
            return RedirectToAction("Index");
        }
    }
}