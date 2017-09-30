using Entidades.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Catalogos
{
    public interface ITipoIdentificacionDAO
    {
        List<TipoIdentificacion> getAllTipoIdentificacion(ref string mensaje);

    }
}
