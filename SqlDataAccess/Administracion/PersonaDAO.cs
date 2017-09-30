using DataAccess.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Administracion;
using SqlDataAccess.Utils;
using System.Data;

namespace SqlDataAccess.Administracion
{
    public class PersonaDAO : IPersonaDAO
    {
        ConsultasSQL sql = new ConsultasSQL();

        public List<Persona> getAllPersona(ref string mensaje)
        {
            List<Persona> personas = new List<Persona>();
            sql = new ConsultasSQL();
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_getAllPersona";

            try
            {
                IDataReader reader = sql.EjecutaReader(ref mensaje);
                while (reader.Read())
                {
                    personas.Add(Persona.CreatePersonaFromDataRecord(reader));
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return personas;
        }

        public Persona getPersona(int id, ref string mensaje)
        {
            Persona persona = new Persona();
            sql = new ConsultasSQL();
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_getPersona";
            sql.Comando.Parameters.AddWithValue("@PersonaID", id);

            try
            {
                IDataReader reader = sql.EjecutaReader(ref mensaje);
                while (reader.Read())
                {
                    persona = Persona.CreatePersonaFromDataRecord(reader);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return persona;
        }

        public void insertPersona(Persona persona, string usuario, ref string mensaje)
        {
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_insertPersona";
            sql.Comando.Parameters.AddWithValue("@RolID", persona.RolID);
            sql.Comando.Parameters.AddWithValue("@TipoIdentificacionID", persona.TipoIdentificacionID);
            sql.Comando.Parameters.AddWithValue("@NumeroIdentificacion", persona.NumeroIdentificacion);
            sql.Comando.Parameters.AddWithValue("@PrimerNombre", persona.PrimerNombre);
            sql.Comando.Parameters.AddWithValue("@SegundoNombre", persona.SegundoNombre);
            sql.Comando.Parameters.AddWithValue("@ApellidoPaterno", persona.ApellidoPaterno);
            sql.Comando.Parameters.AddWithValue("@ApellidoMaterno", persona.ApellidoMaterno);
            sql.Comando.Parameters.AddWithValue("@FechaNacimiento", persona.FechaNacimineto);
            sql.Comando.Parameters.AddWithValue("@Celular", persona.Celular);
            sql.Comando.Parameters.AddWithValue("@CorreoInstitucional", persona.CorreoInstitucional);
            sql.Comando.Parameters.AddWithValue("@CorreoPersonal", persona.CorreoPersonal);
            sql.Comando.Parameters.AddWithValue("@CarreraID", persona.CarreraID);
            sql.Comando.Parameters.AddWithValue("@Usuario", usuario);

            try
            {
                sql.EjecutaQuery(ref mensaje);
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
        }

        public void updatePersona(Persona persona, string usuario, ref string mensaje)
        {
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_updatePersona";
            sql.Comando.Parameters.AddWithValue("@PersonaID", persona.PersonaID);
            sql.Comando.Parameters.AddWithValue("@RolID", persona.RolID);
            sql.Comando.Parameters.AddWithValue("@TipoIdentificacionID", persona.TipoIdentificacionID);
            sql.Comando.Parameters.AddWithValue("@NumeroIdentificacion", persona.NumeroIdentificacion);
            sql.Comando.Parameters.AddWithValue("@PrimerNombre", persona.PrimerNombre);
            sql.Comando.Parameters.AddWithValue("@SegundoNombre", persona.SegundoNombre);
            sql.Comando.Parameters.AddWithValue("@ApellidoPaterno", persona.ApellidoPaterno);
            sql.Comando.Parameters.AddWithValue("@ApellidoMaterno", persona.ApellidoMaterno);
            sql.Comando.Parameters.AddWithValue("@FechaNacimiento", persona.FechaNacimineto);
            sql.Comando.Parameters.AddWithValue("@Celular", persona.Celular);
            sql.Comando.Parameters.AddWithValue("@CorreoInstitucional", persona.CorreoInstitucional);
            sql.Comando.Parameters.AddWithValue("@CorreoPersonal", persona.CorreoPersonal);
            sql.Comando.Parameters.AddWithValue("@CarreraID", persona.CarreraID);
            sql.Comando.Parameters.AddWithValue("@Estado", persona.Estado);
            sql.Comando.Parameters.AddWithValue("@Usuario", usuario);

            try
            {
                sql.EjecutaQuery(ref mensaje);
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
        }

        public void updateFotoPersona(int personaID, byte[] foto, string usuario, ref string mensaje)
        {
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_updateFotoPersona";
            sql.Comando.Parameters.AddWithValue("@PersonaID", personaID);
            sql.Comando.Parameters.AddWithValue("@Foto", foto);
            sql.Comando.Parameters.AddWithValue("@Usuario", usuario);

            try
            {
                sql.EjecutaQuery(ref mensaje);
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
        }

        public Persona getPersonabyUser(int user, ref string mensaje)
        {
            Persona persona = new Persona();
            sql = new ConsultasSQL();
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_getPersonabyUser";
            sql.Comando.Parameters.AddWithValue("@UsuarioID", user);

            try
            {
                IDataReader reader = sql.EjecutaReader(ref mensaje);
                while (reader.Read())
                {
                    persona = Persona.CreatePersonaFromDataRecord(reader);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return persona;
        }
    }
}
