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
    public class CarreraDAO : ICarreraDAO
    {
        ConsultasSQL sql = new ConsultasSQL();

        public List<Carrera> getAllCarrera(ref string mensaje)
        {
            List<Carrera> carreras = new List<Carrera>();
            sql = new ConsultasSQL();
            sql.Comando.CommandText = "SELECT * FROM Tbcarrera";

            try
            {
                IDataReader reader = sql.EjecutaReader(ref mensaje);
                while (reader.Read())
                {
                    carreras.Add(Carrera.CreateCarreraFromDataRecord(reader));
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return carreras;
        }

        public Carrera getCarrera(int id, ref string mensaje)
        {
            Carrera carrera = new Carrera();
            sql = new ConsultasSQL();
            sql.Comando.CommandText = "SELECT * FROM tbCarrera WHERE CarreraID = " + id;

            try
            {
                IDataReader reader = sql.EjecutaReader(ref mensaje);
                while (reader.Read())
                {
                    carrera = Carrera.CreateCarreraFromDataRecord(reader);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return carrera;
        }

        public List<Carrera> getCarrerabyBiometrico(int biometrico, ref string mensaje)
        {
            List<Carrera> carreras = new List<Carrera>();
            sql = new ConsultasSQL();
            sql.Comando.CommandText = "SELECT CAR.CarreraID"
		                            + ",CAR.Descripcion"
                                    + ",CAR.FacultadID"
                                    + ",CAR.Codigo"
                                    + ",CAR.Estado"
                                    + " FROM    tbbiometrico AS BIO"
                                    + " INNER   JOIN tbfacultad     AS FAC"
                                    + " ON FAC.FacultadID = BIO.FacultadID"
                                    + " INNER JOIN tbcarrera AS CAR"
                                    + " ON      FAC.FacultadID = CAR.FacultadID"
                                    + " WHERE BIO.BiometricoID = " + biometrico;

            try
            {
                IDataReader reader = sql.EjecutaReader(ref mensaje);
                while (reader.Read())
                {
                    carreras.Add(Carrera.CreateCarreraFromDataRecord(reader));
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return carreras;
        }

        public void insertCarrera(Carrera carrera, string usuario, ref string mensaje)
        {
            sql = new ConsultasSQL();
            sql.Comando.CommandText = "SELECT * FROM tbCarrera WHERE Codigo = " + carrera.Codigo;
            DataTable dt = sql.EjecutaDataTable(ref mensaje);
            if (dt.Rows.Count < 1)
            {
                sql = new ConsultasSQL();
                sql.Comando.CommandType = CommandType.StoredProcedure;
                sql.Comando.CommandText = "pa_insertCarrera";
                sql.Comando.Parameters.AddWithValue("P_Codigo", carrera.Codigo);
                sql.Comando.Parameters.AddWithValue("P_Descripcion", carrera.Descripcion);
                sql.Comando.Parameters.AddWithValue("P_FacultadID", carrera.FacultadID);
                sql.Comando.Parameters.AddWithValue("P_User", usuario);

                try
                {
                    sql.EjecutaQuery(ref mensaje);
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
            }
            else
            {
                mensaje = "El código de la carrera ya se encuentra registrado";
            }

        }

        public void updateCarrera(Carrera carrera, string usuario, ref string mensaje)
        {
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_updateCarrera";
            sql.Comando.Parameters.AddWithValue("P_CarreraID", carrera.CarreraID);
            sql.Comando.Parameters.AddWithValue("P_Codigo", carrera.Codigo);
            sql.Comando.Parameters.AddWithValue("P_Descripcion", carrera.Descripcion);
            sql.Comando.Parameters.AddWithValue("P_FacultadID", carrera.FacultadID);
            sql.Comando.Parameters.AddWithValue("P_User", usuario);

            try
            {
                sql.EjecutaQuery(ref mensaje);
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
        }

        public void updateCarreraEstado(int id, char estado, string usuario, ref string mensaje)
        {
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_updateCarreraEstado";
            sql.Comando.Parameters.AddWithValue("P_CarreraID", id);
            sql.Comando.Parameters.AddWithValue("P_Estado", estado);
            sql.Comando.Parameters.AddWithValue("P_User", usuario);

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
