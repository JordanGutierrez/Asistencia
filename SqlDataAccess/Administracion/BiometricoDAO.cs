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
    public class BiometricoDAO : IBiometricoDAO
    {
        ConsultasSQL sql = new ConsultasSQL();

        public List<Biometrico> getAllBiometrico(ref string mensaje)
        {
            List<Biometrico> biometricos = new List<Biometrico>();
            sql = new ConsultasSQL();
            // sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "SELECT * FROM tbBiometrico";

            try
            {
                sql.AbrirConexion();
                IDataReader reader = sql.EjecutaReader(ref mensaje);
                while (reader.Read())
                {
                    biometricos.Add(Biometrico.CreateBiometricoFromDataRecord(reader));
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

            return biometricos;
        }

        public Biometrico getBiometrico(int id, ref string mensaje)
        {
            Biometrico biometrico = new Biometrico();
            sql = new ConsultasSQL();
            //sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "SELECT * FROM tbBiometrico WHERE BiometricoID = " + id;

            try
            {
                sql.AbrirConexion();
                IDataReader reader = sql.EjecutaReader(ref mensaje);
                while (reader.Read())
                {
                    biometrico = Biometrico.CreateBiometricoFromDataRecord(reader);
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

            return biometrico;
        }

        public void insertBiometrico(Biometrico biometrico, string usuario, ref string mensaje)
        {
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_insertBiometrico";
            sql.Comando.Parameters.AddWithValue("P_CarreraID", biometrico.CarreraID);
            sql.Comando.Parameters.AddWithValue("P_Descripcion", biometrico.Descripcion);
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

        public void updateBiometrico(Biometrico biometrico, string usuario, ref string mensaje)
        {
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_updateBiometrico";
            sql.Comando.Parameters.AddWithValue("P_CarreraID", biometrico.CarreraID);
            sql.Comando.Parameters.AddWithValue("P_BiometricoID", biometrico.BiometricoID);
            sql.Comando.Parameters.AddWithValue("P_Descripcion", biometrico.Descripcion);
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
