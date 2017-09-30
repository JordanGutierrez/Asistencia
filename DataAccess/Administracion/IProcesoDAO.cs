using Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Administracion
{
    public interface IProcesoDAO
    {
        List<Proceso> getAllProceso(ref string mensaje);

        Proceso getProceso(int id, ref string mensaje);

        void insertProceso(Proceso proceso, string usuario, ref string mensaje);

        void updateProceso(Proceso proceso, string usuario, ref string mensaje);
    }
}
