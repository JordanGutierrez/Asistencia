using Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Administracion
{
    public interface IPermisoDAO
    {
        List<Permiso> getAllPermiso(ref string mensaje);

        List<Permiso> getAllPermisoByEstado(char estado, ref string mensaje);

        Permiso getPermiso(int id, ref string mensaje);

        void insertPermiso(Permiso permiso, string usuario, ref string mensaje);

        void updatePermiso(Permiso permiso, string usuario, ref string mensaje);

        void updatePermisoEstado(int id, string usuario, ref string mensaje);

    }
}
