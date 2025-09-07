using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;    

namespace negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> Listar()
        {
            List<Articulo> lista = new List<Articulo>();

            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector; 



            try
            {
                conexion.ConnectionString = " Server = localhost,1433; Database = CATALOGO_P3_DB; User Id = sa; Password = BaseDeDatos#2;TrustServerCertificate=True;";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "SELECT Id, Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio From ARTICULOS \r\n";
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();
                
                while (lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = lector.GetInt32(0);
                    aux.Codigo = lector.GetString(1);
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];
                    aux.IdMarca = lector.GetInt32(4);
                    aux.IdCategoria = lector.GetInt32(5);
                    // FALTA EL PRECIO // 

                    lista.Add(aux); 
                }

                conexion.Close();

                return lista;
            }
            catch (Exception ex)
            {

                throw ex ;
            }
        }
    }
}
