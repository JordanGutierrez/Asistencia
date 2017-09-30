using Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Administracion
{
    public interface IRolDAO
    {
        List<Rol> getAllRol(ref string mensaje);

        Rol getRol(int id);
    }
}
