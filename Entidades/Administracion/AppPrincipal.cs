using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Administracion
{
    public class AppPrincipal : ClaimsPrincipal
    {
        public AppPrincipal(AppIdentity identity)
            : base(identity)
        {

        }

        public AppPrincipal(ClaimsPrincipal claimsPrincipal)
            : base(claimsPrincipal)
        {

        }
    }
}
