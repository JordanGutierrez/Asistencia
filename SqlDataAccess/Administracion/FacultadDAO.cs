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
    public class FacultadDAO : IFacultadDAO
    {
        ConsultasSQL sql = new ConsultasSQL();

        public List<Facultad> getAllFacultad(ref string mensaje)
        {
            List<Facultad> facultades = new List<Facultad>();
            sql = new ConsultasSQL();
            sql.Comando.CommandText = "SELECT * FROM tbFacultad";

            try
            {
                sql.AbrirConexion();
                IDataReader reader = sql.EjecutaReader(ref mensaje);
                while (reader.Read())
                {
                    facultades.Add(Facultad.CreateFacultadFromDataRecord(reader));
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

            return facultades;
        }

        public Facultad getFacultad(int id, ref string mensaje)
        {
            Facultad facultad = new Facultad();
            sql = new ConsultasSQL();
            sql.Comando.CommandText = "SELECT * FROM tbFacultad WHERE FacultadID = " + id;

            try
            {
                sql.AbrirConexion();
                IDataReader reader = sql.EjecutaReader(ref mensaje);
                while (reader.Read())
                {
                    facultad = Facultad.CreateFacultadFromDataRecord(reader);
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

            return facultad;
        }

        public void insertFacultad(Facultad facultad, string usuario, ref string mensaje)
        {
            sql = new ConsultasSQL();
            sql.Comando.CommandText = "SELECT * FROM tbFacultad WHERE Codigo = " + facultad.Codigo;
            DataTable dt = sql.EjecutaDataTable(ref mensaje);
            if (dt.Rows.Count < 1)
            {
                sql = new ConsultasSQL();
                sql.Comando.CommandType = CommandType.StoredProcedure;
                sql.Comando.CommandText = "pa_insertFacultad";
                sql.Comando.Parameters.AddWithValue("P_Codigo", facultad.Codigo);
                sql.Comando.Parameters.AddWithValue("P_Descripcion", facultad.Descripcion);
                sql.Comando.Parameters.AddWithValue("P_User", usuario);

                try
                {
                    sql.AbrirConexion();
                    sql.EjecutaQuery(ref mensaje);
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
                finally
                {
                    sql.CerrarConexion();
                }
            }
            else
            {
                mensaje = "El código de la facultad ya se encuentra registrado";
            }
        }

        public void updateFacultad(Facultad facultad, string usuario, ref string mensaje)
        {
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_updateFacultad";
            sql.Comando.Parameters.AddWithValue("P_FacultadID", facultad.FacultadID);
            sql.Comando.Parameters.AddWithValue("P_Codigo", facultad.Codigo);
            sql.Comando.Parameters.AddWithValue("P_Descripcion", facultad.Descripcion);
            sql.Comando.Parameters.AddWithValue("P_User", usuario);

            try
            {
                sql.AbrirConexion();
                sql.EjecutaQuery(ref mensaje);
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                sql.CerrarConexion();
            }
        }
    }
}
