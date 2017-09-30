using System;
using System.Data.SqlClient;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace SqlDataAccess.Utils
{
    public class Base
    {

        public MySqlConnection ObtenerConexionSql()
        {
            try
            {
                string Server = ConfigurationManager.AppSettings["Server"].ToString();
                string Catalog = ConfigurationManager.AppSettings["Catalog"].ToString();
                string User = ConfigurationManager.AppSettings["User"].ToString();
                string Password = ConfigurationManager.AppSettings["Password"].ToString();
                return new MySqlConnection("server = " + Server + "; user id = " + User + "; database = " + Catalog + "; Password = " + Password + ";");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
