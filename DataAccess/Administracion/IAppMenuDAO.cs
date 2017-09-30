using Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Administracion
{
    public interface IAppMenuDAO
    {
        List<AppMenu> getAllbyRol(int rolid, ref string mensaje);
    }
}
