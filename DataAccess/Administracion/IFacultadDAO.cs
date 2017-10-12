using Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Administracion
{
    public interface IFacultadDAO
    {
        List<Facultad> getAllFacultad(ref string mensaje);

        Facultad getFacultad(int id, ref string mensaje);

        void insertFacultad(Facultad facultad, string usuario, ref string mensaje);

        void updateFacultad(Facultad facultad, string usuario, ref string mensaje);

        void updateFacultadEstado(int id, char estado, string usuario, ref string mensaje);
    }
}
