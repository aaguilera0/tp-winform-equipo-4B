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
                datos.setearConsulta("SELECT A.Id, Codigo, Nombre, A.Descripcion, IdMarca, IdCategoria, Precio, E.ImagenUrl, M.Descripcion Marca, C.Descripcion Categoria FROM ARTICULOS A, IMAGENES E, MARCAS M, CATEGORIAS C WHERE E.IdArticulo = A.Id AND A.IdMarca = M.Id AND A.IdCategoria = C.Id");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = datos.Lector.GetInt32(0);
                    aux.Codigo = datos.Lector.GetString(1);
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.IdMarca = new Marca();
                    aux.IdMarca.Id = (int)datos.Lector["IdMarca"];
                    aux.IdMarca.Descripcion = (string)datos.Lector["Marca"];
                    aux.IdCategoria = new Categoria();
                    aux.IdCategoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.IdCategoria.Descripcion = (string)datos.Lector["Categoria"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.imagen = new Imagen();
                    aux.imagen.ImagenUrl = (string)datos.Lector["ImagenUrl"];

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
        }

        public void Agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Insert into ARTICULOS (Codigo, Nombre, Descripcion, Precio) values('" + nuevo.Codigo + "', '" + nuevo.Nombre + "', '" + nuevo.Descripcion + "', " + nuevo.Precio + ")");
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Modificar(Articulo articulo) {
            
            AccesoDatos accesoDatos = new AccesoDatos();

            try{

                accesoDatos.setearConsulta("update ARTICULOS set Codigo = @cod, Nombre = @nom, Descripcion = @desc, IdMarca = @idm, IdCategoria = @idc, Precio = @precio Where Id = @id");
                accesoDatos.setearParametro("@cod", articulo.Codigo);
                accesoDatos.setearParametro("@nom", articulo.Nombre);
                accesoDatos.setearParametro("@desc", articulo.Descripcion);     
                accesoDatos.setearParametro("@idm", articulo.IdMarca.Id);
                accesoDatos.setearParametro("@idc", articulo.IdCategoria.Id);
                accesoDatos.setearParametro("@precio", articulo.Precio);
                accesoDatos.setearParametro("@id", articulo.Id);

                accesoDatos.ejecutarAccion();

            }
            catch (Exception ex){

                throw ex;

            }finally{ 

                accesoDatos.cerrarConexion();

            }
        
        }

        public void Eliminar(int Id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta(" delete from ARTICULOS where id= @id ");
                datos.setearParametro("@id ", Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

