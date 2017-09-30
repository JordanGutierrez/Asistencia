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
        DataTable getAllAsistenciasByEstado(char estado, ref string mensaje);
    }
}
