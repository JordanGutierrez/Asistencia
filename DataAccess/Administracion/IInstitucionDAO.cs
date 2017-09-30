using Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Administracion
{
    public interface IInstitucionDAO
    {
        List<Institucion> getAllInstitucion(ref string mensaje);

        Institucion getInstitucion(int id, ref string mensaje);

        void insertInstitucion(Institucion institucion, string usuario, ref string mensaje);

        void updateInstitucion(Institucion institucion, string usuario, ref string mensaje);
    }
}
