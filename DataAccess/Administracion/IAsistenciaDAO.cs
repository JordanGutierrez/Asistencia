using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Administracion
{
    public interface IAsistenciaDAO
    {
        DataTable getAllAsistencia(ref string mensaje);
    }
}
