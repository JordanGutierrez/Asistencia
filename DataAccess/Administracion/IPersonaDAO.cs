using Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Administracion
{
    public interface IPersonaDAO
    {
        List<Persona> getAllPersona(ref string mensaje);

        Persona getPersona(int id, ref string mensaje);

        Persona getPersonabyUser(int user, ref string mensaje);

        void insertPersona(Persona persona, string usuario, ref string mensaje);

        void updatePersona(Persona persona, string usuario, ref string mensaje);

        void updateFotoPersona(int personaID, byte[] foto, string usuario, ref string mensaje);
    }
}
