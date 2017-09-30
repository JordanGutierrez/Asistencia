using Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Administracion
{
    public interface ICarreraDAO
    {
        List<Carrera> getAllCarrera(ref string mensaje);

        List<Carrera> getCarrerabyBiometrico(int biometrico, ref string mensaje);

        Carrera getCarrera(int id, ref string mensaje);

        void insertCarrera(Carrera carrera, string usuario, ref string mensaje);

        void updateCarrera(Carrera carrera, string usuario, ref string mensaje);
    }
}
