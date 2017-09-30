using Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class AppAuthorizeAttribute : AuthorizeAttribute
    {
        string rol;

        public AppAuthorizeAttribute(string rol)
        {
            this.rol = rol;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = filterContext.HttpContext.User as ClaimsPrincipal;
            var claimsIdentity = ClaimsPrincipal.Current.Identities.FirstOrDefault();
            if (claimsIdentity == null)
            {
                HandleUnauthorizedRequest(filterContext);
                return;
            }
            var aux = claimsIdentity.Claims.FirstOrDefault(c => c.Type == AppIdentity.RolesClaimType);
            if (aux == null)
            {
                HandleUnauthorizedRequest(filterContext);
                return;
            }
            var roles = aux.Value;

            var rolesList = roles.Split(',').ToList();
            if (rolesList.Any(r => { return r == rol; }))
            {
                base.OnAuthorization(filterContext);
            }
            else
            {
                HandleUnauthorizedRequest(filterContext);
            }

            base.OnAuthorization(filterContext);
        }
    }
}