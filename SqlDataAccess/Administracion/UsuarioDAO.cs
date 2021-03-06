﻿using DataAccess.Administracion;
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
    public class UsuarioDAO : IUsuarioDAO
    {
        ConsultasSQL sql = new ConsultasSQL();

        public List<Usuario> getAllUsuario(ref string mensaje)
        {
            List<Usuario> usuarios = new List<Usuario>();
            sql = new ConsultasSQL();
            sql.Comando.CommandText = "SELECT	U.*, F.FacultadID"
                                        + " FROM tbUsuario           U"
                                        + " INNER   JOIN tbcarrera      C"
                                        + " ON      C.CarreraID = U.CarreraID"
                                        + " INNER join tbfacultad F"
                                        + " ON F.FacultadID = C.FacultadID";

            try
            {
                IDataReader reader = sql.EjecutaReader(ref mensaje);
                while (reader.Read())
                {
                    usuarios.Add(Usuario.CreateUsuarioFromDataRecord(reader));
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return usuarios;
        }

        public Usuario getUsuario(int id, ref string mensaje)
        {
            Usuario usuario = new Usuario();
            sql = new ConsultasSQL();
            sql.Comando.CommandText = "SELECT	U.*, F.FacultadID"
                                        + " FROM tbUsuario           U"
                                        + " INNER   JOIN tbcarrera      C"
                                        + " ON      C.CarreraID = U.CarreraID"
                                        + " INNER join tbfacultad F"
                                        + " ON F.FacultadID = C.FacultadID WHERE U.UsuarioID = " + id.ToString();

            try
            {
                IDataReader reader = sql.EjecutaReader(ref mensaje);
                while (reader.Read())
                {
                    usuario = Usuario.CreateUsuarioFromDataRecord(reader);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return usuario;
        }

        public void insertUsuario(Usuario usuario, string user, string clave, ref string mensaje)
        {
            sql = new ConsultasSQL();
            sql.Comando.CommandText = "SELECT	U.*, F.FacultadID"
                                        + " FROM tbUsuario           U"
                                        + " INNER   JOIN tbcarrera      C"
                                        + " ON      C.CarreraID = U.CarreraID"
                                        + " INNER join tbfacultad F"
                                        + " ON F.FacultadID = C.FacultadID WHERE U.Cedula = '" + usuario.Cedula + "'";

            DataTable dt = sql.EjecutaDataTable(ref mensaje);
            if (dt.Rows.Count > 0)
            {
                mensaje = "El número de cédula ya se encuentra registrado";
                return;
            }
            sql = new ConsultasSQL();
            sql.Comando.CommandText = "SELECT	U.*, F.FacultadID"
                                        + " FROM tbUsuario           U"
                                        + " INNER   JOIN tbcarrera      C"
                                        + " ON      C.CarreraID = U.CarreraID"
                                        + " INNER join tbfacultad F"
                                        + " ON F.FacultadID = C.FacultadID WHERE U.Correo = '" + usuario.Correo + "'";

            dt = sql.EjecutaDataTable(ref mensaje);
            if (dt.Rows.Count > 0)
            {
                mensaje = "El correo ya se encuentra registrado";
                return;
            }
            sql = new ConsultasSQL();
            sql.Comando.CommandText = "SELECT	U.*, F.FacultadID"
                                        + " FROM tbUsuario           U"
                                        + " INNER   JOIN tbcarrera      C"
                                        + " ON      C.CarreraID = U.CarreraID"
                                        + " INNER join tbfacultad F"
                                        + " ON F.FacultadID = C.FacultadID WHERE U.CodigoBopmetrico = '" + usuario.CodigoBiometrico + "'";

            dt = sql.EjecutaDataTable(ref mensaje);
            if (dt.Rows.Count > 0)
            {
                mensaje = "El código del biométrico ya se encuentra registrado";
                return;
            }

            sql = new ConsultasSQL();
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_insertUsuario";
            sql.Comando.Parameters.AddWithValue("P_CodigoBiometrico", usuario.CodigoBiometrico);
            sql.Comando.Parameters.AddWithValue("P_BiometricoID", usuario.BiometricoID);
            sql.Comando.Parameters.AddWithValue("P_Clave", clave);
            sql.Comando.Parameters.AddWithValue("P_Cedula", usuario.Cedula);
            sql.Comando.Parameters.AddWithValue("P_Nombres", usuario.Nombres);
            sql.Comando.Parameters.AddWithValue("P_Apellidos", usuario.Apellidos);
            sql.Comando.Parameters.AddWithValue("P_Correo", usuario.Correo);
            sql.Comando.Parameters.AddWithValue("P_Telefono", usuario.Telefono);
            sql.Comando.Parameters.AddWithValue("P_Celular", usuario.Celular);
            sql.Comando.Parameters.AddWithValue("P_RolID", usuario.RolID);
            sql.Comando.Parameters.AddWithValue("P_HorarioID", usuario.HorarioID);
            sql.Comando.Parameters.AddWithValue("P_CarreraID", usuario.CarreraID);
            sql.Comando.Parameters.AddWithValue("P_Cargo", usuario.Cargo);
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

        public void restartUsuario(int id, ref string mensaje)
        {
            Usuario usuario = this.getUsuario(id, ref mensaje);
            string clave = Seguridad.SeguridadDAO.GetStringSha256Hash(usuario.Cedula);
            sql = new ConsultasSQL();
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_restartUsuario";
            sql.Comando.Parameters.AddWithValue("P_UsuarioID", id);
            sql.Comando.Parameters.AddWithValue("P_Clave", clave);

            try
            {
                sql.EjecutaQuery(ref mensaje);
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
        }

        public void restartUsuario(Usuario usuario, ref string mensaje)
        {
            string clave = Seguridad.SeguridadDAO.GetStringSha256Hash(usuario.Clave);
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_updateUsuarioClave";
            sql.Comando.Parameters.AddWithValue("P_UsuarioID", usuario.UsuarioID);
            sql.Comando.Parameters.AddWithValue("P_Clave", clave);

            try
            {
                sql.EjecutaQuery(ref mensaje);
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
        }

        public void updateUsuario(Usuario usuario, string user, ref string mensaje)
        {
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_updateUsuario";
            sql.Comando.Parameters.AddWithValue("P_CodigoBiometrico", usuario.CodigoBiometrico);
            sql.Comando.Parameters.AddWithValue("P_BiometricoID", usuario.BiometricoID);
            sql.Comando.Parameters.AddWithValue("P_UsuarioID", usuario.UsuarioID);
            sql.Comando.Parameters.AddWithValue("P_Cedula", usuario.Cedula);
            sql.Comando.Parameters.AddWithValue("P_Nombres", usuario.Nombres);
            sql.Comando.Parameters.AddWithValue("P_Apellidos", usuario.Apellidos);
            sql.Comando.Parameters.AddWithValue("P_Correo", usuario.Correo);
            sql.Comando.Parameters.AddWithValue("P_Telefono", usuario.Telefono);
            sql.Comando.Parameters.AddWithValue("P_Celular", usuario.Celular);
            sql.Comando.Parameters.AddWithValue("P_RolID", usuario.RolID);
            sql.Comando.Parameters.AddWithValue("P_HorarioID", usuario.HorarioID);
            sql.Comando.Parameters.AddWithValue("P_CarreraID", usuario.CarreraID);
            sql.Comando.Parameters.AddWithValue("P_Cargo", usuario.Cargo);
            sql.Comando.Parameters.AddWithValue("P_UsuarioModificacion", user);
            try
            {
                sql.EjecutaQuery(ref mensaje);
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
        }

        public void updateUsuarioEstado(int id, char estado, string usuario, ref string mensaje)
        {
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_updateUsuarioEstado";
            sql.Comando.Parameters.AddWithValue("P_UsuarioID", id);
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
