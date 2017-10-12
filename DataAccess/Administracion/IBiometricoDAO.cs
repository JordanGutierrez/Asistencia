using Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Administracion
{
    public interface IBiometricoDAO
    {
        List<Biometrico> getAllBiometrico(ref string mensaje);

        Biometrico getBiometrico(int id, ref string mensaje);

        void insertBiometrico(Biometrico biometrico, string usuario, ref string mensaje);

        void updateBiometrico(Biometrico biometrico, string usuario, ref string mensaje);

        void updateBiometricoEstado(int id, char estado, string usuario, ref string mensaje);
    }
}
