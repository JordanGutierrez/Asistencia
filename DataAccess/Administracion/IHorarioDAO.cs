using Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Administracion
{
    public interface IHorarioDAO
    {
    
        List<Horario> getAllHorario(ref string mensaje);

        Horario getHorario(int id, ref string mensaje);


        void insertHorario(Horario horario, string user, ref string mensaje);

        void updateHorario(Horario horario, string user, ref string mensaje);

    }
}


