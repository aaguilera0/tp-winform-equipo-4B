using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace negocio
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        public SqlDataReader Lector
        {
            get { return lector;  }
        }
        public AccesoDatos()
        {
            conexion = new SqlConnection("Server = localhost, 1433; Database = CATALOGO_P3_DB; User Id = sa; Password = BaseDeDatos#2;TrustServerCertificate=True;");
            comando = new SqlCommand();
            
        }

        public void setearConsulta (String consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void ejecturaLectura ()
        {
            comando.Connection = conexion;
           
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void cerrarConexion()
        {
            if(lector != null)
                lector.Close();
            conexion.Close();
        }

    }
}
