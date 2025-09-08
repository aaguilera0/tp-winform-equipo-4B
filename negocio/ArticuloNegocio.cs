using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Runtime.Remoting.Lifetime;

namespace negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> Listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT A.Id, Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio , E.ImagenUrl From ARTICULOS A, IMAGENES E  where E.IdArticulo = A.iD \r\n");
                datos.ejecturaLectura();

                while (datos.Lector.Read()) 
                {
                    Articulo aux = new Articulo();
                    aux.Id = datos.Lector.GetInt32(0);
                    aux.Codigo = datos.Lector.GetString(1);
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.IdMarca = datos.Lector.GetInt32(4);
                    aux.IdCategoria = datos.Lector.GetInt32(5);
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.imagen = new Imagen();
                    aux.imagen.ImagenUrl = (string)datos.Lector["ImagenUrl"]; 
                    // FALTA EL PRECIO // 

                    lista.Add(aux);


                }
                return lista;


            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();

            }
            /*List<Articulo> lista = new List<Articulo>();

            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector; 



            try
            {
                conexion.ConnectionString = " Server = localhost,1433; Database = CATALOGO_P3_DB; User Id = sa; Password = BaseDeDatos#2;TrustServerCertificate=True;";
                //conexion.ConnectionString = " Server = localhost,1433; Database = CATALOGO_P3_DB; User Id = sa; Password = Facu-123456;TrustServerCertificate=True;";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "SELECT Id, Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio From ARTICULOS";
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
                    aux.Precio = (decimal)lector["Precio"];
                    // FALTA EL PRECIO // 

                    lista.Add(aux); 
                }

                conexion.Close();

                return lista;
            }
            catch (Exception ex)
            {

                throw ex ;
            }*/
        }
    }
}
