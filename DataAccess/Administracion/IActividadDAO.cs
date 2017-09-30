using Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Administracion
{
    public interface IActividadDAO
    {
        List<Actividad> getAllActividad(ref string mensaje);

        void insertActividad(Actividad actividad, string usuario, ref string mensaje);

    }
}
