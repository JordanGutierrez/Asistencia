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
    public class ProcesoDAO : IProcesoDAO
    {
        ConsultasSQL sql = new ConsultasSQL();

        public List<Proceso> getAllProceso(ref string mensaje)
        {
            List<Proceso> procesos = new List<Proceso>();
            sql = new ConsultasSQL();
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_getAllProceso";

            try
            {
                IDataReader reader = sql.EjecutaReader(ref mensaje);
                while (reader.Read())
                {
                    procesos.Add(Proceso.CreateProcesoFromDataRecord(reader));
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return procesos;
        }

        public Proceso getProceso(int id, ref string mensaje)
        {
            Proceso proceso = new Proceso();
            sql = new ConsultasSQL();
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_getProceso";
            sql.Comando.Parameters.AddWithValue("@PersonaID", id);

            try
            {
                IDataReader reader = sql.EjecutaReader(ref mensaje);
                while (reader.Read())
                {
                    proceso = Proceso.CreateProcesoFromDataRecord(reader);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return proceso;
        }

        public void insertProceso(Proceso proceso, string usuario, ref string mensaje)
        {
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_insertProceso";
            sql.Comando.Parameters.AddWithValue("@PasantiaID", proceso.PasantiaID);
            sql.Comando.Parameters.AddWithValue("@PasanteID", proceso.PasanteID);
            sql.Comando.Parameters.AddWithValue("@TutorID", proceso.TutorID);
            sql.Comando.Parameters.AddWithValue("@InstitucionID", proceso.InstitucionID);
            sql.Comando.Parameters.AddWithValue("@Departamento", proceso.Departamento);
            sql.Comando.Parameters.AddWithValue("@Supervisor", proceso.Supervisor);
            sql.Comando.Parameters.AddWithValue("@CorreoSupervisor", proceso.CorreoSupervisor);
            sql.Comando.Parameters.AddWithValue("@CargoSupervisor", proceso.CargoSupervisor);
            sql.Comando.Parameters.AddWithValue("@FechaInicioPasantia", proceso.FechaInicioPasantias);
            sql.Comando.Parameters.AddWithValue("@FechaFinPasantias", proceso.FechaFinPasantias);
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

        public void updateProceso(Proceso proceso, string usuario, ref string mensaje)
        {
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_updateProceso";
            sql.Comando.Parameters.AddWithValue("@ProcesoID", proceso.ProcesoID);
            sql.Comando.Parameters.AddWithValue("@PasantiaID", proceso.PasantiaID);
            sql.Comando.Parameters.AddWithValue("@PasanteID", proceso.PasanteID);
            sql.Comando.Parameters.AddWithValue("@TutorID", proceso.TutorID);
            sql.Comando.Parameters.AddWithValue("@InstitucionID", proceso.InstitucionID);
            sql.Comando.Parameters.AddWithValue("@Departamento", proceso.Departamento);
            sql.Comando.Parameters.AddWithValue("@Supervisor", proceso.Supervisor);
            sql.Comando.Parameters.AddWithValue("@CorreoSupervisor", proceso.CorreoSupervisor);
            sql.Comando.Parameters.AddWithValue("@CargoSupervisor", proceso.CargoSupervisor);
            sql.Comando.Parameters.AddWithValue("@FechaInicioPasantia", proceso.FechaInicioPasantias);
            sql.Comando.Parameters.AddWithValue("@FechaFinPasantias", proceso.FechaFinPasantias);
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
    }
}