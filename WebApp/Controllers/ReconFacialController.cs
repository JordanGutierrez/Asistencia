using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class ReconFacialController : Controller
    {
        // GET: ReconFacial
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Reconocer()
        {
            var foto = Request.Params["imagen"].ToString();
            var claimsIdentity = ClaimsPrincipal.Current.Identities.FirstOrDefault();
            var aux = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "FaceID");
            string faceid = await Utils.Utils.getFaceID(Utils.Utils.ConvertBase64ToBytes(foto));
            bool esidentico = await Utils.Utils.CompareFaces(faceid, aux.Value);
            if(esidentico)
                return RedirectToAction("Index", "Home");
            else
                return RedirectToAction("Index", "ReconFacial");
        }
    }
}
