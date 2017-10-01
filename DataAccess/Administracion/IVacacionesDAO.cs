using Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Administracion
{
    public interface IVacacionesDAO
    {
        List<Vacaciones> getAllVacaciones(ref string mensaje);

        List<Vacaciones> getAllVacacionesByEstado(char estado, ref string mensaje);

        Vacaciones getVacaciones(int id, ref string mensaje);

        void insertVacaciones(Vacaciones vacaciones, string usuario, ref string mensaje);

        void updateVacaciones(Vacaciones vacaciones, string usuario, ref string mensaje);

        void updateVacacionesEstado(int id, string usuario, ref string mensaje);

    }
}
