using Entidades.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Catalogos
{
    public interface IPasantiaDAO
    {
        List<Pasantia> getAllPasantia(ref string mensaje);
    }
}
