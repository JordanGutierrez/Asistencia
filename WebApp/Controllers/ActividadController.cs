using DataAccess.Administracion;
using Entidades.Administracion;
using SqlDataAccess.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class ActividadController : BaseController
    {
        IActividadDAO actividadDAO = new ActividadDAO();
        IProcesoDAO ProcesoDAO = new ProcesoDAO();
        // GET: Actividad
        public ActionResult Index()
        {
            string mensaje = string.Empty;
            List<Actividad> actividades = actividadDAO.getAllActividad(ref mensaje);
            if (mensaje != "OK")
                return View();
            else
                return View(actividades);
        }

        // GET: Actividad/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Actividad/Create
        [HttpPost]
        public async Task<ActionResult> Create(Actividad actividad)
        {
            try
            {
                var foto = Request.Params["imagen"].ToString();
                var claimsIdentity = ClaimsPrincipal.Current.Identities.FirstOrDefault();
                var aux = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "FaceID");
                //var pasante = int.Parse(claimsIdentity.Claims.FirstOrDefault(c => c.Type == "PersonaID").Value);
                string faceid = await Utils.Utils.getFaceID(Utils.Utils.ConvertBase64ToBytes(foto));
                bool esidentico = await Utils.Utils.CompareFaces(faceid, aux.Value);
                if (esidentico)
                {
                    string mensaje = string.Empty;
                    actividad.ProcesoID = 2;
                    //actividad.ProcesoID = ProcesoDAO.getAllProceso(ref mensaje).Where(x => x.PasanteID == pasante && x.Estado == 'A').FirstOrDefault().ProcesoID;
                    actividadDAO.insertActividad(actividad, GetApplicationUser(), ref mensaje);
                    if (mensaje == "OK")
                    {
                        Success("Actividad registrada con éxito", "Actividad", true);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Warning(mensaje, "Actividad", true);
                        return View(actividad);
                    }
                }
                else
                {
                    Warning("Sus rasgos faciales no coinciden con los del registro", "Actividad", true);
                    return View(actividad);
                }
            }
            catch (Exception ex)
            {
                Danger(ex.Message, "Actividad", true);
                return View(actividad);
            }
        }
    }
}
