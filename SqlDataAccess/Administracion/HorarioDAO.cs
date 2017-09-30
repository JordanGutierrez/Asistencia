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
    public class HorarioDAO : IHorarioDAO
    {
        ConsultasSQL sql = new ConsultasSQL();

        
        public List<Horario> getAllHorario(ref string mensaje)
        {
            List<Horario> horarios = new List<Horario>();
            sql = new ConsultasSQL();
            //sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "SELECT * FROM tbHorario";

            try
            {
                sql.AbrirConexion();
                IDataReader reader = sql.EjecutaReader(ref mensaje);
                while (reader.Read())
                {
                    horarios.Add(Horario.CreateHorarioFromDataRecord(reader));
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                sql.CerrarConexion();
            }

            return horarios ;
        }

     
        public Horario getHorario(int id, ref string mensaje)
        {
            Horario horario = new Horario();
            sql = new ConsultasSQL();
            //sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "SELECT * FROM tbHorario WHERE HorarioID = " + id.ToString();

            try
            {
                IDataReader reader = sql.EjecutaReader(ref mensaje);
                while (reader.Read())
                {
                    horario = Horario.CreateHorarioFromDataRecord(reader);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return horario;
        }


        public void insertHorario(Horario horario, string user, ref string mensaje)
        {
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_insertHorario";
            sql.Comando.Parameters.AddWithValue("P_Descripcion", horario.Descripcion);
            sql.Comando.Parameters.AddWithValue("P_FechaEntrada", horario.HoraEntrada);
            sql.Comando.Parameters.AddWithValue("P_FechaSalida", horario.HoraSalida);
            sql.Comando.Parameters.AddWithValue("P_UsuarioCreacion", user);
            try
            {
                sql.EjecutaQuery(ref mensaje);
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
        }

        public void updateHorario(Horario horario, string user, ref string mensaje)
        {
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_updateHorario";
            sql.Comando.Parameters.AddWithValue("P_HorarioID", horario.HorarioID);
            sql.Comando.Parameters.AddWithValue("P_Descripcion", horario.Descripcion);
            sql.Comando.Parameters.AddWithValue("P_FechaEntrada", horario.HoraEntrada);
            sql.Comando.Parameters.AddWithValue("P_FechaSalida", horario.HoraSalida);
            sql.Comando.Parameters.AddWithValue("P_UsuarioCreacion", user);
            try
            {
                sql.EjecutaQuery(ref mensaje);
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
        }
    }
}