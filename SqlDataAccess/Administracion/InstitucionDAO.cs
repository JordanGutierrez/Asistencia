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
    public class InstitucionDAO : IInstitucionDAO
    {
        ConsultasSQL sql = new ConsultasSQL();

        public List<Institucion> getAllInstitucion(ref string mensaje)
        {
            List<Institucion> instituciones = new List<Institucion>();
            sql = new ConsultasSQL();
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_getAllInstitucion";

            try
            {
                IDataReader reader = sql.EjecutaReader(ref mensaje);
                while (reader.Read())
                {
                    instituciones.Add(Institucion.CreateInstitucionFromDataRecord(reader));
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return instituciones;
        }

        public Institucion getInstitucion(int id, ref string mensaje)
        {
            Institucion institucion = new Institucion();
            sql = new ConsultasSQL();
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_getInstitucion";
            sql.Comando.Parameters.AddWithValue("@InstitucionID", id);

            try
            {
                IDataReader reader = sql.EjecutaReader(ref mensaje);
                while (reader.Read())
                {
                    institucion = Institucion.CreateInstitucionFromDataRecord(reader);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return institucion;
        }

        public void insertInstitucion(Institucion institucion, string usuario, ref string mensaje)
        {
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_insertInstitucion";
            sql.Comando.Parameters.AddWithValue("@RazonSocial", institucion.RazonSocial);
            sql.Comando.Parameters.AddWithValue("@Ruc", institucion.Ruc);
            sql.Comando.Parameters.AddWithValue("@RepresentanteLegal", institucion.RepresentanteLegal);
            sql.Comando.Parameters.AddWithValue("@Direccion", institucion.Direccion);
            sql.Comando.Parameters.AddWithValue("@Telefono", institucion.Telefono);
            sql.Comando.Parameters.AddWithValue("@Correo", institucion.Correo);
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

        public void updateInstitucion(Institucion institucion, string usuario, ref string mensaje)
        {
            sql.Comando.CommandType = CommandType.StoredProcedure;
            sql.Comando.CommandText = "pa_updateInstitucion";
            sql.Comando.Parameters.AddWithValue("@InstitucionID", institucion.InstitucionID);
            sql.Comando.Parameters.AddWithValue("@RazonSocial", institucion.RazonSocial);
            sql.Comando.Parameters.AddWithValue("@Ruc", institucion.Ruc);
            sql.Comando.Parameters.AddWithValue("@RepresentanteLegal", institucion.RepresentanteLegal);
            sql.Comando.Parameters.AddWithValue("@Direccion", institucion.Direccion);
            sql.Comando.Parameters.AddWithValue("@Telefono", institucion.Telefono);
            sql.Comando.Parameters.AddWithValue("@Correo", institucion.Correo);
            sql.Comando.Parameters.AddWithValue("@Estado", institucion.Estado);
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
