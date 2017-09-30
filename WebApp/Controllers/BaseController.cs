using DataAccess.Administracion;
using Entidades.Administracion;
using Entidades.Utils;
using SqlDataAccess.Administracion;
using SqlDataAccess.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class BaseController: Controller
    {
        IAppMenuDAO appmenuDAO = new AppMenuDAO();

        public string GetApplicationUser()
        {
            return User.Identity.Name;
        }

        public string GetApplicationUserID()
        {
            return User.Identity.Name;
        }

        public ActionResult Menu()
        {
            if (Request.IsAuthenticated)
            {
                var claimsIdentity = ClaimsPrincipal.Current.Identities.FirstOrDefault();
                var aux = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "RolID");
                string mensaje = string.Empty;
                List<AppMenu> menus = appmenuDAO.getAllbyRol(int.Parse(aux.Value), ref mensaje);
                return PartialView("_Menu", menus);
            }
            return null;
        }

        public void Success(string message, string titulo, bool dismissable = false)
        {
            AddAlert(AlertStyles.Success, message, titulo, dismissable);
        }

        public void Information(string message, string titulo, bool dismissable = false)
        {
            AddAlert(AlertStyles.Information, message, titulo, dismissable);
        }

        public void Warning(string message, string titulo, bool dismissable = false)
        {
            AddAlert(AlertStyles.Warning, message, titulo, dismissable);
        }

        public void Danger(string message, string titulo, bool dismissable = false)
        {
            AddAlert(AlertStyles.Danger, message, titulo, dismissable);
        }

        private void AddAlert(string alertStyle, string message, string titulo, bool dismissable)
        {
            var alerts = TempData.ContainsKey(Alert.TempDataKey)
                ? (List<Alert>)TempData[Alert.TempDataKey]
                : new List<Alert>();

            alerts.Add(new Alert
            {
                AlertStyle = alertStyle,
                Message = message,
                Dismissable = dismissable,
                Titulo = titulo
            });

            TempData[Alert.TempDataKey] = alerts;
        }

    }
}